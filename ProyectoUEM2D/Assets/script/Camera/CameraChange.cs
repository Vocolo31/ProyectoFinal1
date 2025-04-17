using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject TopDownCamera;
    public GameObject LateralCamera;
    public PlayerMove lateralPlayer;
    public TopDownMovement TopPlayer;
    public Box boxScript;
    public Vector3 lateralPlayerPosition;
    public Vector3 TopPlayerPosition;
    bool activateTop;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && boxScript.canChange)
        {
            Detection();

            if (activateTop)
            {
                lateralPlayerPosition = lateralPlayer.transform.position;
                TopPlayer.transform.position = new Vector3(lateralPlayerPosition.x, 25f, -0.5f);
            }

            else if (!activateTop)
            {
                TopPlayerPosition = TopPlayer.transform.position;
                lateralPlayer.transform.position = new Vector3(TopPlayerPosition.x, -0.450f, -0.500f);
            }
        }   
    }

    void Detection()
    {
            activateTop = LateralCamera.activeSelf;

            TopDownCamera.SetActive(activateTop);
            LateralCamera.SetActive(!activateTop);
    }
}
