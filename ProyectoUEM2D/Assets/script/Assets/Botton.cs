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
    public Animator button;
    BoxCollider2D colliderPlaca;

    public void Start()
    {
        colliderPlaca = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feets"))
        {
            activo = true;
            colliderPlaca.enabled = false;
            puertaScript.cargasActivas++;
            puerta();
            if (puertaScript.cargasActivas == puertaScript.cargasNecesarias)
            {
                puertaScript.ChangeSortingLayer();
                puertaScript.col.enabled = false;
            }
            Debug.Log("si");
        }

        button.SetBool("Active", true);

    }

    void puerta()
    {
        if (puertaScript.cargasActivas == puertaScript.cargasNecesarias)
        {
            puert.SetBool("Open", true);
        }
    }
}
