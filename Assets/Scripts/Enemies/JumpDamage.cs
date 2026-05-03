using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    public Collider2D enemyCollider2D; // Referencia al collider principal del enemigo (el que usamos para detectar pisotón)
    public Animator animator; // Referencia al Animator para reproducir animaciones (Hit, etc.)
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer para ocultar el enemigo al morir
    public GameObject destroyParticle; // Objeto de partículas que ya está dentro del enemigo (NO se instancia)
    public float jumpForce = 2.5f; // Fuerza de rebote que recibe el jugador al pisar al enemigo
    public int lifes = 2; // Número de vidas del enemigo

    private bool isDead = false; // Variable para evitar que la muerte se ejecute varias veces (bug de partículas en bucle)

    private void OnTriggerEnter2D(Collider2D collision)// Este método se activa cuando el jugador pisa al enemigo (trigger del collider de daño)
    {
        if (isDead) return;// Si el enemigo ya está muerto, no hacemos nada (evita bugs de partículas en bucle)

        if (collision.CompareTag("Player"))// Comprobamos que el objeto que entra en el trigger es el Player
        {
            PlayerPowerUp powerUp = collision.GetComponent<PlayerPowerUp>();// Buscamos si el Player tiene el script PlayerPowerUp para comprobar si tiene el power-up activo

            if (powerUp != null && powerUp.powerUpActive)// Si el Player tiene el power-up activo, matamos al enemigo usando su propio sistema (sin rebote ni daño al jugador)
            {
                KillByPowerUp();// Llamamos al método de muerte directa del enemigo
                return;// Salimos para que el jugador NO reciba daño ni rebote
            }

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();// Obtenemos el Rigidbody2D del Player para comprobar si está cayendo (solo así cuenta como pisotón)

            // Solo cuenta como pisotón si el jugador está cayendo
            if (playerRb != null && playerRb.linearVelocity.y < 0)// Si el jugador está cayendo (velocity.y < 0), entonces es un pisotón válido
            {
                // Rebote hacia arriba
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);

                // Quitamos una vida al enemigo
                LosseLifeAndHit();

                // Comprobamos si debe morir
                CheckLife();
            }
        }
    }

    public void LosseLifeAndHit()
    {
        if (isDead) return; // Si ya está muerto, no hacemos nada

        lifes--; // Restamos una vida

        if (animator != null) // Comprobamos que hay animator asignado
        {
            animator.Play("Hit"); // Reproducimos animación de daño
        }
    }

    public void KillByPowerUp()
    {
        if (isDead) return; // Evitamos ejecutar varias veces

        lifes = 0; // Forzamos las vidas a 0 (muerte directa)
        CheckLife(); // Llamamos a la comprobación de muerte
    }

    public void CheckLife()
    {
        if (isDead) return; // Si ya murió, no volvemos a ejecutar esto

        if (lifes <= 0) // Si las vidas son 0 o menos → muere
        {
            isDead = true; // Marcamos como muerto para bloquear futuras ejecuciones

            if (enemyCollider2D != null)
            {
                enemyCollider2D.enabled = false; // Desactivamos el collider para evitar más colisiones
            }

            if (destroyParticle != null)
            {
                destroyParticle.SetActive(true); // Activamos la partícula (ya existente en el enemigo)
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false; // Ocultamos el sprite del enemigo
            }

            Invoke("EnmyDie", 0.2f); // Esperamos un poco antes de destruir el objeto (para que se vea la partícula)
        }
    }

    public void EnmyDie()
    {
        Destroy(gameObject); // Eliminamos completamente el enemigo de la escena
    }
}