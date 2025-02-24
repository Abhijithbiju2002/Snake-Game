using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpSpawner spawner;
    public PowerUpSpawner.PowerUpType powerUpType;
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snake") || collision.CompareTag("Snak2"))
        {
            SnakeController snake = collision.GetComponent<SnakeController>();

            if (snake != null)
            {
                switch (powerUpType)
                {
                    case PowerUpSpawner.PowerUpType.Shield:
                        snake.ActivateShield(duration);
                        break;
                    case PowerUpSpawner.PowerUpType.SpeedBoost:
                        snake.ActivateSpeedBoost(duration);
                        break;
                    case PowerUpSpawner.PowerUpType.ScoreBoost:
                        snake.ActivateScoreBoost(duration);
                        break;
                }
            }

            spawner.PowerUpCollected();
            Destroy(gameObject);
        }
    }
}
