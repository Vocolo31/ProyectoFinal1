using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{
    [Header("Detección")]
    public float radioDeteccion = 5f; // longitud de raycast
    public Transform objetivo; // El jugador
    public float velocidad = 2f; // velocidad del enemigo

    private bool enRango; // detector de player

    void Update()
    {
        // si no hay objeto a detectar que haga chek
        if (objetivo == null) return;

        // registra la posicion del personaje
        float distancia = Vector2.Distance(transform.position, objetivo.position);

        // verifica si el jugador esta en trigger
        if (distancia <= radioDeteccion)
        {
            enRango = true;
        }
        else
        {
            enRango = false;
        }

        // dectecta al jugador, al detectar moverse al jugador
        if (enRango)
        {
            Vector2 direccion = (objetivo.position - transform.position).normalized;
            transform.position += (Vector3)(direccion * velocidad * Time.deltaTime);
        }
    }

    // Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
