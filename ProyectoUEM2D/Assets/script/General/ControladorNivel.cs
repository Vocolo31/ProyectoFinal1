using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNivel : MonoBehaviour
{
    public int pisoActual = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("segundoPiso"))
        {
            pisoActual = 2;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        
            pisoActual = 1;
        
    }

}
