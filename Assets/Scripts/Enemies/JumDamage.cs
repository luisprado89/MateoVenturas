using UnityEngine;

public class JumDamage : MonoBehaviour
{

    public Collider2D enemyCollider2D; // Referencia al componente Collider2D del enemigo para detectar colisiones con el jugador
    public Animator animator; // Referencia al componente Animator para controlar las animaciones del enemigo
    public SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer para voltear el sprite del enemigo
    public GameObject destroyParticle; // Prefab del efecto de destrucción que se instanciará al destruir el enemigo
    public float jumpForce = 2.5f; // Fuerza del salto que se aplicará al jugador al destruir el enemigo
    public int lifes = 2; // Cantidad de vidas del enemigo antes de ser destruido

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verificar si el objeto que colisiona es el jugador
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity.x, jumpForce); // Aplicar una fuerza de salto al jugador para simular el rebote al destruir el enemigo
            LosseLifeAndHit(); // Llamar al método para reducir las vidas del enemigo y activar la animación de golpe
            CheckLife(); // Verificar si el enemigo ha perdido todas sus vidas para destruirlo
        }
    }
    public void LosseLifeAndHit()
    {
        lifes--; // Reducir las vidas del enemigo en 1
        animator.Play("Hit"); // Activar la animación de golpe al recibir daño
    }
    public void CheckLife()
    {
        if (lifes <= 0) // Verificar si el enemigo ha perdido todas sus vidas
        {
            destroyParticle.SetActive(true); // Activar el efecto de destrucción
            spriteRenderer.enabled = false; // Ocultar el sprite del enemigo
            Invoke("EnmyDie", 0.2f); // Llamar al método para destruir el enemigo después de un breve retraso para permitir que se vea el efecto de destrucción
        }
    }
    public void EnmyDie()
    {
        Destroy(gameObject); // Destruir el objeto del enemigo
    }
}
