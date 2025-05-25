using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineDampingController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    private float originalDampingX;
    private float originalDampingY;

    private CinemachineFramingTransposer transposer;

    private void Start()
    {
        if (virtualCam != null)
        {
            transposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (transposer != null)
            {
                originalDampingX = transposer.m_XDamping;
                originalDampingY = transposer.m_YDamping;
            }
        }
    }

    public void CutDampingTemporarily(float duration = 1f)
    {
        if (transposer != null)
        {
            StartCoroutine(CutDampingCoroutine(duration));
        }
    }

    private IEnumerator CutDampingCoroutine(float duration)
    {
        // Apagar smoothing (damping)
        transposer.m_XDamping = 0f;
        transposer.m_YDamping = 0f;

        // Esperar el tiempo que dure el corte
        yield return new WaitForSeconds(duration);

        // Restaurar damping original
        transposer.m_XDamping = originalDampingX;
        transposer.m_YDamping = originalDampingY;
    }
}
