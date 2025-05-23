using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agujero : MonoBehaviour
{
    public TopDownMovement playerTop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feets"))
        {
            if (!playerTop.dashing)
            {
                Debug.Log("Funciona el void");
            }
        }
    }
}
