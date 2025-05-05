using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuInGame : MonoBehaviour
{
    public GameObject panel;
    public GameObject panel2;
    public bool panel1Act; // lo use solo para experimentar. Pero hace la funcion de activar y desactivar
    void Update()
    {
       // llamados
       Pause();
    }
    public void Pause()
    {
        // pausa el juego
        if (Input.GetKeyUp(KeyCode.Escape) && panel1Act == false)
        {
            panel.SetActive(true);
            panel1Act = true;
            Time.timeScale = 0;
        }
    }
    public void BackToMenu()
    {
        // devuelve al meno principal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
    public void Opcion()
    {
        // abre el menu de opciones y cierra el menu InGame
        panel.SetActive(false);
        panel1Act = false;
        panel2.SetActive(true);
    }
    public void Back()
    {
        // El back de el menu de opciones. Puede que tengamos que cambiarlo para hacerlo mas eficiente
        panel1Act = true;    
        panel.SetActive(panel1Act);
        panel2.SetActive(!panel1Act);
    }
    public void Resume()
    {
        // Restablece el juego
        panel1Act = false;
        panel.SetActive(panel1Act);
        Time.timeScale = 1;
    }

}
