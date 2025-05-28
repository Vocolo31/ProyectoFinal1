using UnityEngine;

public class CameraChangeNew : MonoBehaviour
{
    public Camera topDown;
    public Camera lateral;

    public GameObject playerTopDown;
    public GameObject playerLateral;

    public Vector3 cameraOffset = new Vector3(0, 0, -10); // Ajusta según tu necesidad

    private bool isTopDownActive = false;

    void Start()
    {
        SetCameraView();
    }

    void Update()
    {
        FollowPlayers(); // Hacer que ambas cámaras sigan a sus jugadores

        if (Input.GetKeyDown(KeyCode.J))
        {
            isTopDownActive = !isTopDownActive;
            SyncPlayerPosition();
            SetCameraView();
        }
    }

    void SetCameraView()
    {
        topDown.enabled = isTopDownActive;
        lateral.enabled = !isTopDownActive;

        playerTopDown.SetActive(isTopDownActive);
        playerLateral.SetActive(!isTopDownActive);
    }

    void SyncPlayerPosition()
    {
        if (isTopDownActive)
        {
            Vector2 newPos = playerTopDown.transform.position;
            newPos.x = playerLateral.transform.position.x;
            playerTopDown.transform.position = newPos;
        }
        else
        {
            Vector2 newPos = playerLateral.transform.position;
            newPos.x = playerTopDown.transform.position.x;
            playerLateral.transform.position = newPos;
        }
    }

    void FollowPlayers()
    {
        if (topDown != null && playerTopDown != null)
        {
            topDown.transform.position = playerTopDown.transform.position + cameraOffset;
        }

        if (lateral != null && playerLateral != null)
        {
            lateral.transform.position = playerLateral.transform.position + cameraOffset;
        }
    }
}
