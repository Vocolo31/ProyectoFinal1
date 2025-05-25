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
    public ChangeBehaviour ChangeBehaviour;
    public TopDownMovement TopPlayer;
    public Vector3 lateralPlayerPosition;
    public Vector3 TopPlayerPosition;
    public bool activateTop;
    bool onTime = true;
    public float cooldownTime;
    public Transicion transicion;
    void Update()
    {
        PlayerMove playerMove = LateralChar.GetComponent<PlayerMove>();
        TopDownMovement topDownMovement = TopChar.GetComponent<TopDownMovement>();

       
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

    public void cambioCamara()
    {
        PlayerMove playerMove = LateralChar.GetComponent<PlayerMove>();
        TopDownMovement topDownMovement = TopChar.GetComponent<TopDownMovement>();
        if (onTime && (ChangeBehaviour.enabled.Equals(false) || ChangeBehaviour.canChange))
        {
            Detection();
            StartCoroutine(changeTime(cooldownTime));

        }
        if (activateTop)
        {
            /* lateralPlayerPosition = lateralPlayer.transform.position;
             TopPlayer.transform.position = new Vector3(lateralPlayerPosition.x, 25f, -0.5f);*/

            playerMove.enabled = false;
            topDownMovement.enabled = true;
        }

        else if (!activateTop)
        {
            /*TopPlayerPosition = TopPlayer.transform.position;
             lateralPlayer.transform.position = new Vector3(TopPlayerPosition.x, -0.450f, -0.500f);*/

            topDownMovement.enabled = false;
            playerMove.enabled = true;
        }
    }
}
