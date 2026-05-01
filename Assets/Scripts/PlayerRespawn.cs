using System.Collections; // Permite usar corrutinas
using UnityEngine; // Importa las herramientas principales de Unity
using UnityEngine.SceneManagement; // Permite cambiar o recargar escenas
using TMPro; // Permite usar textos TextMeshPro

public class PlayerRespawn : MonoBehaviour
{
    private float checkPointPositionX, checkPointPositionY; // Variables para almacenar la posición X e Y del checkpoint

    public Animator animator; // Referencia al Animator para controlar las animaciones del jugador

    public GameObject[] hearts; // Array de corazones que representan las vidas del jugador en la UI

    private int lifes = 3; // Número actual de vidas del jugador

    public GameObject canvasGameOver; // Canvas completo del Game Over que estará desactivado al inicio

    public TMP_Text countdownText; // Texto que mostrará la cuenta atrás para volver al menú

    private bool isDead = false; // Evita que el Game Over se active varias veces

    void Start()
    {
        // Si el array de corazones está asignado
        if (hearts != null && hearts.Length > 0)
        {
            lifes = hearts.Length; // Las vidas serán igual al número de corazones
        }
        else
        {
            Debug.LogWarning("Hearts array is not assigned in the Inspector.");
            lifes = 0;
        }

        // Si el canvas de Game Over está asignado
        if (canvasGameOver != null)
        {
            canvasGameOver.SetActive(false); // Ocultar Game Over al iniciar la escena
        }

        // Si existe una posición guardada de checkpoint
        if (PlayerPrefs.HasKey("CheckPointPositionX"))
        {
            checkPointPositionX = PlayerPrefs.GetFloat("CheckPointPositionX");
            checkPointPositionY = PlayerPrefs.GetFloat("CheckPointPositionY");

            transform.position = new Vector2(checkPointPositionX, checkPointPositionY);
        }
    }

    private void CheckLife()
    {
        // Si la vida está dentro del rango de corazones
        if (lifes >= 0 && lifes < hearts.Length)
        {
            hearts[lifes].SetActive(false); // Ocultar el corazón que corresponde a la vida perdida
            animator.Play("Hit"); // Reproducir animación de daño
        }

        // Si el jugador se queda sin vidas y todavía no se activó el Game Over
        if (lifes <= 0 && !isDead)
        {
            isDead = true; // Marcar al jugador como muerto para evitar repetir esta lógica

            Debug.Log("GAME OVER");

            canvasGameOver.SetActive(true); // Activar el CanvasGameOver

            Time.timeScale = 0f; // Pausar el juego

            StartCoroutine(ReturnToMenuCountdown()); // Iniciar cuenta atrás para volver al menú
        }
    }

    public void PlayerDamaged()
    {
        // Evita que siga restando vidas si ya está en 0
        if (lifes <= 0) return;

        lifes--; // Restar una vida
        CheckLife(); // Revisar corazones y muerte
    }

    public bool AddLife()
    {
        // Si ya tiene todas las vidas, no suma nada
        if (lifes >= hearts.Length)
        {
            Debug.Log("Vida máxima. No se puede recoger el corazón.");
            return false;
        }

        hearts[lifes].SetActive(true); // Activar el corazón que corresponde a la nueva vida

        lifes++; // Sumar una vida

        Debug.Log("Vida sumada. Vidas actuales: " + lifes);

        return true; // Indica que la vida sí se sumó
    }

    private IEnumerator ReturnToMenuCountdown()
    {
        int timeLeft = 10; // Tiempo inicial de la cuenta atrás

        while (timeLeft > 0)
        {
            countdownText.text = "Volviendo al menú en " + timeLeft + "..."; // Actualizar texto

            yield return new WaitForSecondsRealtime(1f); // Esperar 1 segundo real aunque el juego esté pausado

            timeLeft--; // Restar 1 segundo
        }

        Time.timeScale = 1f; // Reanudar el tiempo antes de cambiar de escena

        SceneManager.LoadScene(0); // Cargar el menú principal, suponiendo que está en Build Index 0
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Reanudar el tiempo del juego

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("CheckPointPositionX", x); // Guardar X del checkpoint
        PlayerPrefs.SetFloat("CheckPointPositionY", y); // Guardar Y del checkpoint

        Debug.Log("Checkpoint reached!");
    }
}