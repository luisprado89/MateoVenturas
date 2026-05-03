using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si el objeto que entra en el trigger es el Player
        if (collision.transform.CompareTag("Player"))
        {
            // Obtenemos el Rigidbody2D del Player para saber si está cayendo
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            // Si el jugador está cayendo, NO le hacemos daño
            // Esto evita que al pisar al enemigo active también el collider de daño
            if (playerRb != null && playerRb.linearVelocity.y < 0)
            {
                return;
            }

            // Buscamos si el Player tiene el script PlayerPowerUp
            PlayerPowerUp powerUp = collision.GetComponent<PlayerPowerUp>();

            // Si el Player tiene el power-up activo
            if (powerUp != null && powerUp.powerUpActive)
            {
                // Buscamos el script JumpDamage en los objetos padre del DamageObject
                JumpDamage jumpDamage = GetComponentInParent<JumpDamage>();

                // Si encontramos JumpDamage, matamos al enemigo usando su propio sistema
                if (jumpDamage != null)
                {
                    jumpDamage.KillByPowerUp();
                }

                // Salimos para que el jugador NO reciba daño
                return;
            }

            // Si NO tiene power-up y NO está cayendo, el jugador recibe daño
            collision.transform.SetParent(null);

            Debug.Log("Player hit by damage object!");

            PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();

            if (playerRespawn != null)
            {
                playerRespawn.PlayerDamaged();
            }
        }
    }
}