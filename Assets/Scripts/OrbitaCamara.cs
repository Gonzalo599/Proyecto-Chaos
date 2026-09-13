using UnityEngine;

public class OrbitaCamara : MonoBehaviour
{
    public Transform target; // Arrastra a tu jugador aquí
    public float minDistance = 2.0f;
    public float maxDistance = 10.0f;
    public float zoomSpeed = 10.0f;
    public float rotationSensitivity = 3.0f;

    private float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 15.0f; // Ángulo inicial

    // Límites para que la cámara no dé la vuelta por debajo del suelo o por encima de la cabeza
    public float minY = -10f;
    public float maxY = 80f;

    void Update()
    {
        // 1. Rotación con el ratón
        currentX += Input.GetAxis("Mouse X") * rotationSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * rotationSensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // 2. Zoom con la rueda del ratón
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcular la nueva posición de la cámara basada en la rotación y la distancia
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        transform.position = target.position + rotation * direction;
        transform.LookAt(target.position); // Mantener la mirada en el personaje
    }
}