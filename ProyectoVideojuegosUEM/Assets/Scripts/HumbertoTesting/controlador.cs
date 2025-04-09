using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador : MonoBehaviour
{
    Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        control();
    }

    public void control()
    {
        if (animation != null && Input.GetKeyDown(KeyCode.E)) 
        {
             animation.SetBool("move", true);
        }
        if (animation != null && Input.GetKeyDown(KeyCode.Q))
        {
            animation.SetBool("move", false);
        }




    }
}
