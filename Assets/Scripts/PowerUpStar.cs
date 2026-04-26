using UnityEngine;

public class PowerUpStar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)// Método que se llama cuando otro collider entra en contacto con el collider del power-up
    {
        if (collision.CompareTag("Player"))// Si el objeto que colisiona tiene la etiqueta "Player", activar el power-up para el jugador
        {
            PlayerPowerUp playerPowerUp = collision.GetComponent<PlayerPowerUp>();// Obtener la referencia al componente PlayerPowerUp del jugador para activar el power-up

            if (playerPowerUp != null)// Verificar si el jugador tiene el componente PlayerPowerUp antes de intentar activar el power-up para evitar errores
            {// Si el jugador tiene el componente PlayerPowerUp, llamar al método para activar el power-up
                playerPowerUp.ActivatePowerUp();
            }
            // Después de activar el power-up para el jugador, destruir el objeto del power-up para que no pueda ser recogido nuevamente
            Destroy(gameObject);
        }
    }
}