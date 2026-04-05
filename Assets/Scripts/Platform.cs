using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D platformEffector; // Referencia al componente PlatformEffector2D para controlar el comportamiento de la plataforma
    public float startWaitTime; // Tiempo de espera antes de que la plataforma permita al jugador pasar a través de ella
    private float waitTime; // Variable para almacenar el tiempo de espera actual
    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>(); // Obtener la referencia al componente PlatformEffector2D al iniciar el juego
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s") || Input.GetKeyUp(KeyCode.DownArrow)) // Verificar si se suelta la tecla "S" de bajar o la flecha hacia abajo para volver a bloquear la plataforma
        {
            waitTime = startWaitTime; // Reiniciar el tiempo de espera después de permitir al jugador pasar a través de la plataforma


        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) // Verificar si se presiona la tecla "S" de bajar o la flecha hacia abajo para permitir al jugador pasar a través de la plataforma
        {
            if (waitTime <= 0) // Verificar si el tiempo de espera ha transcurrido para permitir al jugador pasar a través de la plataforma
            {
                platformEffector.rotationalOffset = 180f; // Cambiar el ángulo de rotación del PlatformEffector2D para permitir al jugador pasar a través de la plataforma
                waitTime = startWaitTime; // Reiniciar el tiempo de espera después de permitir al jugador pasar a través de la plataforma
            }
            else
            {
                waitTime -= Time.deltaTime; // Reducir el tiempo de espera en cada actualización hasta que se alcance el tiempo de espera establecido
            }
        }

        if (Input.GetKeyUp("s") || Input.GetKeyUp(KeyCode.DownArrow)) // Verificar si se suelta la tecla "S" de bajar o la flecha hacia abajo para volver a bloquear la plataforma
        {
            platformEffector.rotationalOffset = 0f; // Restablecer el ángulo de rotación del PlatformEffector2D para bloquear la plataforma nuevamente
        }
    }
}
