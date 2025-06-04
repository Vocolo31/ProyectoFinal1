using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActivador : MonoBehaviour
{
    Animator animacion;
    void Start()
    {
        animacion = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tutorial"))
        animacion.SetBool("in",true);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        animacion.SetBool("in", false);
        if (collision.gameObject.CompareTag("tutorial"))
        {
            Destroy(collision.gameObject);
        }
    }

}
