using UnityEngine;

public class EnemySpikeHead : MonoBehaviour
{
    // Velocidad de movimiento
    public float speed = 2f;

    // Array de puntos (igual que Move Spots en el Mushroom)
    public Transform[] moveSpots;

    // Índice del punto actual
    private int currentSpot = 0;

    void Update()
    {
        // Si no hay puntos, no hacemos nada
        if (moveSpots.Length == 0) return;

        // Movimiento hacia el punto actual
        transform.position = Vector2.MoveTowards(
            transform.position,
            moveSpots[currentSpot].position,
            speed * Time.deltaTime
        );

        // Si llega al punto, pasa al siguiente
        if (Vector2.Distance(transform.position, moveSpots[currentSpot].position) < 0.05f)
        {
            currentSpot++;

            // Si llega al final, vuelve al primero (loop)
            if (currentSpot >= moveSpots.Length)
            {
                currentSpot = 0;
            }
        }
    }

    // Daño al jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player hit by damage object!");
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged();
        }
    }
}