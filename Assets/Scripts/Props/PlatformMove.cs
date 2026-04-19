using UnityEngine;

public class PlatfromMove : MonoBehaviour
{
    public float speed = 0.5F; // Velocidad a la que se mueve la plataforma

    private float waitTime; // Tiempo actual que la plataforma permanece parada en cada punto
    public float startWaitTime = 2; // Tiempo inicial de espera en cada punto

    private int i = 0; // Índice del punto objetivo actual dentro del array

    public Transform[] moveSpots; // Puntos entre los que se moverá la plataforma

    void Start()
    {
        // Inicializamos el tiempo de espera al valor configurado
        waitTime = startWaitTime;
    }

    void Update()
    {
        // =========================================
        // MOVIMIENTO DE LA PLATAFORMA
        // =========================================
        // Movemos la plataforma hacia el punto actual del array
        // MoveTowards permite un movimiento progresivo y suave
        transform.position = Vector2.MoveTowards(
            transform.position,
            moveSpots[i].position,
            speed * Time.deltaTime
        );

        // =========================================
        // COMPROBAR SI HA LLEGADO AL PUNTO
        // =========================================
        // Si la plataforma está lo suficientemente cerca del punto destino
        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.1f)
        {
            // Si ya ha terminado el tiempo de espera
            if (waitTime <= 0)
            {
                // Si NO estamos en el último punto → avanzar al siguiente
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    // Si estamos en el último punto → volver al primero (bucle)
                    i = 0;
                }

                // Reiniciamos el tiempo de espera para el siguiente punto
                waitTime = startWaitTime;
            }
            else
            {
                // Mientras está parada, reducimos el tiempo de espera
                waitTime -= Time.deltaTime;
            }
        }
    }

    // =========================================
    // DETECCIÓN DE COLISIÓN CON EL PLAYER
    // =========================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cuando el jugador toca la plataforma:
        // Lo convertimos en hijo de la plataforma
        // → Así se moverá junto a ella automáticamente
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Cuando el jugador deja de tocar la plataforma:
        // Quitamos la relación padre-hijo
        // → Para que el jugador vuelva a moverse de forma independiente
        collision.collider.transform.SetParent(null);
    }
}