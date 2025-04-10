using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayer : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashinTime = 0.2f;
    [SerializeField] private float dashForce = 60f;
    [SerializeField] private float timeCanDash = 1f;
    bool dashing;
    bool canDash = true;
    public bool Dashing => dashing;
    private Rigidbody rb;
    private playerMove player;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<playerMove>();
        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        dashing = true;
        canDash = false;

        // Guardar la gravedad actual
        float gravedadOriginal = rb.velocity.y;

        // Apagar gravedad durante el dash (opcional)
        rb.useGravity = false;

        rb.velocity = new Vector3(player.Directtion * dashForce, 0f, 0f); // Si tu juego es lateral

        yield return new WaitForSeconds(dashinTime);

        // Restaurar gravedad
        rb.useGravity = true;
        rb.velocity = new Vector3(0f, gravedadOriginal, 0f); // o simplemente dejar que caiga

        dashing = false;
        yield return new WaitForSeconds(timeCanDash);
        canDash = true;
    }

}
