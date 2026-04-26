using UnityEngine;

public class PowerUpStar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) // Se ejecuta cuando algo entra en el trigger
    {
        if (collision.CompareTag("Player")) // Comprobamos que quien entra es el jugador
        {
            // Obtenemos el script del jugador que gestiona el power-up
            PlayerPowerUp playerPowerUp = collision.GetComponent<PlayerPowerUp>();

            // Si el jugador tiene el script, activamos el power-up
            if (playerPowerUp != null)
            {
                playerPowerUp.ActivatePowerUp(); // Activa color + lógica de invencibilidad
            }

            //  Reproducimos el sonido de recoger el power-up
            if (GameAudioManager.Instance != null)
            {
                GameAudioManager.Instance.PlayPowerUpPickupSound();
            }

            //  Destruimos el objeto para que no pueda recogerse otra vez
            Destroy(gameObject);
        }
    }
}