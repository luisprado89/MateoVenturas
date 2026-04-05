using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private float checkPointPositionX, checkPointPositionY; // Variables para almacenar las posiciones X e Y  del checkpoint
    public Animator animator; // Referencia al componente Animator para controlar las animaciones del jugador
    void Start()
    {
        if (PlayerPrefs.GetFloat("checkPointPositionX") != 0) // Verificar si las posiciones del checkpoint han sido guardadas previamente
        {

            transform.position = (new Vector2(PlayerPrefs.GetFloat("checkPointPositionX"), PlayerPrefs.GetFloat("checkPointPositionY"))); // Teletransportar al jugador a la posición del checkpoint al iniciar el juego
        }
    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("checkPointPositionX", x); // Guardar la posición X del checkpoint usando PlayerPrefs
        PlayerPrefs.SetFloat("checkPointPositionY", y); // Guardar la posición Y del checkpoint usando PlayerPrefs
        Debug.Log("Checkpoint reached!"); // Imprimir un mensaje en la consola para indicar que se ha alcanzado el checkpoint
    }
    public void PlayerDamaged()
    {
        animator.Play("Hit"); // Reproducir la animación de daño al recibir daño
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual al recibir daño para simular el respawn del jugador
    }

}
