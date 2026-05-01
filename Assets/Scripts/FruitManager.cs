using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FruitManager : MonoBehaviour
{
    public TMP_Text levelCleared; // Texto que aparece al completar el nivel
    public GameObject transition; // Grupo de transición (el que has creado)
    public TMP_Text totalFruits; // Texto total frutas
    public TMP_Text fruitsCollected; // Texto frutas recogidas

    private int totalFruitsInLevel; // Total de frutas en el nivel

    public PlayerRespawn playerRespawn; // Referencia al jugador

    private bool levelCompleted = false; //  IMPORTANTE: evita que se ejecute varias veces

    void Start()
    {
        // Contar todas las frutas activas al inicio
        totalFruitsInLevel = CountActiveFruits();

        // Asegurarse de que la transición empieza apagada
        transition.SetActive(false);
    }

    private void Update()
    {
        AllFruitCollected();

        // Actualizar UI
        totalFruits.text = totalFruitsInLevel.ToString();
        fruitsCollected.text = CountActiveFruits().ToString();
    }

    public void AllFruitCollected()
    {
        // Solo entra si el nivel todavía no se ha completado y no quedan frutas activas
        if (!levelCompleted && CountActiveFruits() == 0)
        {
            levelCompleted = true; // Evita que esta lógica se ejecute muchas veces

            Debug.Log("No quedan frutas"); // Mensaje en consola para comprobar que funciona

            // Guardamos si esta escena es la última del Build Settings
            bool isLastLevel = SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;

            // Si es el último nivel, mostramos mensaje final de victoria
            if (isLastLevel)
            {
                levelCleared.text = "¡ENHORABUENA!\nHAS COMPLETADO LA AVENTURA\n\nHAS SUPERADO TODOS LOS DESAFÍOS\nY RECOGIDO TODAS LAS FRUTAS\n\n¡ERES UN VERDADERO MAESTRO FRUTÍCOLA!";
            }
            else
            {
                levelCleared.text = "¡Has recogido todas las frutas!";
            }

            levelCleared.gameObject.SetActive(true); // Mostrar el texto

            transition.SetActive(true); // Activar TransitionAnimationGroup

            // Si es el último nivel, damos más tiempo para leer el mensaje final
            if (isLastLevel)
            {
                Invoke("ChangeScene", 8f); // Espera 8 segundos antes de volver al menú
            }
            else
            {
                Invoke("ChangeScene", 2f); // Espera 2 segundos antes de pasar al siguiente nivel
            }
        }
    }

    private int CountActiveFruits()
    {
        int activeFruits = 0;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                activeFruits++;
            }
        }

        return activeFruits;
    }

    void ChangeScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}