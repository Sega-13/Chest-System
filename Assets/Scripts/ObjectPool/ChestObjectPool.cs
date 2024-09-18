using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObjectPool 
{
    private ChestScriptableObject chestData;
    private List<PoolItem> itemPool = new();

    public ChestObjectPool(ChestScriptableObject _chestData)
    {
        chestData = _chestData;
    }
    public ChestController GetItem()
    {
        if (itemPool.Count == 0)
            return CreatePooledItem().item;

        PoolItem pooledItem = itemPool.Find(newItem => newItem.isUsed == false);
        if (pooledItem != null)
        {
            pooledItem.isUsed = true;
            return pooledItem.item;
        }

        return CreatePooledItem().item;
    }

    private PoolItem CreatePooledItem()
    {
        PoolItem newPoolItem = new PoolItem();
        newPoolItem.item = CreateItem();
        newPoolItem.isUsed = true;
        itemPool.Add(newPoolItem);
        return newPoolItem;
    }

    public void ReturnItem(ChestController chestController)
    {
        PoolItem poolItem = itemPool.Find(newItem => newItem.item == chestController);
        if (poolItem != null)
            poolItem.isUsed = false;
    }

    private ChestController CreateItem()
    {
        ChestController chestController = new ChestController(chestData);
        return chestController;
    }
}
public class PoolItem
{
    public ChestController item;
    public bool isUsed;
}
