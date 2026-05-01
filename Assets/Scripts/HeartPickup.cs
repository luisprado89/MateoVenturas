using UnityEngine; // Importa las herramientas principales de Unity

// Script que controla:
// 1. El efecto visual del corazón (giro izquierda ↔ derecha sin animación)
// 2. La lógica de recoger el corazón y sumar vida al jugador
public class HeartPickup : MonoBehaviour
{
    public float flipSpeed = 2f; // Velocidad del efecto de giro (cuanto mayor, más rápido oscila)

    private Vector3 originalScale; // Variable para guardar la escala original del objeto (la que tú pusiste en el Inspector)

    void Start()
    {
        // Guardamos la escala original del objeto al iniciar el juego
        // Esto es MUY importante para no perder el tamaño que configuraste manualmente
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Mathf.Sin genera un valor que oscila entre -1 y 1 en el tiempo
        // Esto crea un efecto continuo de ida y vuelta (izquierda ↔ derecha)
        float scaleX = Mathf.Sin(Time.time * flipSpeed);

        // Aplicamos la escala SOLO en el eje X, multiplicando por la escala original
        // De esta forma:
        // - No cambiamos el tamaño real del objeto
        // - Solo hacemos que "se dé la vuelta" visualmente (flip)
        transform.localScale = new Vector3(
            originalScale.x * scaleX, // Eje X afectado (flip izquierda/derecha)
            originalScale.y,          // Eje Y se mantiene igual (altura original)
            originalScale.z           // Eje Z se mantiene igual
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si el objeto que entra en el trigger es el jugador
        if (collision.CompareTag("Player"))
        {
            // Intentamos obtener el script PlayerRespawn del jugador
            PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();

            // Si el jugador tiene el script (evitamos errores)
            if (playerRespawn != null)
            {
                // Intentamos añadir una vida
                // Este método devuelve true si se ha sumado correctamente
                bool lifeAdded = playerRespawn.AddLife();

                // SOLO destruimos el corazón si realmente se ha añadido la vida
                // (si el jugador ya tenía 3 vidas, no se destruye)
                if (lifeAdded)
                {
                    Destroy(gameObject); // Elimina el corazón de la escena
                }
            }
        }
    }
}