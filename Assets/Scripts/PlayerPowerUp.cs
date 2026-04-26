using System.Collections; // Permite usar corrutinas con IEnumerator.
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour // Script que controla el power-up temporal del jugador.
{
    public float powerUpDuration = 30f; // Duración del power-up en segundos.
    public float blinkSpeed = 0.15f; // Tiempo entre cada cambio de color del parpadeo.
    public Color powerUpColor = Color.cyan; // Color que tendrá el jugador mientras parpadea.

    private Color originalColor; // Guarda el color original del jugador.
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del jugador.

    public bool powerUpActive = false; // Indica si el power-up está activo o no.

    void Start() // Se ejecuta una vez al iniciar la escena.
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer del jugador.
        originalColor = spriteRenderer.color; // Guarda el color inicial del jugador.
    }

    public void ActivatePowerUp() // Método que se llama cuando el jugador recoge el power-up.
    {
        StopAllCoroutines(); // Detiene cualquier power-up anterior que estuviera activo.
        StartCoroutine(PowerUpRoutine()); // Inicia la corrutina que controla el efecto del power-up.
    }

    private IEnumerator PowerUpRoutine() // Corrutina que controla duración y parpadeo.
    {
        powerUpActive = true; // Activa el estado de power-up.

        float endTime = Time.time + powerUpDuration; // Calcula el momento exacto en que debe terminar.

        while (Time.time < endTime) // Mientras no haya pasado el tiempo total del power-up.
        {
            spriteRenderer.color = powerUpColor; // Cambia el color del jugador al color del power-up.
            yield return new WaitForSeconds(blinkSpeed); // Espera antes de volver al color normal.

            spriteRenderer.color = originalColor; // Devuelve temporalmente el color original.
            yield return new WaitForSeconds(blinkSpeed); // Espera antes de volver a cambiar al color del power-up.
        }

        powerUpActive = false; // Desactiva el estado de power-up.
        spriteRenderer.color = originalColor; // Asegura que el jugador recupere su color original.
    }
}