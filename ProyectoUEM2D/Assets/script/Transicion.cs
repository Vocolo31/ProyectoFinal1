using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Transicion : MonoBehaviour
{
    public Animator transicion;
    public CameraChange cambio;
    public bool activo1 = false;
    

    public void Start()
    {
        transicion = GetComponent<Animator>();
    }
    private void Update()
    {
        activo1 = transicion.GetBool("activo");
        if (Input.GetKey(KeyCode.J))
        {
            animacion();
        }
    }
    public void animacion()
    {
        if (activo1)
        {
            transicion.SetBool("activo", false);
        }

        if (!activo1)
        {
            transicion.SetBool("activo", true);
        }
    }
    public void cambios()
    {
        cambio.cambioCamara();
    }
    

}
