using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [Header("Controlador del player")]
    private Rigidbody2D rb; // f�sicas del personaje
    public float speed = 3f; // velocidad del personaje
    public float jumpForce; // fuerza de salto
    public float sprintSpeed = 5f; // velocidad de sprint

    [Header("Vida")]
    public int vidaMax = 3;
    public int vidaActual = 3;

    [Header("Settings")]
    public float coyoteTime = 0.2f; // Tiempo de gracia para saltar despu�s de caer
    private float coyoteTimeCounter; // Contador para coyote time
    public bool enSuelo; // indica si el jugador est� en el suelo
    public float longitudrayCast = 2f; // largo del raycast para el suelo
    public LayerMask capaSuelo; // filtro que identifica el suelo
    public bool hit; // raycast colisiona o no
    bool dobelJump = false; // doble salto
    bool running = true; // verifica si est� corriendo
    bool jumping; // verifica si est� saltando
    Animator animator; // controlador de animaci�n
    float direction;
    private Dash dash;

    public bool moveControlerL = true;
    public float Directtion => direction;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();
    }

    void Update()
    {
        // llamados
        Move();
        sprint();
        ataque();
        dead();
        jump();
    }

    private void Move()
    {
        float direccion = Input.GetAxisRaw("Horizontal");

        // limita el movimiento del personaje
        if (Mathf.Abs(direccion) > 0.01f)
        {
            rb.velocity = new Vector2(direccion * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        // guarda la direccion del personaje
        direction = direccion;

        // Voltear personaje según la dirección
        if (direccion < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (direccion > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        animator.SetFloat("Blend", Mathf.Abs(direccion));
    }

    public void jump()
    {
        // Ray cast, se encarga de verificar que el objeto se encunetre en colision con el suelo
        RaycastHit2D raycastInfo = Physics2D.Raycast(transform.position, Vector2.down, longitudrayCast, capaSuelo);
        hit = raycastInfo.collider != null;
        enSuelo = hit;

        // coyotetime
        if (hit)
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("Jump",false);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0;
            dobelJump = true;
            jumping = true;
            
        }
        if (!hit)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump",false);
        }

        // Doble salto
        if (!hit && dobelJump && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("funciona");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            dobelJump = false;
            jumping = false;
        }
    }

    public void ataque()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Ataque");
        }
    }

    public void sprint()
    {
        // correr
        if (hit && Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            running = false;
            speed = 3f;
        }
        // limitador de salto y spring. hace que solo se pueda correr en el primer salto, en cambio en el segundo pierde impulso y vuelve a su velocidad 
        if (!hit && jumping && dobelJump && !running)
        {
            speed = sprintSpeed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("colisiono");
            //damage();
        }
    }

   /* public void damage()
    {
        vidaActual -= 1;

        // llamado a la animacion de da�o
        animator.SetTrigger("Damage"); 
    }*/

    public void dead()
    {
        if (vidaActual <= 0)
        {
            
            // Reinicia nivel
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudrayCast);
    }
}
