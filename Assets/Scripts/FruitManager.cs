using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitManager : MonoBehaviour
{
    private void Update()
    {
        AllFruitsCollected();  // Llamar al método AllFruitsCollected en cada actualización para verificar si todas las frutas han sido recogidas
    }
    public void AllFruitsCollected()
    {
        if (transform.childCount == 0) // Verificar si no quedan frutas como hijos del objeto
        {
            Debug.Log("All fruits collected!"); // Imprimir un mensaje en la consola para indicar que todas las frutas han sido recogidas
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cargar la siguiente escena al recoger todas las frutas
        }
    }
}
