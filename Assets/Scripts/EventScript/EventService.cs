using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService 
{
    private static EventService instance = null;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
                instance = new EventService();

            return instance;
        }
    }

    public event Action<Transform> OnCreateChest;
    public event Action<int> OnUpdateCoinCount;
    public event Action<int> OnUpdateGemCount;
    public event Action<int, string> OnCheckConfirmUnlock;
    public event Action<int> OnCheckConfirmGemsUnlock;
    public event Action OnConfirmUnlock;
    public event Action OnDenyUnlock;
    public event Action<int, int> OnRewardReceived;
    public event Action OnRewardAccepted;
    public event Action OnErrorAlreadyUnlocking;
    public event Action OnUnlockWithTimer;
    public event Action OnUnlockWithGems;
    public event Action OnOpenNextChestInQueue;
    public event Action OnChestQueueEmpty;
    public event Action<string> OnOkayPopUp;

    public void InvokeOnCreateChest(Transform chestHolder)
    {
        OnCreateChest?.Invoke(chestHolder);
    }

    public void InvokeOnUpdateCoinCount(int coinCount)
    {
        OnUpdateCoinCount?.Invoke(coinCount);
    }

    public void InvokeOnUpdateGemCount(int gemCount)
    {
        OnUpdateGemCount?.Invoke(gemCount);
    }

    public void InvokeOnCheckConfirmUnlock(int gemCount, string time)
    {
        OnCheckConfirmUnlock?.Invoke(gemCount, time);
    }

    public void InvokeOnCheckConfirmGemsUnlock(int gemCount)
    {
        OnCheckConfirmGemsUnlock?.Invoke(gemCount);
    }

    public void InvokeOnConfirmUnlock()
    {
        OnConfirmUnlock?.Invoke();
    }

    public void InvokeOnDenyUnlock()
    {
        OnDenyUnlock?.Invoke();
    }

    public void InvokeOnInsufficientGems()
    {
        OnOkayPopUp?.Invoke("Insufficient Gems!");
    }

    public void InvokeOnRewardReceived(int coinCount, int gemCount)
    {
        OnRewardReceived?.Invoke(coinCount, gemCount);
    }

    public void InvokeOnRewardAccepted()
    {
        OnRewardAccepted?.Invoke();
    }

    public void InvokeOnErrorAlreadyUnlocking()
    {
        OnErrorAlreadyUnlocking?.Invoke();
    }

    public void InvokeOnUnlockWithTimer()
    {
        OnUnlockWithTimer?.Invoke();
    }

    public void InvokeOnUnlockWithGems()
    {
        OnUnlockWithGems?.Invoke();
    }

    public void InvokeOnSlotsAreFull()
    {
        OnOkayPopUp?.Invoke("Slots are full!");
    }

    public void InvokeOnOpenNextChestInQueue()
    {
        OnOpenNextChestInQueue?.Invoke();
    }

    public void InvokeOnQueueContainsChest()
    {
        OnOkayPopUp?.Invoke("Chest already in queue!");
    }

    public void InvokeOnChestQueueFull()
    {
        OnOkayPopUp?.Invoke("Queue is full!");
    }

    public void InvokeOnChestQueueEmpty()
    {
        OnChestQueueEmpty?.Invoke();
    }

    public void InvokeOnOkayPopUp(string text)
    {
        OnOkayPopUp?.Invoke(text);
    }
}
