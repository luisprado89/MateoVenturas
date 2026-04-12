using UnityEngine;

public class ChangeDoorSkins : MonoBehaviour
{
    public GameObject skinsPanel; // Referencia al panel de skins
    public bool inDoor = false; // Variable para verificar si el jugador está dentro de la puerta
    public GameObject player; // Referencia al jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skinsPanel.gameObject.SetActive(true); // Mostrar el panel de skins
            inDoor = true; // El jugador ha entrado en la puerta
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skinsPanel.gameObject.SetActive(false); // Ocultar el panel de skins
            inDoor = false; // El jugador ha salido de la puerta
        }
    }
    public void SetPlayerMat()
    {
        PlayerPrefs.SetString("PlayerSelected", "Mat"); // Guardar la selección del jugador en PlayerPrefs
        ResetPlayerSkin(); // Reiniciar la apariencia del jugador
    }
    public void SetPlayerDino()
    {
        PlayerPrefs.SetString("PlayerSelected", "Dino"); // Guardar la selección del jugador en PlayerPrefs
        ResetPlayerSkin(); // Reiniciar la apariencia del jugador
    }
    void ResetPlayerSkin()
    {
        skinsPanel.gameObject.SetActive(false); // Ocultar el panel de skins
        player.GetComponent<PlayerSelect>().ChangePlayerInMenu(); // Llamar al método para cambiar la apariencia del jugador en el menú
    }
}
