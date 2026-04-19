using UnityEngine;
using UnityEngine.SceneManagement;

// Clase encargada de gestionar los botones y paneles de la interfaz del juego
public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel; // Panel de opciones
    public GameObject iconMute;     // Imagen del icono de mute (solo visible cuando NO hay sonido)

    private bool isMuted = false;   // Estado del sonido (true = muteado)

    private void Start()
    {
        AudioListener.volume = 1f; // Al iniciar el juego el sonido está activo

        if (iconMute != null) // Comprobamos que el icono esté asignado
        {
            iconMute.SetActive(false); // Ocultamos el icono porque al inicio hay sonido
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted; // Cambiamos el estado (true/false)

        // Aplicamos mute global a TODO el juego
        AudioListener.volume = isMuted ? 0f : 1f;

        // Activamos el icono solo cuando el juego esté muteado
        if (iconMute != null)
        {
            iconMute.SetActive(isMuted);
        }
    }

    public void OptionsPanel()
    {
        Time.timeScale = 0f; // Pausar el juego
        optionsPanel.SetActive(true); // Mostrar panel
    }

    public void Return()
    {
        Time.timeScale = 1f; // Reanudar juego
        optionsPanel.SetActive(false); // Ocultar panel
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Asegurar tiempo normal
        SceneManager.LoadScene("Level1"); // Cambia a "Menu" si corresponde
    }

    public void QuitGame()
    {
        Application.Quit(); // Cerrar juego (solo funciona en build)
    }
}