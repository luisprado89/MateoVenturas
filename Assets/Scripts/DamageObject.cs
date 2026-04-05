using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player hit by damage object!"); // Imprimir un mensaje en la consola para indicar que el jugador ha sido golpeado por el objeto de daño
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged(); // Llamar al método PlayerDamaged del script PlayerRespawn para simular el daño al jugador
        }
    }
}
