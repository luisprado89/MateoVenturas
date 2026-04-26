using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    public float speed = 2f; // Velocidad de la bola en horizontal
    public float lifeTime = 2f; // Tiempo antes de destruirse
    public bool left; // Dirección: true izquierda, false derecha

    void Start()
    {
        //  Reproducimos el sonido cuando la bala aparece (se dispara)
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayPlantBulletShootSound();
        }

        // ⏱ Destruimos la bala después de un tiempo para no saturar la escena
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Si la bala va hacia la izquierda
        if (left)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else // Si va hacia la derecha
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}