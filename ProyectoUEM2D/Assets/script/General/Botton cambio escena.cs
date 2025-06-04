
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bottoncambioescena : MonoBehaviour
{
    [Header("Selector escena")] //Works to have a header on Unity's inspector
    public string sceneToLoad;
    public GameObject panel1;

  
    public void LoadScene() //Load scene by name
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }

        else
        {
            Debug.LogError("Algo salio mal"); //Prints a message on the console
        }
    }

    public void ExitProgram()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Pausa()
    {
        panel1.SetActive(true);
        Time.timeScale = 0;
        if (panel1 == false && Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 1;
        }
    }
    
}
