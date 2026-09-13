using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 6f;
    public float fuerzaSalto = 7f;
    public float velocidadRotacion = 10f;
    public Transform camaraTransform; // Arrastra tu Main Camera aquí desde el Inspector

    private Rigidbody rb;
    private bool enPiso = true;

    void Start()
    {
        // El script busca automáticamente el Rigidbody de tu cápsula
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // --- 1. MOVIMIENTO RELATIVO A LA CÁMARA ---
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        // Calculamos hacia dónde mira la cámara, ignorando la altura (Y) para no volar
        Vector3 adelante = camaraTransform.forward;
        Vector3 derecha = camaraTransform.right;
        adelante.y = 0f;
        derecha.y = 0f;
        adelante.Normalize();
        derecha.Normalize();

        // Creamos la dirección final combinando las teclas y la vista de la cámara
        Vector3 direccionMovimiento = adelante * movimientoZ + derecha * movimientoX;

        // Movemos al personaje sin alterar su velocidad en Y (para que la gravedad funcione)
        rb.linearVelocity = new Vector3(direccionMovimiento.x * velocidad, rb.linearVelocity.y, direccionMovimiento.z * velocidad);

        // Hacemos que el modelo del personaje gire hacia donde está caminando
        if (direccionMovimiento.magnitude > 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
        }

        // --- 2. LÓGICA DE SALTO ---
        // Salta solo si presionas Espacio y la variable enPiso es verdadera
        if (Input.GetButtonDown("Jump") && enPiso)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enPiso = false;
        }
    }

    // --- 3. DETECCIÓN DEL PISO (FÍSICAS 3D) ---
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que acaba de tocar se llama exactamente "Piso"
        if (collision.gameObject.name == "Piso")
        {
            enPiso = true;
        }
    }
}