using UnityEngine;

public class PlayerMoveJoystick : MonoBehaviour
{
    private float horizontalMove = 0f; // Movimiento horizontal basado en la entrada del joystick
    // private float verticalMove = 0f; // Comentado porque no se utiliza en este script

    public Joystick joystick; // Referencia al joystick virtual

    public float runSpeedHorizontal = 2; // Velocidad horizontal del jugador
    public float runSpeed = 1.2f; // Multiplicador de velocidad para el movimiento horizontal

    public float jumpSpeed = 3; // Velocidad del salto inicial
    public float doubleJumpSpeed = 2.5f; // Velocidad del doble salto, menor que el primer salto
    private bool canDoubleJump; // Indica si el jugador puede realizar un doble salto
    Rigidbody2D rb2D; // Referencia al componente Rigidbody2D del jugador

    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer para voltear el sprite
    public Animator animator; // Referencia al Animator para controlar las animaciones

    // Propiedad para obtener el valor del eje vertical del joystick
    public float Vertical
    {
        get { return joystick.Vertical; } // Devuelve el valor del eje vertical del joystick
    }
    void Start()
    {
        // Obtener el componente Rigidbody2D del jugador
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Manejar la dirección del sprite y las animaciones de correr
        if (horizontalMove > 0) // Movimiento hacia la derecha
        {
            spriteRenderer.flipX = false; // Asegurar que el sprite no esté volteado
            animator.SetBool("Run", true); // Activar la animación de correr
        }
        else if (horizontalMove < 0) // Movimiento hacia la izquierda
        {
            spriteRenderer.flipX = true; // Voltear el sprite hacia la izquierda
            animator.SetBool("Run", true); // Activar la animación de correr
        }
        else
        {
            animator.SetBool("Run", false); // Desactivar la animación de correr
        }

        // Manejar las animaciones de salto y caída
        if (!CheckGround.isGrounded)
        {
            animator.SetBool("Jump", true); // Activar la animación de salto
            animator.SetBool("Run", false); // Desactivar la animación de correr
        }
        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false); // Desactivar la animación de salto
            animator.SetBool("DoubleJump", false); // Desactivar la animación de doble salto
            animator.SetBool("Falling", false); // Desactivar la animación de caída
        }
        if (rb2D.linearVelocity.y < 0)
        {
            animator.SetBool("Falling", true); // Activar la animación de caída
        }
        else if (rb2D.linearVelocity.y > 0)
        {
            animator.SetBool("Falling", false); // Desactivar la animación de caída
        }
    }

    // Manejar el movimiento horizontal basado en la entrada del joystick
    void FixedUpdate()
    {
        horizontalMove = joystick.Horizontal * runSpeedHorizontal; // Obtener el movimiento horizontal del joystick
        rb2D.linearVelocity = new Vector2(horizontalMove * runSpeed, rb2D.linearVelocity.y); // Aplicar el movimiento horizontal
    }

    // Manejar el salto y el doble salto
    public void JumpButton()
    {
        if (CheckGround.isGrounded)
        {
            canDoubleJump = true; // Permitir el doble salto si el jugador está en el suelo
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed); // Realizar el salto inicial
        }
        else
        {
            if (canDoubleJump)
            {
                animator.SetBool("DoubleJump", true); // Activar la animación de doble salto
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, doubleJumpSpeed); // Realizar el doble salto
                canDoubleJump = false; // Desactivar la posibilidad de realizar otro doble salto
            }
        }
    }

}