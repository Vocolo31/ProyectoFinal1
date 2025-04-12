using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashinTime = 0.2f;
    [SerializeField] private float dashForce = 60f;
    [SerializeField] private float timeCanDash = 1f;

    private bool dashing;
    private bool canDash = true;
    public bool Dashing => dashing;

    private Rigidbody2D rb;
    private PlayerMove player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cambiado a Rigidbody2D
        player = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canDash && Mathf.Abs(player.Directtion) > 0)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        dashing = true;
        canDash = false;

        // Guardar la velocidad vertical actual para restaurarla después
        float originalY = rb.velocity.y;

        // Realizar el dash solo en el eje X
        rb.velocity = new Vector2(player.Directtion * dashForce, 0f);

        yield return new WaitForSeconds(dashinTime);

        // Restaurar la velocidad vertical para no cortar el salto
        rb.velocity = new Vector2(0f, originalY);

        dashing = false;
        yield return new WaitForSeconds(timeCanDash);
        canDash = true;
    }
}
