using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Controlador del player")]
    private Rigidbody2D rb; // fisicas del personaje
    public float speed = 3f; //velocidad del personaje
    public float jumpForce; // fuerza de salto
    public float sprintSpeed = 5f; //velocidad de spritn

    [Header("Vida")]
    public int vidaMax= 3;
    public int vidaMin = 3;


    [Header("Settings")]
    public float coyoteTime = 0.2f; // Tiempo de gracia para saltar después de caer
    public Transform position;
    private float coyoteTimeCounter; // Contador para coyote time
    public bool enSuelo; // indica si el jugador esta en el suelo
    public float longitudrayCast = 2f; // medida del largo de la linea que detecta el suelo
    public LayerMask capaSuelo; // filtro que identifica si es suelo o no
    public bool hit; // registra si el rayCast colisiona
    bool dobelJump = false; // doble salto
    bool running = true; // chequea si esta corriendo
    bool jumping; // chequea si esta saltando
    Animator animator; // Controlador de animacion
    float direction;
    private Dash dash;
    public float Directtion => direction;

    // Start is called before the first frame update
    void Start()
    {
        // chequeo de componentes
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();
        position = GetComponent<Transform>();

        // controles del movimiento
        direction = Input.GetAxis("Horizontal");

    }


    // Update is called once per frame
    void Update()
    {
        //llamado
        Move();
        ataque();
        dead();

        if (!dash.Dashing)
        {
            jump();
        }
    }
    private void Move()
    {
        direction = Input.GetAxisRaw("Horizontal");

        //aplicar el control
        Vector2 velocidadActual = rb.velocity;

        // Aplicar contol al rigidbody
        Vector2 input = new Vector2(direction, velocidadActual.y);

        if (input.magnitude > 0)
        {
            rb.velocity = input * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudrayCast, capaSuelo);
        enSuelo = hit.collider != null; // si hay colisión, está en el suelo

        // Si está en el suelo, resetear el contador de Coyote Time
        if (hit)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Si presiona "SPACE" y el Coyote Time aún no se ha agotado, puede saltar
        if (coyoteTimeCounter > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // para mayor control se usa velocity en vez de AddForce
            coyoteTimeCounter = 0; // Se gasta el Coyote Time al saltar
            dobelJump = true;
            jumping = true;

        }



        // Doble salto
        if (!hit && dobelJump == true && Input.GetKeyDown(KeyCode.Space))
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("colisiono");
            damage();
        }
    }

    public void damage()
    {
        vidaMin -= 1;
    }

    public void sprint()
    {
        // controlador de correr
        if (hit == true && Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;

        }
        else
        {
            running = false;
            speed = 3f;
        }
        if (!hit && jumping == true && dobelJump == true && !running)
        {
            speed = sprintSpeed;
        }
    }
    public void dead()
    {
        if (vidaMin<= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        //color del raycast
        Gizmos.color = Color.red;

        // posicion del raycast y direccion del mismo
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudrayCast);
    }

}
