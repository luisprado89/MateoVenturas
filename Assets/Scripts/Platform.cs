using UnityEngine;
using UnityEngine.SceneManagement; // Permite comprobar en qué escena estamos actualmente
using UnityEngine.InputSystem; // Permite usar el nuevo Input System de Unity

public class Platform : MonoBehaviour
{
    private PlatformEffector2D platformEffector; // Referencia al PlatformEffector2D para permitir o bloquear el paso por la plataforma

    public float startWaitTime; // Tiempo que debe mantenerse pulsada la tecla antes de atravesar la plataforma
    private float waitTime; // Temporizador interno que va descontando el tiempo

    [Header("Mensaje UI (solo Level1)")]
    public GameObject messageUI; // Texto informativo que solo se usará en Level1

    private bool isLevel1; // Indica si estamos en Level1 para usar el mensaje solo en ese nivel

    void Start()
    {
        // Obtenemos el componente PlatformEffector2D que está en este mismo objeto
        platformEffector = GetComponent<PlatformEffector2D>();

        // Inicializamos el temporizador con el tiempo configurado en el Inspector
        waitTime = startWaitTime;

        // Comprobamos una sola vez si la escena actual es Level1
        isLevel1 = SceneManager.GetActiveScene().name == "Level1";

        // Si estamos en Level1 y hay mensaje asignado, lo ocultamos al iniciar
        // En Level2 y Level3 puede estar vacío sin dar error
        if (isLevel1 && messageUI != null)
        {
            messageUI.SetActive(false);
        }
    }

    void Update()
    {
        // ==============================
        // SEGURIDAD DEL INPUT SYSTEM
        // ==============================
        // Si no hay teclado detectado, salimos para evitar errores
        if (Keyboard.current == null)
            return;

        // ==============================
        // FUNCIONAMIENTO GENERAL
        // ==============================
        // Esta parte funciona en todos los niveles.
        // No depende del mensaje UI.

        // Comprobamos si el jugador mantiene pulsada la tecla S o la flecha hacia abajo
        // usando el nuevo Input System de Unity.
        bool bajarPulsado = Keyboard.current.sKey.isPressed ||
                            Keyboard.current.downArrowKey.isPressed;

        // Comprobamos si el jugador acaba de soltar la tecla S o la flecha hacia abajo
        // usando el nuevo Input System de Unity.
        bool bajarSoltado = Keyboard.current.sKey.wasReleasedThisFrame ||
                            Keyboard.current.downArrowKey.wasReleasedThisFrame;

        // Si el jugador suelta la tecla S o la flecha hacia abajo,
        // la plataforma vuelve a bloquear el paso desde arriba.
        if (bajarSoltado)
        {
            waitTime = startWaitTime; // Reiniciamos el tiempo de espera

            platformEffector.rotationalOffset = 0f; // Volvemos a dejar la plataforma en modo normal
        }

        // Si el jugador mantiene pulsada la tecla S o la flecha hacia abajo,
        // se activa la posibilidad de atravesar la plataforma desde arriba.
        if (bajarPulsado)
        {
            // Si el tiempo de espera ya terminó, permitimos atravesar la plataforma
            if (waitTime <= 0)
            {
                platformEffector.rotationalOffset = 180f; // Cambiamos el ángulo para permitir bajar atravesando la plataforma

                waitTime = startWaitTime; // Reiniciamos el temporizador
            }
            else
            {
                waitTime -= Time.deltaTime; // Reducimos el temporizador poco a poco
            }
        }
    }

    // ==============================
    // MOSTRAR MENSAJE AL PISAR
    // ==============================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si NO estamos en Level1, no hacemos nada.
        // Así en Level2 y Level3 no hace falta asignar ningún mensaje.
        if (!isLevel1)
            return;

        // Si el objeto que toca la plataforma es el jugador
        // y hay un mensaje asignado, se muestra el texto.
        if (collision.gameObject.CompareTag("Player") && messageUI != null)
        {
            messageUI.SetActive(true);
        }
    }

    // ==============================
    // OCULTAR MENSAJE AL SALIR
    // ==============================
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si NO estamos en Level1, no hacemos nada.
        // Así en Level2 y Level3 no hace falta asignar ningún mensaje.
        if (!isLevel1)
            return;

        // Si el jugador deja de tocar la plataforma
        // y hay un mensaje asignado, se oculta el texto.
        if (collision.gameObject.CompareTag("Player") && messageUI != null)
        {
            messageUI.SetActive(false);
        }
    }
}