using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerRespawn>().ReachedCheckPoint(transform.position.x, transform.position.y); // Llamar al método ReachedCheckPoint del script PlayerRespawn para teletransportar
            GetComponent<Animator>().enabled = true; // Activar la animación del checkpoint al ser alcanzado por el jugador
        }
    }
}
