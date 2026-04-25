using UnityEngine;
using UnityEngine.InputSystem; // Necesario para usar el nuevo sistema de entrada de Unity

public class PlayerMove : MonoBehaviour
{
    // ================= MOVIMIENTO =================
    public float runSpeed = 2; // Velocidad de movimiento horizontal del jugador
    // ================= SALTO =================
    public float jumpSpeed = 4; // Velocidad de salto del jugador
    public float doubleJumpSpeed = 3; // Velocidad de salto para el doble salto del jugador
    private bool canDoubleJump; // Variable para controlar si el jugador puede realizar un doble salto
    // ================= FÍSICAS =================
    Rigidbody2D rb2d; // Referencia al componente Rigidbody2D del jugador
    // ================= SALTO MEJORADO =================
    public bool betterJump = false; // Variable para activar o desactivar el salto mejorado
    public float fallMultiplier = 0.5f; // Multiplicador para la caída del jugador
    public float lowJumpMultiplier = 1f; // Multiplicador para el salto bajo del jugador
    // ================= VISUAL =================
    public SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del jugador
    public Animator animator; // Referencia al componente Animator del jugador
    // ================= INPUT SYSTEM =================
    private float moveInput; // Guardamos el movimiento horizontal
    private bool jumpHeld; // Saber si el botón de salto está pulsado

    void Start()
    {   // Obtener la referencia al componente Rigidbody2D del jugador
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        // ================= ANIMACIONES =================

        // Si NO está en el suelo → está saltando
        if (!CheckGround.isGrounded)// Si el jugador no está en el suelo, activar la animación de salto y desactivar la animación de correr
        {
            animator.SetBool("Jump", true);// Activar la animación de salto
            animator.SetBool("Run", false);// Desactivar la animación de correr
        }

        // Si está en el suelo → reset animaciones
        if (CheckGround.isGrounded)// Si el jugador está en el suelo, desactivar las animaciones de salto, doble salto y caída
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }

        // Si cae → animación de caída
        if (rb2d.linearVelocity.y < 0)// Si la velocidad vertical del jugador es menor que 0, activar la animación de caída
        {
            animator.SetBool("Falling", true);// Activar la animación de caída
        }
        else if (rb2d.linearVelocity.y > 0)// Si la velocidad vertical del jugador es mayor que 0, desactivar la animación de caída
        {
            animator.SetBool("Falling", false);// Desactivar la animación de caída
        }

    }

    // Manejar el movimiento horizontal del jugador y las animaciones correspondientes
    void FixedUpdate()
    {
        // ================= MOVIMIENTO =================

        // Aplicamos movimiento horizontal
        rb2d.linearVelocity = new Vector2(moveInput * runSpeed, rb2d.linearVelocity.y);

        // Girar sprite según dirección
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // ================= SALTO MEJORADO =================

        if (betterJump)
        {
            // Caída más rápida
            if (rb2d.linearVelocity.y < 0)
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            }
            // Salto más corto si sueltas el botón
            else if (rb2d.linearVelocity.y > 0 && !jumpHeld)
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            }
        }
    }
    // ================= INPUT SYSTEM =================
    // Este método se ejecuta cuando mueves el joystick/teclas
    public void OnMove(InputAction.CallbackContext context)
    {
        // Leemos el valor (Vector2) y nos quedamos con X (horizontal)
        moveInput = context.ReadValue<Vector2>().x;
    }

    // Este método se ejecuta cuando pulsas salto
    public void OnJump(InputAction.CallbackContext context)
    {
        // Cuando empiezas a pulsar
        if (context.started)
        {
            jumpHeld = true;

            // Si está en el suelo → salto normal
            if (CheckGround.isGrounded)
            {
                canDoubleJump = true;
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
            }
            // Si está en el aire → posible doble salto
            else if (canDoubleJump)
            {
                animator.SetBool("DoubleJump", true);
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, doubleJumpSpeed);
                canDoubleJump = false;
            }
        }

        // Cuando sueltas el botón (para salto mejorado)
        if (context.canceled)
        {
            jumpHeld = false;
        }
    }
}
