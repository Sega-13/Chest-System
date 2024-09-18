using UnityEngine;

[CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "ScriptableObjects/NewChestList")]
public class ChestScriptableObjectList : ScriptableObject
{
    public ChestScriptableObject[] chests;
}
