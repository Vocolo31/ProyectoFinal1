using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limitations : MonoBehaviour
{
    Box boxScript;
    BoxCollider2D triggerLimitation;
    public bool canChange = true;

    private void Start()
    {
        BoxCollider2D triggerLimitation = boxScript.trigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("PlayerTop"))
        {
            canChange = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.CompareTag("PlayerTop"))
        {
            canChange = true;
        }
    }
}
