using UnityEngine;
using UnityEngine.SceneManagement;

// Clase encargada de gestionar los botones y paneles de la interfaz del juego
public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel; // Panel de opciones
    public GameObject iconMute; // Imagen del icono de mute

    private void Start()
    {
        if (iconMute != null) // Comprobamos que el icono esté asignado
        {
            iconMute.SetActive(false); // Ocultamos el icono al inicio
        }
    }

    public void ToggleMute()
    {
        if (GameAudioManager.Instance != null) // Comprobamos que exista el gestor global de audio
        {
            GameAudioManager.Instance.ToggleMute(); // Pedimos al gestor de audio que mutee o reactive el sonido

            if (iconMute != null) // Comprobamos que el icono esté asignado
            {
                iconMute.SetActive(GameAudioManager.Instance.IsMuted()); // Mostramos el icono solo si el juego está muteado
            }
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
        SceneManager.LoadScene("Level1"); // Cargar escena principal
    }

    public void QuitGame()
    {
        Application.Quit(); // Cerrar juego
    }
}