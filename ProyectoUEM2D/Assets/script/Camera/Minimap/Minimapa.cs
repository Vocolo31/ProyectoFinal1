using UnityEngine;
using UnityEngine.UI;

public class Minimapa : MonoBehaviour
{
    public RawImage rawImageUI;               // Asignar desde el Inspector
    public RenderTexture renderTextureA;      // Primera RenderTexture
    public RenderTexture renderTextureB;      // Segunda RenderTexture

    private bool usandoA = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (rawImageUI != null)
            {
                rawImageUI.texture = usandoA ? renderTextureB : renderTextureA;
                usandoA = !usandoA;
            }
        }
    }
}