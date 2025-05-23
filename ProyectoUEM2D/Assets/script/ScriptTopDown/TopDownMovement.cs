using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
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

    [Header("Dash Settings")]
    [SerializeField] private float dashinTime = 0.2f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float timeCanDash = 1f;

    public GameObject tr;
    private bool dashing;
    private bool canDash = true;
    public bool movingPuck = false;

    public void Start()
    {
        changeBehaviour = GetComponent<ChangeBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        animatorTop = GetComponent<Animator>();

    }

    public void Update()
    {
        Walking();
        deactivatingTrigger();
        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            movingPuck = true;
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

        // Enviar datos al Animator
        animatorTop.SetFloat("Blend", directionX);
        animatorTop.SetFloat("BlendY", directionY);

        // Determinar cuál eje domina
        if (Mathf.Abs(directionX) > Mathf.Abs(directionY) + 0.1f)
        {
            Debug.Log("funciona");
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
        }
        else
        {
            rb.velocity = Vector2.zero;
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
    }

    public void Attack()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            animatorTop.SetTrigger("AttackTop");
        }
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
}
