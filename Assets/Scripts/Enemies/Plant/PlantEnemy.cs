using UnityEngine;

// Script que controla el comportamiento de una planta enemiga que dispara proyectiles automáticamente
public class PlantEnemy : MonoBehaviour
{
    // Tiempo acumulado de espera antes de atacar
    private float waitedTime;

    // Tiempo que debe pasar entre cada ataque (cooldown)
    public float waitTimeToAttack = 3f;

    // Referencia al Animator para reproducir la animación de ataque
    public Animator animator;

    // Prefab de la bala que se va a instanciar al atacar
    public GameObject bulletPrefab;

    // Punto desde donde se generará la bala (posición y rotación)
    public Transform launchSpawnPoint;

    void Start()
    {
        // Inicializamos el contador con el tiempo de espera definido
        waitedTime = waitTimeToAttack;
    }

    // Se ejecuta en cada frame
    private void Update()
    {
        // Si el tiempo de espera ha llegado a 0 o menos, el enemigo puede atacar
        if (waitedTime <= 0)
        {
            // Reiniciamos el contador para el siguiente ataque
            waitedTime = waitTimeToAttack;

            // Comprobamos que el Animator existe antes de usarlo
            if (animator != null)
            {
                // Reproduce la animación de ataque
                animator.Play("Attack");
            }

            // Llamamos a la función LaunchBullet después de 0.5 segundos
            // Esto suele sincronizarse con la animación (por ejemplo, cuando "dispara")
            Invoke("LaunchBullet", 0.5f);
        }
        else
        {
            // Si aún no toca atacar, reducimos el tiempo restante según el tiempo real
            waitedTime -= Time.deltaTime;
        }
    }

    // Método encargado de crear (instanciar) la bala
    public void LaunchBullet()
    {
        // Verificamos que el prefab de la bala y el punto de disparo estén asignados
        if (bulletPrefab != null && launchSpawnPoint != null)
        {
            // Creamos una nueva bala en la posición y rotación del punto de disparo
            GameObject newBullet = Instantiate(
                bulletPrefab,
                launchSpawnPoint.position,
                launchSpawnPoint.rotation
            );
        }
        else
        {
            // Mensaje de advertencia en consola si falta alguna referencia
            Debug.LogWarning("bulletPrefab o launchSpawnPoint no estan asignados o han sido destruidos.");
        }
    }
}