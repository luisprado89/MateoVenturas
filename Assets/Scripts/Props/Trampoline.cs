using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator para controlar la animación de la trampolín
    public float jumpForce = 2f; // Fuerza con la que el jugador será impulsado hacia arriba

    // Método que se ejecuta cuando otro objeto colisiona con la trampolín
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisionó tiene la etiqueta "Player"
        if (collision.transform.CompareTag("Player"))
        {
            // Obtiene el Rigidbody2D del jugador y aplica una fuerza hacia arriba
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (Vector2.up * jumpForce);

            // Reproduce la animación de la trampolín al ser activada
            animator.Play("JumpTrampoline");
        }
    }
}
