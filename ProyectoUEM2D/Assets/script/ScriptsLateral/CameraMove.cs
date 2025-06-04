using UnityEngine;

public class CharacterFollower : MonoBehaviour
{
    public Transform objetivo1;
    public Transform objetivo2;
    public TopDownMovement topCharacter; // Para saber si está siguiendo puck o no
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = Vector3.zero;
    public ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

    }
    void Update()
    {
        if (topCharacter == null) return;

        Transform objetivoActual = topCharacter.movingPuck ? objetivo2 : objetivo1;

        if (objetivoActual != null)
        {
            Vector3 targetPosition = objetivoActual.position + offset;
            targetPosition.z = transform.position.z; // Mantén la posición Z del personaje si estás en 2D

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
