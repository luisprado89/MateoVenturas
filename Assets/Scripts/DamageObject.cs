using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si el objeto que entra en el trigger es el Player
        if (collision.transform.CompareTag("Player"))
        {
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

            // Si NO tiene power-up, comportamiento normal: el jugador recibe daño
            collision.transform.SetParent(null);

            Debug.Log("Player hit by damage object!");

            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged();
        }
    }
}