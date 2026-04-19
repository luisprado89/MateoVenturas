using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private float checkPointPositionX, checkPointPositionY; // Variables para almacenar las posiciones X e Y  del checkpoint
    public Animator animator; // Referencia al componente Animator para controlar las animaciones del jugador
    public GameObject[] hearts; // Array de GameObjects que representan los corazones de vida del jugador
    private int lifes = 3; // Variable para almacenar el número de vidas del jugador
    void Start()
    {
        // Si el array de corazones está asignado, el número de vidas será igual al número de corazones
        if (hearts != null)
        {
            lifes = hearts.Length;
        }
        else
        {
            Debug.LogWarning("Hearts array is not assigned in the Inspector.");
            lifes = 0;
        }

        if (PlayerPrefs.GetFloat("checkPointPositionX") != 0) // Verificar si las posiciones del checkpoint han sido guardadas previamente
        {

            transform.position = (new Vector2(PlayerPrefs.GetFloat("checkPointPositionX"), PlayerPrefs.GetFloat("checkPointPositionY"))); // Teletransportar al jugador a la posición del checkpoint al iniciar el juego
        }
    }
    private void CheckLife()
    {
        // Si al jugador todavía le quedan vidas, desactivamos el corazón correspondiente
        if (lifes >= 0 && lifes < hearts.Length)
        {
            hearts[lifes].SetActive(false); // Ocultar el corazón correspondiente a la vida perdida
            animator.Play("Hit"); // Reproducir la animación de daño
        }

        // Si el jugador ya no tiene vidas, recargamos la escena actual
        if (lifes <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("CheckPointPositionX", x); // Guardar la posición X del checkpoint usando PlayerPrefs
        PlayerPrefs.SetFloat("CheckPointPositionY", y); // Guardar la posición Y del checkpoint usando PlayerPrefs
        Debug.Log("Checkpoint reached!"); // Imprimir un mensaje en la consola para indicar que se ha alcanzado el checkpoint
    }
    public void PlayerDamaged()
    {
        lifes--;// Reducir el número de vidas del jugador en 1 al recibir daño
        CheckLife();// Llamar al método CheckLife para verificar si el jugador ha perdido todas sus vidas y tomar las acciones correspondientes
    }

}
