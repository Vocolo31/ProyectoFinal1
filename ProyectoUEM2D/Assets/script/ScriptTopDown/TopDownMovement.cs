using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.WSA;

public class TopDownMovement : MonoBehaviour
{
    Transform transform;
    Rigidbody2D rb;
    Animator animatorTop;
    float directionX;
    float directionY;
    public float speed;
    public Vector2 position;
    ChangeBehaviour changeBehaviour;
    public CameraChange cameraChange;

    public bool moveControl = false;
    public void Start()
    {
        changeBehaviour = GetComponent<ChangeBehaviour>();
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animatorTop = GetComponent<Animator>();
    }

    public void Update()
    {
        Walking();
        deactivatingTrigger();
    }

    public void Walking()
    {
        // contolr de movimiento. asegura que el personaje quede quieto cuando la camara pasa a lateral
        Debug.Log("puedes moverte de forma vertical");
        directionY = Input.GetAxisRaw("Vertical");
        directionX = Input.GetAxisRaw("Horizontal");

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

    public void deactivatingTrigger()
    {
        changeBehaviour.enabled = cameraChange.activateTop;
    }
}
