using UnityEngine;
using UnityEngine.UI;

public class Minimapa : MonoBehaviour
{
    public RawImage rawImageUI;               // Asignar desde el Inspector
    public RenderTexture renderTextureA;      // Primera RenderTexture
    public RenderTexture renderTextureB;      // Segunda RenderTexture
    public CameraChange bloqueador;

    private bool usandoA = true;

    private void Start()
    {
        rawImageUI.texture = renderTextureB;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (rawImageUI != null && bloqueador != null && bloqueador.cambioCamaraPermitido)
            {
                if (renderTextureB  != null)
                {
                    rawImageUI.texture = renderTextureA;
                }
                else if (renderTextureA != null)
                {
                    rawImageUI.texture = renderTextureB;
                }
                //usandoA = !usandoA;
                //rawImageUI.texture = usandoA ? renderTextureA : renderTextureB;
            }
        }
    }
}