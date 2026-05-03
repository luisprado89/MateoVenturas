using TMPro; // Permite usar textos TMP (TextMeshPro)
using UnityEngine; // Importa las funciones principales de Unity
using UnityEngine.SceneManagement; // Permite cambiar de escena

public class OpenDoor : MonoBehaviour
{
    public TMP_Text text; // Referencia al texto que muestra el mensaje para entrar por la puerta
    public string levelName; // Nombre del nivel que se cargará al entrar por la puerta

    public GameObject titleGame; // Referencia al título del menú MateoVenturas para ocultarlo cuando aparece el mensaje de la puerta

    private bool inDoor = false; // Indica si el jugador está dentro del área de la puerta

    public float waitTime = 5f; // Tiempo de espera en segundos antes de entrar automáticamente por la puerta
    private float timer = 0f; // Temporizador que cuenta cuánto tiempo lleva el jugador dentro de la puerta

    private void OnTriggerEnter2D(Collider2D collision) // Se ejecuta cuando otro Collider2D entra en el área de la puerta
    {
        if (collision.gameObject.CompareTag("Player")) // Comprueba si el objeto que entra tiene la etiqueta Player
        {
            inDoor = true; // Marcamos que el jugador está dentro del área de la puerta
            timer = 0f; // Reiniciamos el temporizador al entrar

            if (text != null) // Comprobamos que el texto esté asignado en el Inspector
            {
                text.gameObject.SetActive(true); // Mostramos el mensaje de "Pulsa E para entrar"
            }

            if (titleGame != null) // Comprobamos que el título del juego esté asignado en el Inspector
            {
                titleGame.SetActive(false); // Ocultamos el título MateoVenturas para que no se solape con el mensaje de la puerta
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // Se ejecuta cuando otro Collider2D sale del área de la puerta
    {
        if (collision.CompareTag("Player")) // Comprueba si el objeto que sale tiene la etiqueta Player
        {
            inDoor = false; // Marcamos que el jugador ya no está dentro del área de la puerta
            timer = 0f; // Reiniciamos el temporizador al salir

            if (text != null) // Comprobamos que el texto esté asignado en el Inspector
            {
                text.gameObject.SetActive(false); // Ocultamos el mensaje de "Pulsa E para entrar"
            }

            if (titleGame != null) // Comprobamos que el título del juego esté asignado en el Inspector
            {
                titleGame.SetActive(true); // Volvemos a mostrar el título MateoVenturas cuando el jugador sale de la puerta
            }
        }
    }

    void Update() // Se ejecuta una vez por frame
    {
        if (inDoor) // Comprueba si el jugador está dentro del área de la puerta
        {
            timer += Time.deltaTime; // Aumenta el temporizador con el tiempo real transcurrido

            if (timer >= waitTime) // Comprueba si el jugador ha esperado el tiempo suficiente dentro de la puerta
            {
                EnterDoor(); // Carga el nivel automáticamente
            }

            if (Input.GetKeyDown(KeyCode.E)) // Comprueba si el jugador pulsa la tecla E
            {
                EnterDoor(); // Carga el nivel al pulsar E
            }
        }
    }

    void EnterDoor() // Método encargado de cargar el nivel correspondiente
    {
        if (text != null) // Comprobamos que el texto esté asignado en el Inspector
        {
            text.gameObject.SetActive(false); // Ocultamos el mensaje antes de cambiar de escena
        }

        SceneManager.LoadScene(levelName); // Carga la escena indicada en levelName
    }
}