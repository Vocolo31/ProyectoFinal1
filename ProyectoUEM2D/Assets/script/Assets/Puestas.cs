using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puestas : MonoBehaviour
{
    public Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();

    }

}
