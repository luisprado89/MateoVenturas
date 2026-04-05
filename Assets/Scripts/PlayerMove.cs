using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 2; // Velocidad de movimiento horizontal del jugador
    public float jumpSpeed = 4; // Velocidad de salto del jugador

    public float doubleJumpSpeed = 3; // Velocidad de salto para el doble salto del jugador

    private bool canDoubleJump; // Variable para controlar si el jugador puede realizar un doble salto
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
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if (CheckGround.isGrounded == true) // Verificar si el jugador está en el suelo para permitir el salto
            {
                canDoubleJump = true; // Permitir el doble salto después de realizar un salto desde el suelo
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed); // Hacer que el jugador salte
            }
            else // Verificar si el jugador puede realizar un doble salto
            {
                if (Input.GetKeyDown("space")) // Verificar si se presiona la tecla de salto nuevamente y si el jugador puede realizar un doble salto
                {
                    if (canDoubleJump)
                    {
                        animator.SetBool("DoubleJump", true); // Activar la animación de doble salto al realizar un doble salto
                        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, doubleJumpSpeed); // Hacer que el jugador realice un doble salto
                        canDoubleJump = false; // Desactivar la posibilidad de realizar otro doble salto hasta que el jugador vuelva a tocar el suelo

                    }
                }
            }
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
            animator.SetBool("DoubleJump", false); // Desactivar la animación de doble salto cuando el jugador está en el suelo
            animator.SetBool("Falling", false); // Desactivar la animación de caída cuando el jugador está en el suelo
        }
        if (rb2d.linearVelocity.y < 0)
        {
            animator.SetBool("Falling", true); // Activar la animación de caída cuando el jugador está cayendo
        }
        else if (rb2d.linearVelocity.y > 0)
        {
            animator.SetBool("Falling", false); // Desactivar la animación de caída cuando el jugador no está cayendo   
        }

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
