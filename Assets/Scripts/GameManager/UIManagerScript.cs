using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    private Transform[] chestHolders;
    private Coroutine textFadeCoroutine;
    private bool coroutineRunning;

    [Header("Currency")]
    [SerializeField] private TMP_Text coinCount;
    [SerializeField] private TMP_Text gemCount;

    [Header("Chest Container")]
    [SerializeField] private Transform chestContainer;

    [Header("Confirm Unlock")]
    [SerializeField] private GameObject confirmUnlockPanel;
    [SerializeField] private TMP_Text gemCountText;
    [SerializeField] private TMP_Text timerText;

    [Header("Confirm Unlock with gems")]
    [SerializeField] private GameObject confirmUnlockWithGemsPanel;
    [SerializeField] private TMP_Text gemUnlockWithGemsText;

    [Header("Rewards Popup")]
    [SerializeField] private GameObject rewardsPopup;
    [SerializeField] private TMP_Text coinRewardCount;
    [SerializeField] private TMP_Text gemRewardCount;

    [Header("Errors")]
    [SerializeField] private CanvasGroup errorChestAlreadyOpening;

    [Header("Okay PopUp")]
    [SerializeField] private GameObject okayPopup;
    [SerializeField] private TMP_Text okayText;

    private void Awake()
    {
        chestHolders = new Transform[chestContainer.childCount];
        for (int i = 0; i < chestContainer.childCount; i++)
        {
            chestHolders[i] = chestContainer.GetChild(i);
        }

        EventService.Instance.OnUpdateCoinCount += UpdateCoinCount;
        EventService.Instance.OnUpdateGemCount += UpdateGemCount;
        EventService.Instance.OnCheckConfirmUnlock += UnlockChestPopUp;
        EventService.Instance.OnCheckConfirmGemsUnlock += UnlockChestWithGemsPopUp;
        EventService.Instance.OnRewardReceived += EnableRewardsPopup;
        EventService.Instance.OnErrorAlreadyUnlocking += ChestAlreadyBeingOpened;
        EventService.Instance.OnOkayPopUp += EnableOkayPopUp;
    }

    public Transform GetChestHolder()
    {
        foreach (Transform item in chestHolders)
        {
            if (item.childCount == 0)
                return item;
        }
        return null;
    }

    public void CreateChest()
    {
        Transform chestHolder = GetChestHolder();
        EventService.Instance.InvokeOnCreateChest(chestHolder);
    }

    public void UpdateCoinCount(int coinCountValue)
    {
        coinCount.text = coinCountValue.ToString();
    }

    public void UpdateGemCount(int gemCountValue)
    {
        gemCount.text = gemCountValue.ToString();
    }

    public void UnlockChestPopUp(int gemCount, string timer)
    {
        gemCountText.text = gemCount.ToString();
        timerText.text = timer;
        confirmUnlockPanel.SetActive(true);
    }

    public void UnlockChestWithGemsPopUp(int gemCount)
    {
        gemUnlockWithGemsText.text = "Unlock chest with " + gemCount + " gems?";
        confirmUnlockWithGemsPanel.SetActive(true);
    }

    public void CloseUnlockChestPopUp()
    {
        confirmUnlockPanel.SetActive(false);
    }

    public void UnlockChestWithTimer()
    {
        EventService.Instance.InvokeOnUnlockWithTimer();
        CloseUnlockChestPopUp();
    }

    public void UnlockChestWithGems()
    {
        EventService.Instance.InvokeOnUnlockWithGems();
        CloseUnlockChestPopUp();
    }

    public void CloseUnlockChestWithGemsPopUp()
    {
        confirmUnlockWithGemsPanel.SetActive(false);
    }

    public void EnableOkayPopUp(string text)
    {
        okayText.text = text;
        okayPopup.SetActive(true);
    }

    public void DisableOkayPopUp()
    {
        okayPopup.SetActive(false);
    }

    public void ConfirmUnlock()
    {
        CloseUnlockChestWithGemsPopUp();
        EventService.Instance.InvokeOnConfirmUnlock();
    }

    public void DenyUnlock()
    {
        CloseUnlockChestWithGemsPopUp();
        EventService.Instance.InvokeOnDenyUnlock();
    }

    public void EnableRewardsPopup(int coinCount, int gemCount)
    {
        coinRewardCount.text = coinCount.ToString();
        gemRewardCount.text = gemCount.ToString();
        rewardsPopup.SetActive(true);
    }

    public void AcceptRewards()
    {
        rewardsPopup.SetActive(false);
        EventService.Instance.InvokeOnRewardAccepted();
    }

    public void ChestAlreadyBeingOpened()
    {
        if (coroutineRunning)
        {
            StopCoroutine(textFadeCoroutine);
            coroutineRunning = false;
        }
        textFadeCoroutine = StartCoroutine(BeginFade());
    }

    private IEnumerator BeginFade()
    {
        errorChestAlreadyOpening.alpha = 1;
        coroutineRunning = true;
        yield return new WaitForSeconds(2f);

        while (errorChestAlreadyOpening.alpha > 0)
        {
            errorChestAlreadyOpening.alpha -= 0.5f * Time.deltaTime;
            yield return null;
        }
        errorChestAlreadyOpening.alpha = 0;
        coroutineRunning = false;
    }

    private void OnDestroy()
    {
        EventService.Instance.OnUpdateCoinCount -= UpdateCoinCount;
        EventService.Instance.OnUpdateGemCount -= UpdateGemCount;
        EventService.Instance.OnCheckConfirmUnlock -= UnlockChestPopUp;
        EventService.Instance.OnCheckConfirmGemsUnlock -= UnlockChestWithGemsPopUp;
        EventService.Instance.OnRewardReceived -= EnableRewardsPopup;
        EventService.Instance.OnErrorAlreadyUnlocking -= ChestAlreadyBeingOpened;
        EventService.Instance.OnOkayPopUp -= EnableOkayPopUp;
    }
}
