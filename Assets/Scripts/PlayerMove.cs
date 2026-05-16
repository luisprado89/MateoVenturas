using UnityEngine;
using UnityEngine.InputSystem; // Necesario para usar el nuevo sistema de entrada de Unity

public class PlayerMove : MonoBehaviour
{
    // ================= MOVIMIENTO =================
    public float runSpeed = 2; // Velocidad de movimiento horizontal del jugador

    // ================= SALTO =================
    public float jumpSpeed = 4; // Velocidad de salto del jugador
    public float doubleJumpSpeed = 4; // Velocidad de salto para el doble salto del jugador
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
    private float moveInput; // Guardamos el movimiento horizontal recibido desde teclado, mando o joystick
    private bool jumpHeld; // Guarda si el botón de salto sigue pulsado

    void Start()
    {
        // Obtener la referencia al componente Rigidbody2D del jugador
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ================= ANIMACIONES =================

        // Si el jugador está tocando el suelo
        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false); // Desactivar animación de salto
            animator.SetBool("DoubleJump", false); // Desactivar animación de doble salto
            animator.SetBool("Falling", false); // Desactivar animación de caída
        }
        // Si el jugador NO está tocando el suelo
        else
        {
            // Si el jugador está subiendo, activamos Jump
            if (rb2d.linearVelocity.y > 0f)
            {
                animator.SetBool("Jump", true); // Activar animación de salto
                animator.SetBool("Falling", false); // Desactivar animación de caída
            }
            // Si el jugador no está subiendo, entonces está cayendo
            else
            {
                animator.SetBool("Jump", false); // Desactivar animación de salto
                animator.SetBool("Falling", true); // Activar animación de caída
            }

            // Mientras está en el aire, no debe reproducir Run
            animator.SetBool("Run", false);
        }
    }

    void FixedUpdate()
    {
        // ================= MOVIMIENTO =================

        // Aplicamos movimiento horizontal manteniendo la velocidad vertical actual
        rb2d.linearVelocity = new Vector2(moveInput * runSpeed, rb2d.linearVelocity.y);

        // ================= GIRO DEL SPRITE Y ANIMACIÓN RUN =================

        // Si el jugador se mueve hacia la derecha
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // Mirar hacia la derecha

            // Solo activar Run si el jugador está tocando el suelo
            animator.SetBool("Run", CheckGround.isGrounded);
        }
        // Si el jugador se mueve hacia la izquierda
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // Mirar hacia la izquierda

            // Solo activar Run si el jugador está tocando el suelo
            animator.SetBool("Run", CheckGround.isGrounded);
        }
        // Si no hay movimiento horizontal
        else
        {
            animator.SetBool("Run", false); // Desactivar animación de correr
        }

        // ================= SALTO MEJORADO =================

        if (betterJump)
        {
            // Si el jugador está cayendo, modificamos la velocidad para controlar la caída
            if (rb2d.linearVelocity.y < 0)
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            }
            // Si el jugador está subiendo y suelta el botón de salto, hacemos el salto más corto
            else if (rb2d.linearVelocity.y > 0 && !jumpHeld)
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            }
        }
    }

    // ================= INPUT SYSTEM =================

    public void OnMove(InputAction.CallbackContext context)
    {
        // Leemos el valor Vector2 del movimiento y nos quedamos solo con el eje X
        moveInput = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Cuando empieza a pulsarse el botón de salto
        if (context.started)
        {
            jumpHeld = true; // Guardamos que el botón de salto está pulsado

            // Si el jugador está en el suelo, realiza salto normal
            if (CheckGround.isGrounded)
            {
                canDoubleJump = true; // Permitimos un doble salto después del salto normal
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed); // Aplicamos fuerza vertical de salto
            }
            // Si el jugador está en el aire y todavía puede hacer doble salto
            else if (canDoubleJump)
            {
                animator.SetBool("DoubleJump", true); // Activar animación de doble salto
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, doubleJumpSpeed); // Aplicar velocidad del doble salto
                canDoubleJump = false; // Consumimos el doble salto
            }
        }

        // Cuando se suelta el botón de salto
        if (context.canceled)
        {
            jumpHeld = false; // Guardamos que el botón de salto ya no está pulsado
        }
    }
}