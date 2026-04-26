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

    private void OnCollisionEnter2D(Collision2D collision) // Método que se ejecuta cuando ocurre una colisión física
    {
        if (isDead) return; // Si el enemigo ya está muerto, no hacemos nada más

        if (collision.gameObject.CompareTag("Player")) // Comprobamos si el objeto que colisiona es el jugador
        {
            PlayerPowerUp powerUp = collision.gameObject.GetComponent<PlayerPowerUp>(); // Obtenemos el script del power-up del jugador

            // SI EL JUGADOR TIENE POWER-UP ACTIVO
            if (powerUp != null && powerUp.powerUpActive)
            {
                KillByPowerUp(); // Matamos al enemigo directamente
                return; // Salimos para evitar que haga el resto de lógica (rebote, daño, etc.)
            }

            // SI NO TIENE POWER-UP → comportamiento normal
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>(); // Obtenemos el Rigidbody del jugador

            if (playerRb != null) // Comprobamos que existe para evitar errores
            {
                // Aplicamos rebote hacia arriba al jugador (efecto Mario al pisar enemigo)
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
            }

            LosseLifeAndHit(); // Quitamos una vida al enemigo y activamos animación de golpe
            CheckLife(); // Comprobamos si el enemigo debe morir
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