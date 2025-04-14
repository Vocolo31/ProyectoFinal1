using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{
    [Header("Detección")]
    public float radioDeteccion = 5f; // Radio de detección
    public float velocidad = 2f; // Velocidad del enemigo

    private Transform objetivo; // Referencia a la posición del jugador
    private bool enRango; // Verifica si el jugador está en rango

    void Start()
    {
        // Busca el objeto con la etiqueta "Player" y obtiene su Transform
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
            Vector2 direccion = (objetivo.position - transform.position).normalized;
            transform.position += (Vector3)(direccion * velocidad * Time.deltaTime);
        }
    }
    
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
