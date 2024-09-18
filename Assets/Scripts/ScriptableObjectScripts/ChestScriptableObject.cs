using UnityEngine;

[CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObjects/NewChest")]
public class ChestScriptableObject : ScriptableObject
{
    public ChestType chestType;

    [Header("Coins")]
    public int minCoins;
    public int maxCoins;

    [Header("Gems")]
    public int minGems;
    public int maxGems;

    [Header("Timer")]
    public float timeToOpen;

    public ChestView chestPrefab;
}
