using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioDecolor : MonoBehaviour
{
    public ParticleSystem particulas;
    public CameraChangeNew cambio;
    private Color colorOriginal; // Guardamos el color original

    void Start()
    {
        colorOriginal = particulas.main.startColor.color;
    }

    public void Update()
    {
        cambioColor();
    }
    public void cambioColor()
    { 
        if (cambio.cambioCamaraPermitido == false)
        {
            Debug.Log("Cambio de color con HEX");

            // Color hexadecimal (puedes cambiarlo)
            Color colorHex;

            // Aqu� pones tu c�digo HEX (ejemplo: rojo brillante)
            if (ColorUtility.TryParseHtmlString("#B40012", out colorHex))
            {
                var main = particulas.main;
                main.startColor = colorHex;

                // Reiniciar part�culas para aplicar color inmediatamente
                particulas.Clear();
                particulas.Play();
            }
            else
            {
                Debug.LogWarning("C�digo HEX inv�lido");
            }
        }
        if (cambio.cambioCamaraPermitido == true)
        {
            Debug.Log("Restaurando color original");

            var main = particulas.main;
            main.startColor = colorOriginal;

            particulas.Clear();
            particulas.Play();
        }
    }
   
}
