using UnityEngine;

public class OrbitaCamara : MonoBehaviour
{
    public Transform target;
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;
    public float zoomSpeed = 10.0f;
    public float rotationSensitivity = 3.0f;
    public LayerMask capaColision;

    private float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 15.0f;

    public float minY = -10f;
    public float maxY = 80f;

    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * rotationSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * rotationSensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 posicionDeseada = target.position + rotation * direction;

        RaycastHit hit;
        if (Physics.Linecast(target.position, posicionDeseada, out hit, capaColision))
        {
            transform.position = hit.point + hit.normal * 0.2f;
        }
        else
        {
            transform.position = posicionDeseada;
        }

        transform.LookAt(target.position);
    }
}