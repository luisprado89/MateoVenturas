using UnityEngine;

public class AIBasicMushroom : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator para controlar las animaciones del enemigo
    public SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer para voltear el sprite del enemigo
    public float speed = 0.5f; // Velocidad de movimiento del enemigo
    private float waitTime; // Tiempo de espera antes de cambiar de dirección
    public float startWaitTime = 2f; // Tiempo de espera inicial antes de cambiar de dirección
    private int i = 0; // Variable para controlar la dirección del movimiento del enemigo (0: derecha, 1: izquierda)
    private Vector2 actualPos; // Variable para almacenar la posición actual del enemigo
    public Transform[] moveSpots; // Array de puntos de movimiento para el enemigo

    void Start()
    {
        waitTime = startWaitTime;
        animator.SetBool("Run", true); // Activar la animación de correr al iniciar el juego
        animator.SetBool("Idle", false); // Desactivar la animación de idle al iniciar el juego

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].position, speed * Time.deltaTime); // Mover el enemigo hacia el punto de movimiento actual
        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.2f) // Verificar si el enemigo ha llegado al punto de movimiento actual
        {
            //Mientras espera en el punto -> Idle
            animator.SetBool("Run", false); // Desactivar la animación de correr mientras el enemigo espera en el punto de movimiento
            animator.SetBool("Idle", true); // Activar la animación de idle mientras el enemigo espera en el punto de movimiento
            if (waitTime <= 0) // Verificar si el tiempo de espera ha terminado para cambiar de dirección
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1]) // Si la dirección actual es hacia la derecha, cambiar a la izquierda
                {
                    i++; // Incrementar el índice para cambiar al siguiente punto de movimiento
                    spriteRenderer.flipX = true; // Voltear el sprite del enemigo hacia la izquierda


                }
                else // Si la dirección actual es hacia la izquierda, cambiar a la derecha
                {
                    i = 0;
                    spriteRenderer.flipX = false; // Voltear el sprite del enemigo hacia la derecha

                }
                //Al volver a moverse -> Run
                animator.SetBool("Run", true); // Activar la animación de correr al cambiar de dirección
                animator.SetBool("Idle", false); // Desactivar la animación de idle al cambiar de dirección
                waitTime = startWaitTime; // Reiniciar el tiempo de espera para el próximo cambio de dirección
            }
            else
            {
                waitTime -= Time.deltaTime; // Reducir el tiempo de espera mientras el enemigo está en el punto de movimiento
            }

        }
        else
        {
            //Si todavía no ha llegado al punto -> Run
            animator.SetBool("Run", true); // Activar la animación de correr mientras el enemigo se mueve hacia el punto de movimiento
            animator.SetBool("Idle", false); // Desactivar la animación de idle mientras el enemigo se mueve hacia el punto de movimiento
        }
    }
}
