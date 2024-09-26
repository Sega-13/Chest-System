using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLockedState : ChestState
{
    private GameObject lockedPanel;
    private float timeToUnlock;
    private string timer;
    private int gemCost;

    protected override void Awake()
    {
        base.Awake();

        lockedPanel = chestView.GetLockedPanel();
    }

    private void Start()
    {
        timeToUnlock = chestView.GetTimeToOpenChest();
        SetTimeString();
        SetGemCount();
    }

    private void SetTimeString()
    {
        float hours = Mathf.FloorToInt(timeToUnlock / 3600);
        float minutes = Mathf.FloorToInt(timeToUnlock / 60);
        float seconds = Mathf.FloorToInt(timeToUnlock % 60);
        timer = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void SetGemCount()
    {
        float minutes = Mathf.FloorToInt((timeToUnlock + 1) / 60);
        gemCost = Mathf.CeilToInt(minutes / 10);
        if (gemCost == 0)
            gemCost = 1;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        lockedPanel.SetActive(true);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        lockedPanel.SetActive(false);
    }

    private void UnlockChest()
    {
        EventService.Instance.InvokeOnCheckConfirmUnlock(gemCost, timer);

        EventService.Instance.OnUnlockWithGems += GemsBasedUnlock;
        EventService.Instance.OnUnlockWithTimer += TimerBasedUnlock;
    }

    private void TimerBasedUnlock()
    {
        EventService.Instance.OnUnlockWithGems -= GemsBasedUnlock;
        EventService.Instance.OnUnlockWithTimer -= TimerBasedUnlock;

        chestView.ChangeChestState(chestView.chestUnlockingState);
    }

    private void GemsBasedUnlock()
    {
        EventService.Instance.OnUnlockWithGems -= GemsBasedUnlock;
        EventService.Instance.OnUnlockWithTimer -= TimerBasedUnlock;

        if (CurrencyService.Instance.RemoveGems(gemCost))
            chestView.ChangeChestState(chestView.chestOpenedState);
        else
            EventService.Instance.InvokeOnInsufficientGems();
    }

    public override void OnChestClick()
    {
        base.OnChestClick();

        // if no other chest is unlocking, then unlock, else add to queue
        if (!chestView.GetChestUnlockProcess())
            UnlockChest();
        else if (chestView.CheckIfChestAlreadyInQueue())
            EventService.Instance.InvokeOnQueueContainsChest();
        else if (chestView.CheckIfQueueIsFull())
            EventService.Instance.InvokeOnChestQueueFull();
        else
        {
            chestView.EnableQueueText();
            chestView.AddChestToQueue();
        }
    }
}
