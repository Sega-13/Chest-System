using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService : GenericMonoSingleton<ChestService>
{
    private ChestObjectPool commonChestObjectPool;
    private ChestObjectPool rareChestObjectPool;
    private ChestObjectPool epicChestObjectPool;
    private ChestObjectPool legendaryChestObjectPool;

    private Queue<ChestController> chestQueue = new();
    private List<ChestController> chestControllers = new();
    private bool chestUnlockingInProcess;

    [SerializeField] private int numberOfSlots = 4;
    [SerializeField] private int queueLength = 2;
    [SerializeField] private ChestScriptableObjectList chestScriptableObjectList;

    protected override void Awake()
    {
        base.Awake();

        commonChestObjectPool = new ChestObjectPool(chestScriptableObjectList.chests[0]);
        rareChestObjectPool = new ChestObjectPool(chestScriptableObjectList.chests[1]);
        epicChestObjectPool = new ChestObjectPool(chestScriptableObjectList.chests[2]);
        legendaryChestObjectPool = new ChestObjectPool(chestScriptableObjectList.chests[3]);
    }
    private void Start()
    {
        EventService.Instance.OnCreateChest += CreateRandomChest;
        EventService.Instance.OnOpenNextChestInQueue += OpenNextChestInQueue;
    }
    private void CreateRandomChest(Transform chestHolder)
    {
        if (chestControllers.Count == numberOfSlots || chestHolder == null)
        {
            EventService.Instance.InvokeOnSlotsAreFull();
            return;
        }
        CreateChest((ChestType)Random.Range(0, chestScriptableObjectList.chests.Length), chestHolder);
    }
    public void CreateChest(ChestType chestType, Transform chestHolder)
    {
        ChestScriptableObject chestData = chestScriptableObjectList.chests[(int)chestType];
        ChestController newChestController = null;

        switch (chestType)
        {
            case ChestType.Common:
                newChestController = commonChestObjectPool.GetItem();
                break;
            case ChestType.Rare:
                newChestController = rareChestObjectPool.GetItem();
                break;
            case ChestType.Epic:
                newChestController = epicChestObjectPool.GetItem();
                break;
            case ChestType.Legendary:
                newChestController = legendaryChestObjectPool.GetItem();
                break;
        }

        newChestController.EnableChest(chestHolder);
        chestControllers.Add(newChestController);
    }
    public void DestroyChest(ChestController chestController, ChestType chestType)
    {
        switch (chestType)
        {
            case ChestType.Common:
                commonChestObjectPool.ReturnItem(chestController);
                break;
            case ChestType.Rare:
                rareChestObjectPool.ReturnItem(chestController);
                break;
            case ChestType.Epic:
                epicChestObjectPool.ReturnItem(chestController);
                break;
            case ChestType.Legendary:
                legendaryChestObjectPool.ReturnItem(chestController);
                break;
        }

        chestControllers.Remove(chestController);
        chestController.DisableChest();
    }
    public bool GetChestUnlockProcess()
    {
        return chestUnlockingInProcess;
    }

    public void SetChestUnlockProcess(bool isUnlocking)
    {
        chestUnlockingInProcess = isUnlocking;
    }

    public bool CheckIfQueueIsFull()
    {
        return chestQueue.Count == queueLength;
    }

    public void AddChestToQueue(ChestController chestController)
    {
        if (chestQueue.Count < queueLength)
            chestQueue.Enqueue(chestController);
    }

    public bool CheckIfChestAlreadyInQueue(ChestController chestController)
    {
        return chestQueue.Contains(chestController);
    }

    public void OpenNextChestInQueue()
    {
        if (chestQueue.Count == 0)
            return;

        ChestController chestController = chestQueue.Dequeue();
        chestController.ChangeChestStateToUnlocking();
    }

    private void OnDestroy()
    {
        EventService.Instance.OnCreateChest -= CreateRandomChest;
        EventService.Instance.OnOpenNextChestInQueue -= OpenNextChestInQueue;
    }

}
