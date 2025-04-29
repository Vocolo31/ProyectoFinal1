using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashinTime = 0.2f;
    [SerializeField] private float dashForce = 60f;
    [SerializeField] private float timeCanDash = 1f;
    [SerializeField] private TrailRenderer tr;

    private bool dashing;
    private bool canDash = true;
    public bool Dashing => dashing;
    public TopDownMovement player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (tr == null)
            tr = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        dashing = true;
        canDash = false;

        // Obtener la dirección del input
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Por si el jugador no está tocando ninguna dirección
        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.right; 
        }

        // Activar estela 
        if (tr != null)
            tr.emitting = true;

        // Aplicar la fuerza del dash
        rb.velocity = dashDirection * dashForce;
        
        yield return new WaitForSeconds(dashinTime);

        rb.velocity = Vector2.zero;

        if (tr != null)
            tr.emitting = false;

        dashing = false;
        yield return new WaitForSeconds(timeCanDash);
        
        canDash = true;
    }
}
