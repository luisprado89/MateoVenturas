using System.Collections;
using UnityEngine;

// Script que controla el movimiento de un enemigo entre varios puntos
// y además ajusta su orientación (flip) y animación según si se está moviendo o no
public class AIBasicMushroon : MonoBehaviour
{
    public Animator animator; // Referencia al Animator para controlar las animaciones (Idle, Run, etc.)
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer para girar el sprite (flipX)

    public float speed = 0.5F; // Velocidad de movimiento del enemigo

    private float waitTime; // Tiempo actual de espera antes de cambiar de punto
    public float startWaitTime = 2; // Tiempo de espera inicial en cada punto

    private int i = 0; // Índice del punto de destino actual
    private Vector2 actualPos; // Guarda la posición anterior del enemigo para comparar movimiento

    public Transform[] moveSpots; // Array de puntos entre los que se mueve el enemigo

    void Start()
    {
        // Inicializamos el tiempo de espera
        waitTime = startWaitTime;
    }

    // Update se ejecuta en cada frame
    void Update()
    {
        // Lanza una corrutina para comprobar si el enemigo se está moviendo o no
        // (esto se usa para girar el sprite y cambiar animaciones)
        StartCoroutine(CheckEnemyMoving());

        // =========================================
        // MOVIMIENTO DEL ENEMIGO
        // =========================================
        // Mover el enemigo hacia el punto actual usando MoveTowards
        transform.position = Vector2.MoveTowards(
            transform.position,
            moveSpots[i].transform.position,
            speed * Time.deltaTime
        );

        // =========================================
        // COMPROBAR SI LLEGÓ AL PUNTO
        // =========================================
        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {
            // Si ya pasó el tiempo de espera, cambiar de punto
            if (waitTime <= 0)
            {
                // Si no es el último punto, avanzar al siguiente
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    // Si es el último punto, volver al primero
                    i = 0;
                }

                // Reiniciar el tiempo de espera
                waitTime = startWaitTime;
            }
            else
            {
                // Reducir el tiempo de espera mientras está parado
                waitTime -= Time.deltaTime;
            }
        }
    }

    // =========================================
    // CORRUTINA PARA DETECTAR MOVIMIENTO
    // =========================================
    // Esta corrutina comprueba si el enemigo se está moviendo comparando su posición
    // actual con la posición después de un pequeño tiempo (0.5 segundos)
    IEnumerator CheckEnemyMoving()
    {
        // Guardar la posición actual
        actualPos = transform.position;

        // Esperar medio segundo
        yield return new WaitForSeconds(0.5f);

        // =========================================
        // COMPROBAR DIRECCIÓN DE MOVIMIENTO
        // =========================================

        // Si la posición actual es mayor que la anterior → se mueve a la derecha
        if (transform.position.x > actualPos.x)
        {
            spriteRenderer.flipX = true; // Voltear sprite (depende del sprite base)
            animator.SetBool("Idle", false); // No está en Idle porque se está moviendo
        }
        // Si la posición actual es menor → se mueve a la izquierda
        else if (transform.position.x < actualPos.x)
        {
            spriteRenderer.flipX = false; // Voltear sprite en el otro sentido
            animator.SetBool("Idle", false); // No está en Idle
        }
        // Si la posición no cambió → está parado
        else if (transform.position.x == actualPos.x)
        {
            animator.SetBool("Idle", true); // Activar animación Idle
        }
    }
}