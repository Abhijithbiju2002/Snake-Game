using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public enum PowerUpType
    {
        Shield,
        SpeedBoost,
        ScoreBoost
    }

    public GameObject powerUpPrefab; // Prefab reference for power-ups
    public BoxCollider2D spawnArea;  // The area where power-ups spawn
    public float respawnTime = 30f;  // Respawn time for power-ups

    private GameObject currentPowerUp;
    private void Start()
    {
        StartCoroutine(SpawnPowerUpLoop()); // Start continuous power-up spawning
    }

    IEnumerator SpawnPowerUpLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        Vector3 spawnPos = GetRandomPosition();
        GameObject newPowerUp = Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
        newPowerUp.GetComponent<PowerUp>().spawner = this; // Link to spawner
    }

    public Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    public void PowerUpCollected()
    {
        currentPowerUp = null; // Reset the tracker when collected
    }
}
