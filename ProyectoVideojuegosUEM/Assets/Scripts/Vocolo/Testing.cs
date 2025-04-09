using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [Header("Managers")]
    public CinemachineTransition cameraManager;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            cameraManager.SwitchCamera(cameraManager.topDown);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            cameraManager.SwitchCamera(cameraManager.lateral);
        }
    }
}
