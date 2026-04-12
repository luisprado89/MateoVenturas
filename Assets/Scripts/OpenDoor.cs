using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public TMP_Text text; // Referencia al componente de texto para mostrar el mensaje de Presiona la tecla E para entrar
    public string levelName; // Nombre del nivel al que se desea cargar
    private bool inDoor = false; // Variable para verificar si el jugador está dentro del área de la puerta

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoor = true; // El jugador ha entrado en el área de la puerta
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

            if (text != null)// Verificar si el componente de texto está asignado
            {
                text.gameObject.SetActive(false);// Ocultar el mensaje de Presiona la tecla E para entrar
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inDoor && Input.GetKey("e"))
        {
            if (text != null)// Verificar si el componente de texto está asignado
            {
                text.gameObject.SetActive(false); // Ocultar el mensaje de Presiona la tecla E para entrar
            }
            SceneManager.LoadScene(levelName); // Cargar el nivel especificado
        }
    }
}
