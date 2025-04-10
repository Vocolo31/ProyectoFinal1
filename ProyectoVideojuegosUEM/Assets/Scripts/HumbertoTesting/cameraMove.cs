
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Vector3 desplazamiento; // Ajustes para la posicion de la camara
    public float smoothTime = 0.25f; //tiempo de smooth
    public Transform objetivo; // el objeto que seguira la camara
    private Vector3 velocidad = Vector3.zero; // velocidad de la camara
    controlador controlador = null;
    private void Update()
    {
        if (objetivo != null)
        {
            // Seguimiento solo en X y Y manteniendo la Z de la cámara fija
            Vector3 targetPos = new Vector3(
                objetivo.position.x + desplazamiento.x,
                objetivo.position.y + desplazamiento.y,
                transform.position.z// fijar la Z para mantener la vista lateral
            );

            // suvisado de movimiento de la camara
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocidad, smoothTime);
        }
    }
}
