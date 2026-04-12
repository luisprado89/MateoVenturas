using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel; // Referencia al panel de opciones
    public void OptionsPanel()
    {
        Time.timeScale = 0f; // Pausar el juego
        optionsPanel.SetActive(true); // Mostrar el panel de opciones
    }
    public void Return()
    {
        Time.timeScale = 1f; // Reanudar el juego
        optionsPanel.SetActive(false); // Ocultar el panel de opciones
    }
    public void MainMenu()
    {
        Time.timeScale = 1f; // Reanudar el juego
        SceneManager.LoadScene("Level1"); // Cargar la escena del menú principal 
    }
    public void QuitGame()// Método para salir del juego que solo funcionará en la versión compilada del juego, no en el editor de Unity, aplication.build
    {
        Application.Quit(); // Salir del juego
    }

}
