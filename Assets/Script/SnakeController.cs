using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public enum Player { Player1, Player2 }
    public Player player; // Identify Player 1 or Player 2

    private Vector2 direction = Vector2.right;
    private Vector2 lastDirection;

    public Transform bodyPartPrefab;
    private List<Transform> bodyParts;


    private bool hasShield = false;
    private float speedMultiplier = 1f;
    private int scoreMultiplier = 1;
    public GameManager gameManager;

    public AudioSource foodSound;

    private void Start()
    {
        bodyParts = new List<Transform>();
        bodyParts.Add(this.transform);
        lastDirection = Vector2.right;
        direction = Vector2.right;
    }

    void Update()
    {
        if (player == Player.Player1)
        {
            if (Input.GetKeyDown(KeyCode.W) && lastDirection != Vector2.down)
            {
                direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && lastDirection != Vector2.up)
            {
                direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A) && lastDirection != Vector2.right)
            {
                direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) && lastDirection != Vector2.left)
            {
                direction = Vector2.right;
            }
        }
        else if (player == Player.Player2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && lastDirection != Vector2.down)
            {
                direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && lastDirection != Vector2.up)
            {
                direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && lastDirection != Vector2.right)
            {
                direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && lastDirection != Vector2.left)
            {
                direction = Vector2.right;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }

        this.transform.position = new Vector3(
           Mathf.Round(this.transform.position.x) + (direction.x * speedMultiplier),
           Mathf.Round(this.transform.position.y) + (direction.y * speedMultiplier),
           0.0f);

        lastDirection = direction;
        WrapScreen();
    }
    void WrapScreen()
    {
        Vector3 newPos = transform.position;

        if (newPos.x > 36) newPos.x = -36;
        else if (newPos.x < -36) newPos.x = 36;
        if (newPos.y > 20) newPos.y = -20;
        else if (newPos.y < -20) newPos.y = 20;

        transform.position = newPos;
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.bodyPartPrefab);
        segment.position = bodyParts[bodyParts.Count - 1].position;

        bodyParts.Add(segment);
    }
    private void ResetState()
    {
        for (int i = 1; i < bodyParts.Count; i++)
        {
            Destroy(bodyParts[i].gameObject);
        }
        bodyParts.Clear();
        bodyParts.Add(this.transform);

        this.transform.position = Vector3.zero;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
            GameManager.instance.UpdateScore(player == Player.Player1 ? 1 : 2, 10 * scoreMultiplier);

            //play food sound
            if (foodSound != null)
            {
                foodSound.Play();
            }
        }
        else if (collision.tag == "BodyPart" || collision.tag == "Snake" || collision.tag == "Snake2")
        {
            if (hasShield)
            {
                Debug.Log("Shield saved the snake! No game over.");
                return; // Do nothing, as shield prevents death
            }



            GameManager.instance.GameOver(player == Player.Player1 ? "Player 2" : "Player 1");
            ResetState();




        }
    }
    public void ActivateShield(float duration)
    {
        hasShield = true;
        Debug.Log("Shield Activated!");

        Invoke(nameof(DeactivateShield), duration);

    }
    private void DeactivateShield()
    {
        hasShield = false;
        Debug.Log("Shield Deactivated!");
    }
    public void ActivateSpeedBoost(float duration)
    {
        speedMultiplier = 1.5f; // Increase speed by 50%
        Debug.Log("Speed Boost Activated!");

        Invoke(nameof(DeactivateSpeedBoost), duration);
    }
    private void DeactivateSpeedBoost()
    {
        speedMultiplier = 1f;
        Debug.Log("Speed Boost Deactivated!");
    }
    public void ActivateScoreBoost(float duration)
    {
        scoreMultiplier = 2; // Double the score
        Debug.Log("Score Boost Activated!");

        Invoke(nameof(DeactivateScoreBoost), duration);
    }
    private void DeactivateScoreBoost()
    {
        scoreMultiplier = 1;
        Debug.Log("Score Boost Deactivated!");
    }


}
