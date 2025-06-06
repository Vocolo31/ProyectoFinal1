﻿using System.Collections;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject TopDownCamera;
    public GameObject LateralCamera;

    public GameObject LateralChar;
    public GameObject TopChar;

    public ControladorNivel newCamera;

    public PlayerMove lateralPlayer;
    public TopDownMovement TopPlayer;

    public Vector2 lateralPlayerPosition;
    public Vector2 TopPlayerPosition;

    public bool activateTop = false; // true = TopDown, false = Lateral
    public bool cambioCamaraPermitido = true;

    private bool onTime = true;
    public float cooldownTime = 0.5f;

    public Transicion transicion;

    private void Start()
    {
        // Guarda posiciones iniciales
        TopPlayerPosition = TopPlayer.transform.position;
        lateralPlayerPosition = lateralPlayer.transform.position;

        // Inicia en vista lateral
        LateralCamera.SetActive(true);
        TopDownCamera.SetActive(false);
        activateTop = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Presioné J");

            if (cambioCamaraPermitido)
            {
                Debug.Log("Cambio permitido");
                cambioCamara();
            }
            else
            {
                Debug.Log("Cambio NO permitido");
            }
        }
    }

    IEnumerator changeTime(float time)
    {
        onTime = false;
        yield return new WaitForSeconds(time);
        onTime = true;
    }

    public void cambioCamara()
    {
        if (!onTime) return;

        PlayerMove playerMove = LateralChar.GetComponent<PlayerMove>();
        TopDownMovement topDownMovement = TopChar.GetComponent<TopDownMovement>();

        StartCoroutine(changeTime(cooldownTime));

        if (!activateTop)
        {
            // De lateral a top-down
            Debug.Log("Cambiando a TopDown");
            lateralPlayerPosition = lateralPlayer.transform.position;
            TopPlayer.transform.position = new Vector2(lateralPlayerPosition.x, TopPlayer.transform.position.y);

            LateralCamera.SetActive(false);
            TopDownCamera.SetActive(true);

            playerMove.enabled = false;
            topDownMovement.enabled = true;

            activateTop = true;
        }
        else
        {
            // De top-down a lateral
            Debug.Log("Cambiando a Lateral");
            TopPlayerPosition = TopPlayer.transform.position;
            lateralPlayer.transform.position = new Vector2(TopPlayerPosition.x, lateralPlayer.transform.position.y);

            TopDownCamera.SetActive(false);
            LateralCamera.SetActive(true);

            topDownMovement.enabled = false;
            playerMove.enabled = true;

            activateTop = false;
        }
    }
}