using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right;


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

        Vector3 newPos = transform.position;

        if (newPos.x > 9) newPos.x = -9;// Right Edge → Left
        if (newPos.x < -9) newPos.x = 9;// Left Edge → Right
        if (newPos.y > 5) newPos.y = -5;// Top Edge → Bottom
        if (newPos.y < -5) newPos.y = 5;   // Bottom Edge → Top

        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)(direction * speed * Time.fixedDeltaTime);
    }
}
