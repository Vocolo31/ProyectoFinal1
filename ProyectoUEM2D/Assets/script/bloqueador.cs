using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloqueador : MonoBehaviour
{
    public CameraChange cambioCamaraManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTop"))
        {
            Debug.Log("s");
            cambioCamaraManager.cambioCamaraPermitido = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTop"))
        {
            Debug.Log("salio");
            cambioCamaraManager.cambioCamaraPermitido = true;
        }
    }
}
