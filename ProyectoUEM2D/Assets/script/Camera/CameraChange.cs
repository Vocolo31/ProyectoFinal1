using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject TopDownCamera;
    public GameObject LateralCamera;
    public GameObject LateralChar;
    public GameObject TopChar;
    public PlayerMove lateralPlayer;
    public TopDownMovement TopPlayer;
    public Vector2 lateralPlayerPosition;
    public Vector2 TopPlayerPosition;
    public bool activateTop;
    bool onTime = true;
    public float cooldownTime = 0.5f; // Ajusta el cooldown como prefieras
    public Transicion transicion;

    private void Start()
    {
        TopPlayerPosition = TopPlayer.transform.position;
        lateralPlayerPosition = lateralPlayer.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            cambioCamara(); // Puedes seguir invocándolo desde aquí
        }
    }

    void Detection()
    {
        activateTop = LateralCamera.activeSelf;

        TopDownCamera.SetActive(activateTop);
        LateralCamera.SetActive(!activateTop);
    }

    IEnumerator changeTime(float time)
    {
        onTime = false;
        yield return new WaitForSeconds(time);
        onTime = true;
    }

    // Esta función puedes seguir llamándola desde el evento en la animación
    public void cambioCamara()
    {
        if (!onTime) return;

        PlayerMove playerMove = LateralChar.GetComponent<PlayerMove>();
        TopDownMovement topDownMovement = TopChar.GetComponent<TopDownMovement>();

        Detection();
        StartCoroutine(changeTime(cooldownTime));

        if (activateTop)
        {
            // De lateral → top
            lateralPlayerPosition = lateralPlayer.transform.position;
            TopPlayer.transform.position = new Vector2(lateralPlayerPosition.x, TopPlayer.transform.position.y);

            playerMove.enabled = false;
            topDownMovement.enabled = true;
        }
        else
        {
            // De top → lateral
            TopPlayerPosition = TopPlayer.transform.position;
            lateralPlayer.transform.position = new Vector2(TopPlayerPosition.x, lateralPlayer.transform.position.y);

            topDownMovement.enabled = false;
            playerMove.enabled = true;
        }
    }
}
