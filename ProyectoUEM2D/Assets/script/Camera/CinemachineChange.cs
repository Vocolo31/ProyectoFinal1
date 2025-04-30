using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineChange : MonoBehaviour
{
    public CinemachineVirtualCamera TopDownCamera;
    public CinemachineVirtualCamera LateralCamera;
    public GameObject LateralChar;
    public GameObject TopChar;
    public PlayerMove lateralPlayer;
    public ChangeBehaviour ChangeBehaviour;
    public TopDownMovement TopPlayer;
    public float cooldownTime;
    bool onTime = true;
    public bool activateTop;

    void Start()
    {
        // Asegurarse de que el cambio es instantáneo (sin blends)
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        if (brain != null)
        {
            brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        }
    }

    void Update()
    {
        PlayerMove playerMove = LateralChar.GetComponent<PlayerMove>();
        TopDownMovement topDownMovement = TopChar.GetComponent<TopDownMovement>();

        if ((Input.GetKeyDown(KeyCode.J) && onTime) && (ChangeBehaviour.enabled.Equals(false) || ChangeBehaviour.canChange))
        {
            ToggleCameraPriority();
            StartCoroutine(changeTime(cooldownTime));

            if (activateTop)
            {
                playerMove.enabled = false;
                topDownMovement.enabled = true;
            }
            else
            {
                topDownMovement.enabled = false;
                playerMove.enabled = true;
            }
        }
    }

    void ToggleCameraPriority()
    {
        activateTop = LateralCamera.Priority > TopDownCamera.Priority;

        if (activateTop)
        {
            TopDownCamera.Priority = 10;
            LateralCamera.Priority = 0;
        }
        else
        {
            TopDownCamera.Priority = 0;
            LateralCamera.Priority = 10;
        }
    }

    IEnumerator changeTime(float time)
    {
        onTime = false;
        yield return new WaitForSeconds(time);
        onTime = true;
    }
}