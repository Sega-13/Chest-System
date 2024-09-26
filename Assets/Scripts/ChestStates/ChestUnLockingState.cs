using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestUnLockingState : ChestState
{
    private float timeToUnlock;
    private bool timerIsRunning;
    private int gemCost;

    private GameObject unlockingPanel;
    private TMP_Text timerText;
    private TMP_Text gemCostText;

    protected override void Awake()
    {
        base.Awake();

        unlockingPanel = chestView.GetUnlockingPanel();
        timerText = chestView.GetTimerText();
        gemCostText = chestView.GetGemCountText();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        chestView.SetChestUnlockProcess(true);
        timeToUnlock = chestView.GetTimeToOpenChest();
        timerIsRunning = true;
        unlockingPanel.SetActive(true);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        chestView.SetChestUnlockProcess(false);
        unlockingPanel.SetActive(false);
    }

    public override void OnChestClick()
    {
        base.OnChestClick();

        EventService.Instance.InvokeOnCheckConfirmGemsUnlock(gemCost);
        EventService.Instance.OnConfirmUnlock += UnlockChestWithGems;
        EventService.Instance.OnDenyUnlock += UnlockDenied;
    }

    public void UpdateTimeAndGemCount()
    {
        timeToUnlock -= Time.deltaTime;
        DisplayTime(timeToUnlock);
        FindGemCost(timeToUnlock);
        SetGemCostText();
    }

    public void UnlockChestWithGems()
    {
        if (CurrencyService.Instance.RemoveGems(gemCost))
        {
            UnlockChest();
        }
        else
            EventService.Instance.InvokeOnInsufficientGems();

        EventService.Instance.OnConfirmUnlock -= UnlockChestWithGems;
        EventService.Instance.OnDenyUnlock -= UnlockDenied;
    }

    public void UnlockDenied()
    {
        EventService.Instance.OnConfirmUnlock -= UnlockChestWithGems;
        EventService.Instance.OnDenyUnlock -= UnlockDenied;
    }

    public void UnlockChest()
    {
        timeToUnlock = 0;
        timerIsRunning = false;
        chestView.ChangeChestState(chestView.chestOpenedState);
    }

    private void DisplayTime(float time)
    {
        time += 1;
        float hours = Mathf.FloorToInt(time / 3600);
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void FindGemCost(float time)
    {
        time += 1;
        float minutes = Mathf.FloorToInt(time / 60);
        gemCost = Mathf.CeilToInt(minutes / 10);
        if (gemCost == 0)
            gemCost = 1;
    }

    private void SetGemCostText()
    {
        gemCostText.text = gemCost.ToString();
    }

    public override void Tick()
    {
        base.Tick();

        if (!timerIsRunning)
            return;

        if (timeToUnlock > 0)
            UpdateTimeAndGemCount();
        else
            UnlockChest();
    }
}
