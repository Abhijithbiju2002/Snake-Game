using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpSpawner spawner;
    public PowerUpSpawner.PowerUpType powerUpType;
    public float duration = 10f;

    public AudioSource powerUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snake") || collision.CompareTag("Snake2"))
        {
            SnakeController snake = collision.GetComponent<SnakeController>();


            if (snake != null)
            {
                PlayPowerUp();

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
    private void PlayPowerUp()
    {
        if (powerUpSound != null)
        {
            powerUpSound.Play();
        }
    }
}
