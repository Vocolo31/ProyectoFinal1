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

    [Header("Prueba Escaleras")]
    public Transform punto1;
    public Transform punto2;
    bool punto1Bool;
    bool punto2Bool;
  
    public void Start()
    {
        changeBehaviour = GetComponent<ChangeBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        animatorTop = GetComponent<Animator>();

    }

    public void Update()
    {
        SubirBajar();
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
        

        Vector2 input = new Vector2 (directionX, directionY).normalized;
        animatorTop.SetFloat("Blend", input.magnitude);

        if (input.magnitude > 0)
        {
            rb.velocity = input.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
        // Voltear personaje según la dirección
        if (directionX < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (directionX > 0)
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
    public void SubirBajar()
    {
        if (punto1Bool == true && Input.GetKeyUp(KeyCode.E))
        {
            transform.position = punto2.position;
        }
        if (punto2Bool == true && Input.GetKeyUp(KeyCode.E))
        {
            transform.position = punto1.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EEEEEEEE");
        if (collision.CompareTag("EscaleraA"))
        {
            punto1Bool = true;
            punto2Bool = false;
        }
        if (collision.CompareTag("EscaleraB"))
        {
            punto1Bool = false;
            punto2Bool = true;
        }
    }
}
