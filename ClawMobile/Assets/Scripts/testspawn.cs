using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testspawn : MonoBehaviour
{
    [Header("Circle Settings")]
    public GameObject circlePrefab; // Assign your circle prefab in the Inspector
    public int circleCount = 40;    // Desired number of circles in the level

    [Header("Spawn Settings")]
    public Vector2 spawnAreaMin = new Vector2(-10, -5); // Bottom-left corner of spawn area
    public Vector2 spawnAreaMax = new Vector2(10, 5);   // Top-right corner of spawn area

    private List<GameObject> circles = new List<GameObject>();

    void Start()
    {
        InitializeCircles();
    }

    void Update()
    {
        MaintainCircles();
    }

    /// <summary>
    /// Initialize the scene with the required number of circles.
    /// </summary>
    void InitializeCircles()
    {
        for (int i = 0; i < circleCount; i++)
        {
            SpawnCircle();
        }
    }

    /// <summary>
    /// Maintain the required number of circles by replacing destroyed ones.
    /// </summary>
    void MaintainCircles()
    {
        for (int i = circles.Count - 1; i >= 0; i--)
        {
            if (circles[i] == null) // Check if a circle was destroyed
            {
                circles.RemoveAt(i); // Remove from the list
                SpawnCircle();       // Replace it with a new one
            }
        }
    }

    /// <summary>
    /// Spawns a new circle at a random position within the defined area.
    /// </summary>
    void SpawnCircle()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        circles.Add(newCircle);
    }
}
