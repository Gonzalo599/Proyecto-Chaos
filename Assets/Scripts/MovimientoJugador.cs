using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 6f;
    public float fuerzaSalto = 7f;
    public float velocidadRotacion = 10f;
    public float multiplicadorCaida = 2.5f;
    public Transform camaraTransform;

    public bool puedeMoverse = true;

    private Rigidbody rb;
    private bool enPiso = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        Vector3 adelante = camaraTransform.forward;
        Vector3 derecha = camaraTransform.right;
        adelante.y = 0f;
        derecha.y = 0f;
        adelante.Normalize();
        derecha.Normalize();

        Vector3 direccionMovimiento = adelante * movimientoZ + derecha * movimientoX;

        rb.linearVelocity = new Vector3(direccionMovimiento.x * velocidad, rb.linearVelocity.y, direccionMovimiento.z * velocidad);

        if (direccionMovimiento.magnitude > 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
        }

        enPiso = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (Input.GetButtonDown("Jump") && enPiso)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (multiplicadorCaida - 1) * Time.deltaTime;
        }
    }
}