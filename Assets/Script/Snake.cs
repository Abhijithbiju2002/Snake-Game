using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right;

    public GameObject bodyPartPrefab;
    private List<Transform> bodyParts = new List<Transform>();

    private Rigidbody2D rb;  // Add Rigidbody2D reference

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D
        rb.isKinematic = false;  // Ensure physics is applied
        rb.gravityScale = 0;  // No gravity in 2D snake game
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }


    }

    private void FixedUpdate()
    {
        MoveSnake();
        WrapScreen();


    }
    void MoveSnake()
    {
        Vector3 prevPos = transform.position;
        rb.velocity = direction * speed;// Move with Rigidbody2D velocity

        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 tempPos = bodyParts[i].position;
            bodyParts[i].position = prevPos;
            prevPos = tempPos;
        }
    }
    void WrapScreen()
    {

        Vector3 newPos = transform.position;

        if (newPos.x > 9) newPos.x = -9;// Right Edge → Left
        else if (newPos.x < -9) newPos.x = 9;// Left Edge → Right
        if (newPos.y > 5) newPos.y = -5;// Top Edge → Bottom
        else if (newPos.y < -5) newPos.y = 5;   // Bottom Edge → Top

        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snake"))
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            GrowSnake();
        }
    }
    void GrowSnake()
    {
        GameObject newPart = Instantiate(bodyPartPrefab);
        newPart.transform.position = bodyParts.Count == 0 ? transform.position : bodyParts[bodyParts.Count - 1].position;
        bodyParts.Add(newPart.transform);
    }
}
