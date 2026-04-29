using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    public float speed = 2f; // Velocidad de la bola en horizontal
    public float lifeTime = 5f; // Tiempo antes de destruirse

    public bool left; // Dirección: true izquierda, false derecha

    void Start()
    {
        // Reproducimos el sonido cuando la bala aparece
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayPlantBulletShootSound();
        }

        // Destruimos la bala después de un tiempo para no saturar la escena
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Si left es true, la bala se mueve hacia la izquierda
        if (left)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        // Si left es false, la bala se mueve hacia la derecha
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}