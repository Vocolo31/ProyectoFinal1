using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCamera : MonoBehaviour
{
    [Header("Cámaras")]
    public GameObject CameraTop2;
    public GameObject CameraTop1;

    [Header("Configuración")]
    public float cooldownTime = 1f;
    private bool onCooldown = false;
    public GameObject cofinder1;
    public GameObject cofinder2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTop") && !onCooldown)
        {
            CambiarCamara();
        }
    }

    private void CambiarCamara()
    {
        // Detectar qué cámara está activa
        bool estaActivaLateral = CameraTop1.activeSelf;
        bool cofinder = cofinder1.activeSelf;
        // Cambiar cámaras
        CameraTop2.SetActive(estaActivaLateral);
        CameraTop1.SetActive(!estaActivaLateral);
        // Cambiar de cofinder
        cofinder2.SetActive(cofinder);
        cofinder1.SetActive(!cofinder);
        // Iniciar cooldown
        StartCoroutine(EsperarCooldown());
    }

    private System.Collections.IEnumerator EsperarCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }
}