using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puestas : MonoBehaviour
{
    public Collider2D col;
    public Animator animator;
    public int cargasNecesarias;
    public int cargasActivas;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void ChangeSortingLayer()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        GetComponent<SpriteRenderer>().sortingOrder = 5;
    }

}
