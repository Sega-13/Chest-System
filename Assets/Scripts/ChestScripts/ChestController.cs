using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    public ChestModel chestModel { get; }
    public ChestView chestView { get; }

    public ChestController(ChestScriptableObject chestData)
    {
        chestModel = new ChestModel(chestData);
        chestView = GameObject.Instantiate<ChestView>(chestData.chestPrefab);
        chestView.transform.position = Vector3.zero;

        chestModel.SetChestController(this);
        chestView.SetChestController(this);
    }
    private void SetChestParent(Transform parent)
    {
        chestView.transform.SetParent(parent, false);
    }

    public void ChestOpened()
    {
        ChestService.Instance.DestroyChest(this, chestModel.chestType);
    }

    public ChestRewards GetChestRewards()
    {
        return chestModel.chestRewards;
    }

    public bool GetChestUnlockProcess()
    {
        return ChestService.Instance.GetChestUnlockProcess();
    }

    public void SetChestUnlockProcess(bool isUnlocking)
    {
        ChestService.Instance.SetChestUnlockProcess(isUnlocking);
    }

    public float GetTimeToOpen()
    {
        return chestModel.timeToOpen;
    }

    public void AddChestToQueue()
    {
        ChestService.Instance.AddChestToQueue(this);
    }

    public bool CheckIfChestAlreadyInQueue()
    {
        return ChestService.Instance.CheckIfChestAlreadyInQueue(this);
    }

    public bool CheckIfQueueIsFull()
    {
        return ChestService.Instance.CheckIfQueueIsFull();
    }

    public void ChangeChestStateToUnlocking()
    {
        chestView.ChangeChestState(chestView.chestUnlockingState);
    }

    public void EnableChest(Transform parent)
    {
        SetChestParent(parent);
        chestView.gameObject.SetActive(true);
    }

    public void DisableChest()
    {
        SetChestParent(null);
        chestView.ChangeChestState(chestView.chestLockedState);
        chestView.GetImageHolder().sprite = chestView.GetChestClosedImage();
        chestView.DisableQueueText();
        chestView.gameObject.SetActive(false);
    }
}


