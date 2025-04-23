using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Rendering;
using UnityEngine;

public class Botton : MonoBehaviour
{
    public bool activo;
    public Animator puert;
    public Puestas puertaScript;

    public void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTop"))
        {
            activo = true;
            puerta();
            puertaScript.col.enabled = false;
            Debug.Log("si");

        }



    }

    void puerta()
    {
        if (activo)
        {
            puert.SetBool("Open", true);
        }
    }
}
