using UnityEngine;

public class FruitCollected : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false; // Desactivar el sprite del objeto para simular que ha sido recogido
            transform.GetChild(0).gameObject.SetActive(true); // Activar el hijo del objeto para mostrar la fruta recogida

            Destroy(gameObject, 0.5f); // Destruir el objeto después de un corto período de tiempo para limpiar la escena
            GameAudioManager.Instance.PlayFruitCollectedSound(); // Reproducir el sonido de recogida de fruta que se encuentra en el GameAudioManager

        }
    }
}
