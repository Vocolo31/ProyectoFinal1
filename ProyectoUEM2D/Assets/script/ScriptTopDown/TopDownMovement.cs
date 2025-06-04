using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

public class TopDownMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animatorTop;
    float directionX;
    float directionY;
    public float speed;
    public Vector2 position;
    ChangeBehaviour changeBehaviour;
    public CameraChange cameraChange;
    public bool moveControl = false;
    private Vector2 lastDirection = Vector2.down;
    public float longitudrayCast = 2f;
    private Vector2 startposition;

    [Header("Dash Settings")]
    [SerializeField] private float dashinTime = 0.2f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float timeCanDash = 1f;

    public GameObject tr;
    public bool dashing;
    public bool canDash = true;
    public bool movingPuck = false;

    public LayerMask agujero;
    public bool hit;
    public bool falling;

    [Header("sonidos")]
    public AudioSource footstepAudio;
    public float stepRate = 0.4f; // Tiempo entre pasos
    public float minSpeedToPlay = 0.1f; // Mínima velocidad para sonar
    private float stepTimer;


    public void Start()
    {
        changeBehaviour = GetComponent<ChangeBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        animatorTop = GetComponent<Animator>();
        startposition = transform.position;
        footstepAudio = GetComponent<AudioSource>();
        


    }

    public void Update()
    {
        Walking();
        deactivatingTrigger();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
        detecting();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            movingPuck = true;
        }
        
        if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("Entra a la lava");
            transform.position = startposition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            movingPuck = false;
        }
    }

    public void Walking()
    {
        // Obtener input del jugador
        directionY = Input.GetAxisRaw("Vertical");
        directionX = Input.GetAxisRaw("Horizontal");

        // Normalizar el vector para que la velocidad sea constante en diagonal
        Vector2 input = new Vector2(directionX, directionY).normalized;

        // Guardar última dirección si hay movimiento
        if (input.magnitude > 0.1f)
        {
            lastDirection = input;
        }

        // Enviar datos al Animator
        animatorTop.SetFloat("Blend", directionX);
        animatorTop.SetFloat("BlendY", directionY);

        // Determinar cuál eje domina
        if (Mathf.Abs(directionX) > Mathf.Abs(directionY) + 0.1f)
        {
            animatorTop.SetFloat("Transicion", 0);
        }
        else if (Mathf.Abs(directionY) > Mathf.Abs(directionX) + 0.1f)
        {
            animatorTop.SetFloat("Transicion", 1);
        }

        // Aplicar movimiento
        if (input.magnitude > 0)
        {
            rb.velocity = input * speed;
            animatorTop.SetBool("IdleFront", false); // Se está moviendo, desactivar idle frontal
        }
        else
        {
            rb.velocity = Vector2.zero;

            // Activar Idle frontal si última dirección fue hacia abajo
            if (lastDirection.y < -0.5f)
            {
                animatorTop.SetBool("IdleFront", true);
            }
            else
            {
                animatorTop.SetBool("IdleFront", false);
            }


        }

        // Flip horizontal solo X
        if (directionX < 0 && directionY == 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (directionX > 0 && directionY == 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        float velocidad = rb.velocity.magnitude;
        if (velocidad > minSpeedToPlay)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f && !footstepAudio.isPlaying)
            {
                footstepAudio.Play();
                stepTimer = stepRate;
            }
        }
        else
        {
            stepTimer = 0f;
            footstepAudio.Stop(); // Detenemos el sonido si no se mueve
        }
    }

    public void Attack()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            animatorTop.SetTrigger("AttackTop");
        }
    }
    public void detecta()
    {
        // Raycast hacia el suelo 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudrayCast);
    }
    public void deactivatingTrigger()
    {
        changeBehaviour.enabled = cameraChange.activateTop;
    }
    private IEnumerator DashCoroutine()
    {
        dashing = true;
        canDash = false;

        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashing = false;
            canDash = true;
            yield break;
        }

        if (tr != null)
        {
            tr.SetActive(true);
        }

        float elapsedTime = 0f;
        float currentDashForce = dashForce;

        while (elapsedTime < dashinTime)
        {
            rb.velocity = dashDirection * currentDashForce;

            // Suavizar el dash reduciendo la fuerza con el tiempo
            currentDashForce = Mathf.Lerp(dashForce, 0f, elapsedTime / dashinTime);

            elapsedTime += Time.deltaTime;
            yield return null;
            
        }
        rb.velocity = Vector2.zero;

        if (tr != null)
        {
            tr.SetActive(false);
        }
        dashing = false;
        yield return new WaitForSeconds(timeCanDash);
        canDash = true;
    }
    private void OnDrawGizmos()
    {
        //color del raycast
        Gizmos.color = Color.red;

        // posicion del raycast y direccion del mismo
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudrayCast);
    }

    void detecting()
    {
        RaycastHit2D raycastInfo = Physics2D.Raycast(transform.position, Vector2.down, longitudrayCast, agujero);
        hit = raycastInfo.collider != null;
        falling = hit;
    }

    public void sonidoCaminar()
    {

    }
}

