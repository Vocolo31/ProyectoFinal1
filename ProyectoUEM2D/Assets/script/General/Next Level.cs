
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextlevel : MonoBehaviour
{
    [Header("Selector escena")] //Works to have a header on Unity's inspector
    public string sceneToLoad;

    public void LoadScene() //Load scene by name
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        else
        {
            Debug.LogError("Algo salio mal"); //Prints a message on the console
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crab"))
        {
            LoadScene();
        }
    }
}
