using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador : MonoBehaviour
{
    Animator animatio;
    public bool top;

    // Start is called before the first frame update
    void Start()
    {
        animatio = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        control();
    }

    public void control()
    {
        if (animatio != null && Input.GetKeyDown(KeyCode.E)) 
        {
            animatio.SetBool("move", true);
            top = true;
        }
        if (animatio != null && Input.GetKeyDown(KeyCode.Q))
        {
            animatio.SetBool("move", false);
            top = false;
        }




    }
}
