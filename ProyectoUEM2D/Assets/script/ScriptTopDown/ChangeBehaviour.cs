using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBehaviour : MonoBehaviour
{

    public bool canChange = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            canChange = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            canChange = true;
        }
    }
}
