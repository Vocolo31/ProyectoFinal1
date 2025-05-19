using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
   
    public GameObject puntoMover;

    public bool puedoPasarArriva = true;
    public bool puedoPasarAbajo;
    public GameObject Player;

    private bool yaTeletransportado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 nuevaPosicion = new Vector3(
            puntoMover.transform.position.x,
            Player.transform.position.y,
            Player.transform.position.z
        );
        Player.transform.position = nuevaPosicion;

        yaTeletransportado = true;
        puedoPasarArriva = false;
        puedoPasarAbajo = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reseteamos la bandera cuando sale del trigger
        yaTeletransportado = false;
        puedoPasarAbajo = true;
    }
}


