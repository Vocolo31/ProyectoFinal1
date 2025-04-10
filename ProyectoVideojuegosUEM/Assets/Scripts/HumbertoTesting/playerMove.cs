
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class playerMove : MonoBehaviour
{
    [Header("Controlador del player")]
    private Rigidbody rb; // fisicas del personaje
    public float speed = 3f; //velocidad del personaje
    public float jumpForce; // fuerza de salto
    public float sprintSpeed = 5f; //velocidad de spritn

    [Header("Dash Settings")]
    [SerializeField] private float dashinTime;
    [SerializeField] private float dashForce;
    [SerializeField] private float timeCanDash;
    bool dashing;
    bool canDash;


    [Header("Settings")]
    public float coyoteTime = 0.2f; // Tiempo de gracia para saltar después de caer
    private float coyoteTimeCounter; // Contador para coyote time
    public bool enSuelo; // indica si el jugador esta en el suelo
    public float longitudrayCast = 1f; // medida del largo de la linea que detecta el suelo
    public LayerMask capaSuelo; // filtro que identifica si es suelo o no
    public bool hit; // registra si el rayCast colisiona
    bool dobelJump = false; // doble salto
    bool running = true ; // chequea si esta corriendo
    bool jumping; // chequea si esta saltando
    Animator animator; // Controlador de animacion
    float direction;
    private DashPlayer dash;
    public float Directtion => direction;

    // Start is called before the first frame update
    void Start()
    {
        // chequeo de componentes
        animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody>();
        dash = GetComponent<DashPlayer>();

        // controles del movimiento
        direction = Input.GetAxis("Horizontal");

    }


    // Update is called once per frame
    void Update()
    {
        //llamado
        Move();
        sprint();
        if (!dash.Dashing)
        {
            jump();
        }
    }

    

    private void Move()
    {
        direction = Input.GetAxis("Horizontal");

        //aplicar el control
        Vector3 velocidadActual = rb.velocity;

        // Aplicar contol al rigidbody
        rb.velocity = new Vector3(direction * speed, velocidadActual.y, velocidadActual.z);
    }
    public void jump()
    {
        // Raycast hacia el suelo 
        hit = Physics.Raycast(transform.position, Vector3.down, longitudrayCast, capaSuelo);
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
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0); // para mayor control se usa velocity en vez de AddForce
            coyoteTimeCounter = 0; // Se gasta el Coyote Time al saltar
            dobelJump = true;
            jumping = true;

        }

       

        // Doble salto
        if (!hit && dobelJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("funciona");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            dobelJump = false;
            jumping = false;
        }

        

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


    public void OnDrawGizmos()
    {
        //color del raycast
        Gizmos.color = Color.red;

        // posicion del raycast y direccion del mismo
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudrayCast);
    }
}
