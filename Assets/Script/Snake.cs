﻿using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right;
    private Vector2 lastDirection;

    public Transform bodyPartPrefab;
    private List<Transform> bodyParts;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDirection = direction;
        bodyParts = new List<Transform>();
        bodyParts.Add(this.transform);
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }

        MoveSnake();
        WrapScreen();
    }

    void HandleInput()
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

    void MoveSnake()
    {
        Vector3 prevPos = transform.position;
        rb.velocity = direction * speed;

        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 tempPos = bodyParts[i].position;
            bodyParts[i].position = prevPos;
            prevPos = tempPos;
        }
        lastDirection = direction;
    }

    void WrapScreen()
    {
        Vector3 newPos = transform.position;

        if (newPos.x > 9) newPos.x = -9;
        else if (newPos.x < -9) newPos.x = 9;
        if (newPos.y > 5) newPos.y = -5;
        else if (newPos.y < -5) newPos.y = 5;

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
        }
        else if (collision.tag == "BodyPart")
        {
            ResetState();
        }

    }


}
