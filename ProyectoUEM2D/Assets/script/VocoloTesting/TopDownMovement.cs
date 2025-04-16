using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    Transform transform;
    Rigidbody2D rb;
    Animator animatorTop;
    float directionX;
    float directionY;
    public float speed;

    public void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animatorTop = GetComponent<Animator>();
    }

    public void Update()
    {
        Walking();
    }

    public void Walking()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2 (directionX, directionY).normalized;

        if (input.magnitude > 0)
        {
            rb.velocity = input.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animatorTop.SetTrigger("AttackTop");
        }
    }
}
