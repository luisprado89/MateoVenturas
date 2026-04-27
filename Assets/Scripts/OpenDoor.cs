using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public TMP_Text text; // Referencia al componente de texto para mostrar el mensaje de Presiona la tecla E para entrar
    public string levelName; // Nombre del nivel al que se desea cargar
    private bool inDoor = false; // Variable para verificar si el jugador está dentro del área de la puerta
    // Temporizador
    public float waitTime = 5f;// Tiempo de espera en segundos
    private float timer = 0f;// Variable para verificar si el temporizador ha terminado

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoor = true; // El jugador ha entrado en el área de la puerta
            timer = 0f; // Reiniciar el temporizador
            if (text != null)// Verificar si el componente de texto está asignado
            {
                text.gameObject.SetActive(true); // Mostrar el mensaje de Presiona la tecla E para entrar
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDoor = false;
            timer = 0f; // Reiniciar el temporizador
            if (text != null)// Verificar si el componente de texto está asignado
            {
                text.gameObject.SetActive(false);// Ocultar el mensaje de Presiona la tecla E para entrar
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inDoor)// Verificar si el jugador está dentro del área de la puerta
        {
            timer += Time.deltaTime;// Incrementar el temporizador
            if (timer >= waitTime)// Verificar si el temporizador ha terminado
            {
                EnterDoor();// Cargar el nivel automáticamente después de que el temporizador haya terminado
            }


            if (Input.GetKeyDown(KeyCode.E))// Verificar si se presiona la tecla E para entrar
            {
                EnterDoor();// Cargar el nivel al presionar la tecla E
            }
        }
    }

    void EnterDoor()// Método para cargar el nivel
    {
        if (text != null)// Verificar si el componente de texto está asignado
            text.gameObject.SetActive(false);// Ocultar el mensaje de Presiona la tecla E para entrar

        SceneManager.LoadScene(levelName);// Cargar el nivel especificado por el nombre
    }
}
