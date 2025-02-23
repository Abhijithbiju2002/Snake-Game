using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public enum PowerUpType
    {
        Shield,
        SpeedBoost,
        ScoreBoost
    }
    public BoxCollider2D spawnArea;
    public PowerUpType powerUpType;
    public float duration = 5f;

    public float minSpawnTime = 10f; // Minimum time between power-ups
    public float maxSpawnTime = 30f; // Maximum time between power-ups


    private void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {
        Bounds bounds = this.spawnArea.bounds;

        float x = Random.Range(bounds.min.y, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snake"))
        {
            SnakeController snake = collision.GetComponent<SnakeController>();

            if (snake != null)
            {
                switch (powerUpType)
                {
                    case PowerUpType.Shield:
                        snake.ActivateShield(duration);
                        break;
                    case PowerUpType.SpeedBoost:
                        snake.ActivateSpeedBoost(duration);
                        break;
                    case PowerUpType.ScoreBoost:
                        snake.ActivateScoreBoost(duration);
                        break;
                }
            }

            Destroy(gameObject); // Remove power-up after pickup
        }
    }
}


