using UnityEngine; // Importa las funciones básicas de Unity
using UnityEngine.SceneManagement; // Permite cambiar de escena
using TMPro; // Permite usar textos TMP

public class FruitManager : MonoBehaviour // Clase que controla las frutas del nivel
{
    [Header("UI de nivel")]
    public GameObject levelCleared; // Objeto con el texto corto de nivel completado
    public GameObject victoryPanel; // Panel final de victoria con sus textos ya escritos en Unity
    public GameObject transition; // Grupo de transición

    public TMP_Text totalFruits; // Texto que muestra el total de frutas necesarias
    public TMP_Text fruitsCollected; // Texto que muestra las frutas recogidas

    public PlayerRespawn playerRespawn; // Referencia al jugador

    [Header("Modo demostración")]
    public bool demoMode = false; // Si está activado, solo hace falta recoger algunas frutas
    public int demoFruitsToWin = 8; // Frutas necesarias para ganar en modo demo

    private int totalFruitsInLevel; // Total real de frutas activas al iniciar
    private bool levelCompleted = false; // Evita ejecutar la victoria varias veces

    void Start()
    {
        // Contamos todas las frutas activas al inicio del nivel
        totalFruitsInLevel = CountActiveFruits();

        // Ocultamos el texto corto al iniciar el nivel
        if (levelCleared != null)
        {
            levelCleared.SetActive(false);
        }

        // Ocultamos el panel de victoria final al iniciar el nivel
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // Aseguramos que la transición empieza apagada
        if (transition != null)
        {
            transition.SetActive(false);
        }

        // Inicializamos la UI
        UpdateFruitUI();
    }

    private void Update()
    {
        // Si el nivel ya está completado, no seguimos actualizando ni comprobando
        if (levelCompleted)
        {
            return;
        }

        // Actualizamos la UI constantemente
        UpdateFruitUI();

        // Comprobamos si se ha completado el nivel
        AllFruitCollected();
    }

    private void UpdateFruitUI()
    {
        // Si algún texto no está asignado, evitamos errores
        if (totalFruits == null || fruitsCollected == null)
        {
            return;
        }

        // Calculamos cuántas frutas se han recogido
        int collected = totalFruitsInLevel - CountActiveFruits();

        // Si estamos en modo demo, mostramos el objetivo de demo
        if (demoMode)
        {
            totalFruits.text = demoFruitsToWin.ToString();
            fruitsCollected.text = collected.ToString();
        }
        else
        {
            totalFruits.text = totalFruitsInLevel.ToString();
            fruitsCollected.text = collected.ToString();
        }
    }

    public void AllFruitCollected()
    {
        // Calculamos cuántas frutas se han recogido
        int collected = totalFruitsInLevel - CountActiveFruits();

        // En modo demo, se gana al recoger las frutas indicadas en el inspector
        if (demoMode && collected >= demoFruitsToWin)
        {
            CompleteLevel();
        }

        // En modo normal, se gana cuando no queda ninguna fruta activa
        if (!demoMode && CountActiveFruits() == 0)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        levelCompleted = true; // Marcamos el nivel como completado

        Debug.Log("Frutas necesarias recogidas"); // Mensaje de comprobación

        // Comprobamos si esta escena es la última del Build Settings
        bool isLastLevel = SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;

        // Si es el último nivel, activamos el panel final
        if (isLastLevel)
        {
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(true);
            }
        }
        else
        {
            // Si no es el último nivel, activamos el mensaje corto
            if (levelCleared != null)
            {
                levelCleared.SetActive(true);
            }
        }

        // Activamos la transición
        if (transition != null)
        {
            transition.SetActive(true);
        }

        // Si es el último nivel, damos más tiempo para leer
        if (isLastLevel)
        {
            Invoke(nameof(ChangeScene), 8f);
        }
        else
        {
            Invoke(nameof(ChangeScene), 2f);
        }
    }

    private int CountActiveFruits()
    {
        int activeFruits = 0; // Contador de frutas activas

        foreach (Transform child in transform) // Recorremos todos los hijos del FruitManager
        {
            if (child.gameObject.activeSelf) // Si la fruta está activa
            {
                activeFruits++; // Sumamos una fruta activa
            }
        }

        return activeFruits; // Devolvemos el total de frutas activas
    }

    void ChangeScene()
    {
        // Si estamos en la última escena del Build Settings, volvemos al menú
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            // Si no, pasamos al siguiente nivel
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}