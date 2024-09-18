using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestModel 
{
    public ChestRewards chestRewards { get; }
    public float timeToOpen { get; }
    public ChestType chestType { get; }

    private ChestController chestController;

    public ChestModel(ChestScriptableObject chestData)
    {
        chestRewards = new ChestRewards();

        chestRewards.minCoins = chestData.minCoins;
        chestRewards.maxCoins = chestData.maxCoins;
        chestRewards.minGems = chestData.minGems;
        chestRewards.maxGems = chestData.maxGems;

        chestType = chestData.chestType;
        timeToOpen = chestData.timeToOpen;
    }

    public void SetChestController(ChestController _chestController)
    {
        chestController = _chestController;
    }
}

public class ChestRewards
{
    public int minCoins;
    public int maxCoins;
    public int minGems;
    public int maxGems;
}

