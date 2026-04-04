using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public void AllFruitsCollected()
    {
        if (transform.childCount == 1) // Verificar si no quedan frutas como hijos del objeto
        {
            Debug.Log("All fruits collected!"); // Imprimir un mensaje en la consola para indicar que todas las frutas han sido recogidas
        }
    }
}
