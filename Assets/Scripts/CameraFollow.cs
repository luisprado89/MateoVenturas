using UnityEngine; // Importa las herramientas principales de Unity

public class CameraFollow : MonoBehaviour // Script para que la cámara siga al jugador sin salirse del escenario
{
    public Transform player; // Referencia al jugador que la cámara va a seguir

    public Vector3 offset = new Vector3(0f, 0f, -10f); // Distancia entre la cámara y el jugador

    public float smoothSpeed = 0.125f; // Suavidad del movimiento de la cámara

    public float minX = -29.09f; // Límite izquierdo calculado: -29.09 + 2.66

    public float maxX = 81.57f; // Límite derecho calculado: 81.57 - 2.66

    public float minY = -25.6f; // Límite inferior calculado: -25.6 + 1.5

    public float maxY = 29.28f; // Límite superior calculado: 29.28 - 1.5

    private void LateUpdate() // Se ejecuta después del movimiento del jugador
    {
        if (player != null) // Comprueba que el jugador esté asignado
        {
            Vector3 desiredPosition = player.position + offset; // Calcula la posición deseada de la cámara

            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX); // Limita la cámara en X

            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY); // Limita la cámara en Y

            Vector3 clampedPosition = new Vector3(clampedX, clampedY, offset.z); // Mantiene la cámara en Z -10

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed); // Suaviza el movimiento

            transform.position = smoothedPosition; // Aplica la posición final
        }
    }
}