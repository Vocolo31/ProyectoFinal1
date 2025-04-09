
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class playerMove : MonoBehaviour
{
    public Rigidbody rb;
    public float Speed = 3f;
    public float jumpForce;
    public float coyoteTime = 0.2f; // Tiempo de gracia para saltar después de caer
    private float coyoteTimeCounter; // Contador para coyote time

    public bool enSuelo; // indica si el jugador esta en el suelo
    public float longitudrayCast = 1f; // medida del largo de la linea que detecta el suelo
    public LayerMask capaSuelo; // filtro que identifica si es suelo o no
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //llamado
        Move();
        jump();
    }

    private void Move()
    {
        // controles del movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");

        //aplicar el control
        Vector3 velocidadActual = rb.velocity;

        // Aplicar contol al rigidbody
        rb.velocity = new Vector3(inputMovimiento * Speed, velocidadActual.y, velocidadActual.z);
    }
    public void jump()
    {
        // Raycast hacia el suelo 
        hit = Physics.Raycast(transform.position, Vector3.down, longitudrayCast, capaSuelo);
         // si hay colisión, está en el suelo

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
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0); // Se usa velocity en vez de AddForce para mayor control
            coyoteTimeCounter = 0; // Se gasta el Coyote Time al saltar
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
