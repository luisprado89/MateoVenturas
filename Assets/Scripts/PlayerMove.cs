using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 2; // Velocidad de movimiento horizontal del jugador
    public float jumpSpeed = 4; // Velocidad de salto del jugador
    Rigidbody2D rb2d; // Referencia al componente Rigidbody2D del jugador
    public bool betterJump = false; // Variable para activar o desactivar el salto mejorado
    public float fallMultiplier = 0.5f; // Multiplicador para la caída del jugador
    public float lowJumpMultiplier = 1f; // Multiplicador para el salto bajo del jugador

    public SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del jugador

    public Animator animator; // Referencia al componente Animator del jugador
    void Start()
    {   // Obtener la referencia al componente Rigidbody2D del jugador
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Manejar el movimiento horizontal del jugador y las animaciones correspondientes
    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.linearVelocity = new Vector2(runSpeed, rb2d.linearVelocity.y);// Mover el jugador hacia la derecha
            spriteRenderer.flipX = false; // Asegurarse de que el sprite del jugador no esté volteado
            animator.SetBool("Run", true); // Activar la animación de correr  

        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.linearVelocity = new Vector2(-runSpeed, rb2d.linearVelocity.y);
            spriteRenderer.flipX = true; // Voltear el sprite del jugador para que mire hacia la izquierda
            animator.SetBool("Run", true); // Activar la animación de correr  
        }
        else
        {
            rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
            animator.SetBool("Run", false); // Desactivar la animación de correr cuando el jugador no se mueve horizontalmente
        }
        if (Input.GetKey("space") && CheckGround.isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed); // Hacer que el jugador salte
        }
        if (CheckGround.isGrounded == false)
        {
            animator.SetBool("Jump", true); // Activar la animación de salto cuando el jugador está en el aire
            animator.SetBool("Run", false); // Desactivar la animación de correr cuando el jugador está en el aire
        }
        if (CheckGround.isGrounded == true)
        {
            animator.SetBool("Jump", false); // Desactivar la animación de salto cuando el jugador está en el suelo
        }

        if (betterJump)
        {
            if (rb2d.linearVelocity.y < 0)
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime; // Aplicar el multiplicador de caída para hacer que el jugador caiga más rápido
            }
            else if (rb2d.linearVelocity.y > 0 && !Input.GetKey("space"))
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime; // Aplicar el multiplicador de salto bajo para hacer que el jugador salte más bajo si no se mantiene presionada la tecla de salto
            }
        }


    }
}
