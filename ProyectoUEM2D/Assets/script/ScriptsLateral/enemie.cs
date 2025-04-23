using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{
    [Header("Detección")]
    public float radioDeteccion = 5f; // Radio de detección
    public float velocidad = 2f; // Velocidad del enemigo

    public Transform objetivo; // Referencia a la posición del jugador
    private Vector3 ultimaPosicionJugador; // Guarda la última posición detectada
    private bool enRango; // Verifica si el jugador está en rango
    private bool UltimaPosicionPlayer; // Para saber si debe ir a la última posición

    void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            objetivo = jugador.transform;
        }
    }

    void Update()
    {
        if (objetivo == null) return;

        float distancia = Vector2.Distance(transform.position, objetivo.position);
        enRango = distancia <= radioDeteccion;

        if (enRango)
        {
            // Guarda la última posición
            ultimaPosicionJugador = objetivo.position;
            UltimaPosicionPlayer = true;

            // Persigue al jugador
            Vector2 direccion = (objetivo.position - transform.position).normalized;
            transform.position += (Vector3)(direccion * velocidad * Time.deltaTime);
        }
        else if (UltimaPosicionPlayer)
        {
            // Se mueve a la última posición donde vio al jugador
            Vector2 direccion = (ultimaPosicionJugador - transform.position).normalized;
            transform.position += (Vector3)(direccion * velocidad * Time.deltaTime);

            // para de moverse cuando llega a esa posicion
            if (Vector2.Distance(transform.position, ultimaPosicionJugador) < 0.1f)
            {
                UltimaPosicionPlayer = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja la linea del RayCast
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
