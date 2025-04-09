using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Vector3 desplazamiento; // Ajustes para la posicion de la camara
    public float smoothTime = 0.25f; //tiempo de smooth
    public Transform objetivo; // el objeto que seguira la camara
    private Vector3 velocidad = Vector3.zero; // velocidad de la camara

    private void Update()
    {
        if (objetivo != null) // es para que cuando la camra pierda al personaje, o sea, Muera. No salte ningun error 
        {
            //registro de seguimiento de la camara
            Vector3 targetpos = objetivo.position + desplazamiento;

            // suavisado de l movimiento de la camara
            transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref velocidad, smoothTime);
        }
    }
}
