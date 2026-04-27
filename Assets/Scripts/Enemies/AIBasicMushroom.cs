using System.Collections;
using UnityEngine;

// Script que controla el movimiento de un enemigo entre varios puntos.
// Además ajusta su orientación con flipX y cambia la animación entre Idle y Run.
public class AIBasicMushroon : MonoBehaviour
{
    public Animator animator; // Referencia al Animator para controlar las animaciones.
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer para girar el sprite.

    public float speed = 0.5f; // Velocidad de movimiento del enemigo.

    private float waitTime; // Tiempo actual de espera antes de cambiar de punto.
    public float startWaitTime = 2f; // Tiempo de espera inicial en cada waypoint.

    private int i = 0; // Índice del waypoint actual.
    private Vector2 actualPos; // Guarda la posición anterior para comprobar si se mueve.

    public Transform[] moveSpots; // Array de waypoints por los que se moverá el enemigo.

    void Start()
    {
        // Inicializamos el tiempo de espera.
        waitTime = startWaitTime;

        // Iniciamos una sola vez la corrutina que comprueba si el enemigo se mueve.
        // Antes estaba en Update(), pero eso lanzaba muchas corrutinas por segundo.
        StartCoroutine(CheckEnemyMoving());
    }

    void Update()
    {
        // Si no hay puntos asignados, no hacemos nada para evitar errores.
        if (moveSpots == null || moveSpots.Length == 0)
        {
            return;
        }

        // =========================================
        // MOVIMIENTO DEL ENEMIGO
        // =========================================

        // Mueve el enemigo desde su posición actual hasta el waypoint actual.
        transform.position = Vector2.MoveTowards(
            transform.position,
            moveSpots[i].position,
            speed * Time.deltaTime
        );

        // =========================================
        // COMPROBAR SI LLEGÓ AL WAYPOINT
        // =========================================

        // Si la distancia entre el enemigo y el waypoint es muy pequeña,
        // consideramos que ya ha llegado.
        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.1f)
        {
            // Si terminó el tiempo de espera, pasamos al siguiente waypoint.
            if (waitTime <= 0)
            {
                // Avanzamos al siguiente waypoint.
                i++;

                // Si llegamos al final del array, volvemos al primer waypoint.
                // Esto permite hacer un circuito:
                // Waypoint1 → Waypoint2 → Waypoint3 → Waypoint4 → Waypoint1
                if (i >= moveSpots.Length)
                {
                    i = 0;
                }

                // Reiniciamos el tiempo de espera para el siguiente punto.
                waitTime = startWaitTime;
            }
            else
            {
                // Mientras espera en el waypoint, reducimos el contador.
                waitTime -= Time.deltaTime;
            }
        }
    }

    // =========================================
    // CORRUTINA PARA DETECTAR MOVIMIENTO
    // =========================================

    // Esta corrutina comprueba continuamente si el enemigo se está moviendo.
    // Ahora compara la posición completa X e Y, no solo la X.
    // Así detecta correctamente movimientos verticales entre waypoints.
    IEnumerator CheckEnemyMoving()
    {
        while (true)
        {
            // Guardamos la posición actual del enemigo.
            actualPos = transform.position;

            // Esperamos un pequeño tiempo antes de comparar la nueva posición.
            yield return new WaitForSeconds(0.2f);

            // Guardamos la posición nueva después de esperar.
            Vector2 newPos = transform.position;

            // =========================================
            // COMPROBAR SI EL ENEMIGO SE ESTÁ MOVIENDO
            // =========================================

            // Si la distancia entre la posición antigua y la nueva es mayor que 0.01,
            // significa que el enemigo se ha movido, aunque sea en vertical.
            if (Vector2.Distance(newPos, actualPos) > 0.01f)
            {
                // Si se está moviendo, quitamos Idle.
                // En el Animator, esto debería activar la animación Run.
                animator.SetBool("Idle", false);

                // =========================================
                // GIRAR SPRITE SEGÚN DIRECCIÓN HORIZONTAL
                // =========================================

                // Si se mueve hacia la derecha, giramos el sprite.
                if (newPos.x > actualPos.x)
                {
                    spriteRenderer.flipX = true;
                }
                // Si se mueve hacia la izquierda, giramos el sprite al lado contrario.
                else if (newPos.x < actualPos.x)
                {
                    spriteRenderer.flipX = false;
                }

                // Si solo se mueve en vertical, no cambiamos el flipX.
                // Mantiene la última dirección horizontal.
            }
            else
            {
                // Si no cambió de posición, está parado en un waypoint.
                animator.SetBool("Idle", true);
            }
        }
    }
}