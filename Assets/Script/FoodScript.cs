using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public GameObject foodPrefab;

    void Start()
    {
        InvokeRepeating(nameof(SpawnFood), 1f, 5f); // Spawns food every 5 seconds
    }


    void SpawnFood()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-9, 9), Random.Range(-5, 5));
        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}
