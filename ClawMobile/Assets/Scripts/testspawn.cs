using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testspawn : MonoBehaviour
{
  
    [Header("Circle Settings")]
    public string PrizeTag = "Prize"; // Tag assigned to all circle objects
    public GameObject circlePrefab;    // Prefab used for respawning circles
    public int targetCircleCount = 40; // Desired number of circles

    private List<GameObject> circles = new List<GameObject>();

     [Header("Spawn Area Settings")]
    public Vector2 spawnAreaMin = new Vector2(-10, -5); // Minimum spawn position
    public Vector2 spawnAreaMax = new Vector2(10, 5);   // Maximum spawn position


    void Start()
    {
        InitializeCircleList();
    }

    void Update()
    {
        MaintainCircles();
    }

    /// <summary>
    /// Initialize the list by finding all circles with the specified tag.
    /// </summary>
    void InitializeCircleList()
    {
        circles.Clear(); // Clear the list first
        GameObject[] foundCircles = GameObject.FindGameObjectsWithTag(PrizeTag);
        circles.AddRange(foundCircles); // Add all found circles to the list
    }

    /// <summary>
    /// Maintain the required number of circles by replacing destroyed ones.
    /// </summary>
    void MaintainCircles()
    {
        // Remove null entries from the list
        circles.RemoveAll(circle => circle == null);

        // Respawn circles if the count drops below the target
        while (circles.Count < targetCircleCount)
        {
            SpawnCircleAtRandom();
        }
    }

    /// <summary>
    /// Spawns a new circle at a random position within the scene.
    /// </summary>
    void SpawnCircleAtRandom()
    {
        // Define random spawn position (you can customize this)
         Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instantiate a new circle and add it to the list
        GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        newCircle.tag = PrizeTag; // Ensure the new circle has the correct tag
        circles.Add(newCircle);
    }
}
