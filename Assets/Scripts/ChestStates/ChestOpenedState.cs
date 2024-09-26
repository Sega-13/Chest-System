using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpenedState : ChestState
{
    private int coinReward;
    private int gemReward;
    private GameObject chestOpenedPanel;
    private Image imageHolder;
    private Sprite chestOpenedImage;

    protected override void Awake()
    {
        base.Awake();

        chestOpenedPanel = chestView.GetOpenedPanel();
        imageHolder = chestView.GetImageHolder();
        chestOpenedImage = chestView.GetChestOpenedImage();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        EventService.Instance.InvokeOnOpenNextChestInQueue();
        chestOpenedPanel.SetActive(true);
    }

    public override void OnChestClick()
    {
        base.OnChestClick();

        imageHolder.sprite = chestOpenedImage;
        chestOpenedPanel.SetActive(false);

        ChestRewards chestRewards = chestView.GetChestRewardsFromController();
        coinReward = Random.Range(chestRewards.minCoins, chestRewards.maxCoins + 1);
        gemReward = Random.Range(chestRewards.minGems, chestRewards.maxGems + 1);

        EventService.Instance.InvokeOnRewardReceived(coinReward, gemReward);
        EventService.Instance.OnRewardAccepted += CollectRewards;
    }

    private void CollectRewards()
    {
        ChestService.Instance.AddCurrency(coinReward, gemReward);

        EventService.Instance.OnRewardAccepted -= OnStateExit;
        EventService.Instance.OnRewardAccepted -= CollectRewards;

        chestView.ChestCollected();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
