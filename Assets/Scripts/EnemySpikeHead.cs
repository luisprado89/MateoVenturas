using UnityEngine;

public class EnemySpikeHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player hit by damage object!"); // Imprimir un mensaje en la consola para indicar que el jugador ha sido golpeado por el objeto de daño
            Destroy(collision.gameObject); // Destruir el objeto del jugador para simular que ha sido dañado
        }
    }
}
