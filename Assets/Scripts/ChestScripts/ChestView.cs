using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    private ChestController chestController;
    private ChestState currentChestState;

    [SerializeField] private Image imageHolder;
    [SerializeField] private ChestType chestType;
    [SerializeField] private Sprite chestClosedImage;
    [SerializeField] private Sprite chestOpenedImage;

    [Header("State Serialize Fields")]
    [SerializeField] private GameObject lockedPanel;
    [SerializeField] private GameObject inQueueText;
    [SerializeField] private GameObject unlockingPanel;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text gemCountText;
    [SerializeField] private GameObject chestOpenedPanel;

    [Header("States")]
    public ChestLockedState chestLockedState;
    public ChestUnLockingState chestUnlockingState;
    public ChestOpenedState chestOpenedState;

    private void Awake()
    {
        chestLockedState = GetComponent<ChestLockedState>();
        chestUnlockingState = GetComponent<ChestUnLockingState>();
        chestOpenedState = GetComponent<ChestOpenedState>();
    }
    private void Start()
    {
        imageHolder.sprite = chestClosedImage;
        ChangeChestState(chestLockedState);
    }
    public void SetChestController(ChestController _chestController)
    {
        chestController = _chestController;
    }

    public GameObject GetLockedPanel()
    {
        return lockedPanel;
    }

    public GameObject GetUnlockingPanel()
    {
        return unlockingPanel;
    }

    public GameObject GetOpenedPanel()
    {
        return chestOpenedPanel;
    }

    public Image GetImageHolder()
    {
        return imageHolder;
    }

    public Sprite GetChestClosedImage()
    {
        return chestClosedImage;
    }

    public Sprite GetChestOpenedImage()
    {
        return chestOpenedImage;
    }

    public TMP_Text GetTimerText()
    {
        return timerText;
    }

    public TMP_Text GetGemCountText()
    {
        return gemCountText;
    }

    public ChestRewards GetChestRewardsFromController()
    {
        return chestController.GetChestRewards();
    }

    public float GetTimeToOpenChest()
    {
        return chestController.GetTimeToOpen();
    }

    public void ClickedOnChest()
    {
        currentChestState.OnChestClick();
    }

    public void ChestCollected()
    {
        chestController.ChestOpened();
    }

    public bool GetChestUnlockProcess()
    {
        return chestController.GetChestUnlockProcess();
    }

    public void SetChestUnlockProcess(bool isUnlocking)
    {
        chestController.SetChestUnlockProcess(isUnlocking);
    }

    public void EnableQueueText()
    {
        inQueueText.SetActive(true);
    }

    public void DisableQueueText()
    {
        if (inQueueText.activeInHierarchy)
            inQueueText.SetActive(false);
    }

    public void AddChestToQueue()
    {
        chestController.AddChestToQueue();
    }

    public bool CheckIfChestAlreadyInQueue()
    {
        return chestController.CheckIfChestAlreadyInQueue();
    }

    public bool CheckIfQueueIsFull()
    {
        return chestController.CheckIfQueueIsFull();
    }

    public void ChangeChestState(ChestState newChestState)
    {
        if (currentChestState != null)
            currentChestState.OnStateExit();

        currentChestState = newChestState;
        currentChestState.OnStateEnter();
    }

    private void Update()
    {
        currentChestState.Tick();
    }
}
