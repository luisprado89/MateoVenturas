using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FruitManager : MonoBehaviour
{
    public TMP_Text levelCleared;
    public GameObject transition;
    public TMP_Text totalFruits;
    public TMP_Text fruitsCollected;
    private int totalFruitsInLevel;
    public PlayerRespawn playerRespawn; // Referencia al script PlayerRespawn

    void Start()
    {
        // Contar todas las frutas activas al inicio
        totalFruitsInLevel = CountActiveFruits();
    }

    private void Update()
    {
        AllFruitCollected();
        totalFruits.text = totalFruitsInLevel.ToString();
        fruitsCollected.text = CountActiveFruits().ToString();
    }

    public void AllFruitCollected()
    {
        if (CountActiveFruits() == 0)
        {
            Debug.Log("No quedan frutas");
            levelCleared.gameObject.SetActive(true); // Activar el texto
            transition.SetActive(true);
            Invoke("ChangeScene", 2); // Esperar 2 segundos antes de cambiar de escena
        }
    }

    private int CountActiveFruits()
    {
        int activeFruits = 0;

        // Recorrer todos los hijos del objeto FruitManager
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf) // Verificar si el hijo está activo
            {
                activeFruits++;
            }
        }

        return activeFruits;
    }

    void ChangeScene()
    {
        // Guardar las vidas actuales del jugador
        //playerRespawn.SaveCurrentLives();

        // Verificar si el nivel actual es el último nivel
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // Si es el último nivel, cargar el Main Menu (índice 0)
            SceneManager.LoadScene(0);
        }
        else
        {
            // Si no es el último nivel, cargar el siguiente nivel
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
