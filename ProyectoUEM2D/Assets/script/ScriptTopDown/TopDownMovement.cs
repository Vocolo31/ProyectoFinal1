using System.Collections;
using System.Collections.Generic;
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

    public void Walking()
    {
        // Contolr de movimiento. asegura que el personaje quede quieto cuando la camara pasa a lateral
        
        directionY = Input.GetAxisRaw("Vertical");
        directionX = Input.GetAxisRaw("Horizontal");
        float directionYX = directionY + directionX;

        Vector2 input = new Vector2 (directionX, directionY).normalized;
        animatorTop.SetFloat("Blend", Mathf.Abs(directionYX));

        if (input.magnitude > 0)
        {
            rb.velocity = input.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        //animatorTop.SetFloat("Blend", Mathf.Abs(directionYX));
        // Voltear personaje según la dirección
        if (directionYX < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (directionYX > 0)
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

        // Obtener la dirección del input
        Vector2 directionYX = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Si el jugador no está tocando ninguna dirección
        if (directionYX == Vector2.zero)
        {
            directionYX = Vector2.right;
        }

        // Activar estela 
        tr.SetActive(true);
            
        // Aplicar la fuerza del dash
        rb.velocity = directionYX * dashForce;

        yield return new WaitForSeconds(dashinTime);
        
        rb.velocity = Vector2.zero;

        if (dashing && !canDash)
        {
            tr.SetActive(false);
                
        }

        dashing = false;
        yield return new WaitForSeconds(timeCanDash);

        canDash = true;
    }
}
