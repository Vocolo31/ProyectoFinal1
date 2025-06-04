using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pasarLv : MonoBehaviour
{
    public bool puedePasar;

    public void Update()
    {
        if (puedePasar && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Cambiando a la escena: ");
            SceneManager.LoadScene(2);
            puedePasar = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTop"))
        {
            puedePasar = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        puedePasar = false;
    }
}
