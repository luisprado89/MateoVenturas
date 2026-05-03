using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    // ================= MOVIMIENTO =================
    public float speed = 0.5F; // Velocidad a la que se mueve la plataforma

    // ================= ESPERA =================
    private float waitTime; // Tiempo actual que la plataforma permanece parada en cada punto
    public float startWaitTime = 2; // Tiempo inicial de espera en cada punto

    // ================= CONTROL DE PUNTOS =================
    private int i = 0; // Índice del punto objetivo actual dentro del array

    public Transform[] moveSpots; // Puntos entre los que se moverá la plataforma

    // ================= CONTROL DEL PLAYER =================
    private Transform playerToDetach; // Guardamos aquí el Player para separarlo de la plataforma después

    void Start()
    {
        // Inicializamos el tiempo de espera al valor configurado en el Inspector
        waitTime = startWaitTime;
    }

    void Update()
    {
        // ================= MOVIMIENTO DE LA PLATAFORMA =================

        // Movemos la plataforma hacia el punto actual del array moveSpots
        transform.position = Vector2.MoveTowards(
            transform.position, // Posición actual de la plataforma
            moveSpots[i].position, // Posición del punto objetivo actual
            speed * Time.deltaTime // Velocidad suavizada por tiempo
        );

        // ================= COMPROBAR SI LLEGÓ AL PUNTO =================

        // Comprobamos si la plataforma está cerca del punto objetivo actual
        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.1f)
        {
            // Si el tiempo de espera ya terminó
            if (waitTime <= 0)
            {
                // Si el punto actual NO es el último del array
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++; // Avanzamos al siguiente punto
                }
                else
                {
                    i = 0; // Si era el último, volvemos al primer punto
                }

                // Reiniciamos el tiempo de espera
                waitTime = startWaitTime;
            }
            else
            {
                // Mientras espera en el punto, reducimos el contador
                waitTime -= Time.deltaTime;
            }
        }
    }

    // ================= CUANDO EL PLAYER TOCA LA PLATAFORMA =================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos que el objeto que toca la plataforma es el Player
        if (collision.collider.CompareTag("Player"))
        {
            // Hacemos al Player hijo de la plataforma
            // Así el Player se mueve junto con la plataforma
            collision.collider.transform.SetParent(transform);
        }
    }

    // ================= CUANDO EL PLAYER SALE DE LA PLATAFORMA =================

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Comprobamos que el objeto que deja de tocar la plataforma es el Player
        if (collision.collider.CompareTag("Player"))
        {
            // Guardamos la referencia del Player
            playerToDetach = collision.collider.transform;

            // Ejecutamos DetachPlayer en el siguiente momento posible
            // Esto evita el error cuando Brown Off se está activando/desactivando
            Invoke(nameof(DetachPlayer), 0f);
        }
    }

    // ================= SEPARAR PLAYER DE LA PLATAFORMA =================

    private void DetachPlayer()
    {
        // Comprobamos que el Player existe
        // y que todavía sigue siendo hijo de ESTA plataforma
        if (playerToDetach != null && playerToDetach.parent == transform)
        {
            // Quitamos el parent para que el Player vuelva a moverse de forma independiente
            playerToDetach.SetParent(null);
        }
    }
}