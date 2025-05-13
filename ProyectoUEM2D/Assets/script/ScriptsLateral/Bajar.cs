using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bajar : MonoBehaviour
{
    SubirYBajar bajar;
    public GameObject puntoMover;
    private bool yaTeletransportado = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (yaTeletransportado) return;

        if (bajar != null && bajar.puedoPasarAbajo)
        {
            Debug.Log("Bajando");
            collision.transform.position = puntoMover.transform.position;

            // Evita múltiples teletransportes seguidos
            yaTeletransportado = true;

            // Evita que pueda volver a pasar hasta que se lo permitas desde otro código
            bajar.puedoPasarAbajo = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        yaTeletransportado = false;
    }
}
