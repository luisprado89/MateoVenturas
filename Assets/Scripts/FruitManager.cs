using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FruitManager : MonoBehaviour
{
    public TMP_Text levelCleared; // Referencia al componente Text para mostrar el mensaje de nivel completado
    public GameObject transition; // Referencia al objeto de transición para activar la animación de transición al cambiar de escena
    private void Update()
    {
        AllFruitsCollected();  // Llamar al método AllFruitsCollected en cada actualización para verificar si todas las frutas han sido recogidas
    }
    public void AllFruitsCollected()
    {
        if (transform.childCount == 0) // Verificar si no quedan frutas como hijos del objeto
        {
            Debug.Log("All fruits collected!"); // Imprimir un mensaje en la consola para indicar que todas las frutas han sido recogidas
            levelCleared.gameObject.SetActive(true); // Activar el mensaje de nivel completado
            transition.SetActive(true); // Activar el objeto de transición para mostrar la animación de transición al cambiar de escena
            Invoke("ChangeScene", 2f); // Llamar al método ChangeScene después de un retraso de 2 segundos para cambiar a la siguiente escena
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cargar la siguiente escena al llamar a este método
    }
}
