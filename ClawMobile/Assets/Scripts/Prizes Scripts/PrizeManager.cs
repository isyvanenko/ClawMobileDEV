using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeManager : MonoBehaviour
{
    [Header("List of Prizes")]
    public List<Prize> prizePool = new List<Prize>();

    private List<Prize> collectedPrizes = new List<Prize>();

    public Transform spawnPoint; // Position to spawn the prize prefab

    // Function to spawn a random prize based on rarity
    public void SpawnRandomPrize()
    {
        Prize selectedPrize = GetRandomPrize();
        if (selectedPrize != null)
        {
            // Spawn the selected prize prefab at spawnPoint
            Instantiate(selectedPrize.prizePrefab, spawnPoint.position, Quaternion.identity);

            // Add the prize to the player's collection
            collectedPrizes.Add(selectedPrize);

            Debug.Log($"You won: {selectedPrize.prizeName}");
        }
    }

    private Prize GetRandomPrize()
    {
        // Calculate the total weight of all prizes
        int totalWeight = 0;
        foreach (Prize prize in prizePool)
        {
            totalWeight += prize.rarityWeight;
        }

        // Select a random number between 0 and totalWeight
        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        // Loop through the prize pool to find the prize
        foreach (Prize prize in prizePool)
        {
            currentWeight += prize.rarityWeight;
            if (randomValue < currentWeight)
            {
                return prize; // Return the selected prize
            }
        }

        return null; // Fallback in case no prize is selected
    }

    // Function to retrieve collected prizes
    public List<Prize> GetCollectedPrizes()
    {
        return collectedPrizes;
    }
}
