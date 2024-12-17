using UnityEngine;

[System.Serializable]
public class Prize
{
    public string prizeName;     // Name of the prize
    public GameObject prizePrefab; // The prize prefab to spawn
    public Sprite prizeIcon;     // Icon for UI display
    public int rarityWeight;     // The weight of rarity (higher = more common)
}
