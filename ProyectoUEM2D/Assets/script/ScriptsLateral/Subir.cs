using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SubirYBajar : MonoBehaviour
{
    public GameObject puntoMover;

    public bool puedoPasarArriva = true;
    public bool puedoPasarAbajo;
    public GameObject Player;

    private bool yaTeletransportado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (yaTeletransportado) return;

        if (puedoPasarArriva)
        {
            Debug.Log("Subiendo");
            Player.transform.position = puntoMover.transform.position;
            yaTeletransportado = true;


            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reseteamos la bandera cuando sale del trigger
        yaTeletransportado = false;
        puedoPasarAbajo = true;
    }
}
