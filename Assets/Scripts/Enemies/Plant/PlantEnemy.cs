using UnityEngine;

// Script que controla el comportamiento de una planta enemiga que dispara proyectiles automáticamente
public class PlantEnemy : MonoBehaviour
{
    // Tiempo acumulado de espera antes de atacar
    private float waitedTime;

    // Tiempo que debe pasar entre cada ataque (cooldown)
    public float waitTimeToAttack = 3f;

    // Dirección de la planta y del disparo
    // true = mira y dispara hacia la izquierda
    // false = mira y dispara hacia la derecha
    public bool shootLeft = true;

    // Referencia al Animator para controlar las transiciones de animación
    public Animator animator;

    // Referencia al SpriteRenderer para poder girar visualmente la planta con Flip X
    public SpriteRenderer spriteRenderer;

    // Prefab de la bala que se va a instanciar al atacar
    public GameObject bulletPrefab;

    // Punto desde donde se generará la bala (posición y rotación)
    public Transform launchSpawnPoint;

    void Start()
    {
        // Inicializamos el contador con el tiempo de espera definido
        waitedTime = waitTimeToAttack;

        // Aplicamos al empezar la dirección visual de la planta según shootLeft
        UpdateDirectionVisual();
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
                // Activamos el parámetro bool "Attack"
                // Esto hace que el Animator pase de Idle a Attack
                animator.SetBool("Attack", true);
            }

            // Llamamos a la función LaunchBullet después de 0.5 segundos
            // Esto sirve para sincronizar la bala con el momento visual del disparo
            Invoke("LaunchBullet", 0.5f);

            // Desactivamos el ataque después de un pequeño tiempo
            // Esto permite que el Animator vuelva de Attack a Idle
            Invoke("ResetAttack", 0.8f);
        }
        else
        {
            // Si aún no toca atacar, reducimos el tiempo restante según el tiempo real
            waitedTime -= Time.deltaTime;
        }
    }

    // Método encargado de devolver la planta al estado Idle
    private void ResetAttack()
    {
        // Comprobamos que el Animator existe antes de usarlo
        if (animator != null)
        {
            // Desactivamos el parámetro bool "Attack"
            // Esto hace que el Animator pase de Attack a Idle
            animator.SetBool("Attack", false);
        }
    }

    // Método encargado de aplicar visualmente la dirección de la planta
    private void UpdateDirectionVisual()
    {
        // Comprobamos que el SpriteRenderer está asignado antes de usarlo
        if (spriteRenderer != null)
        {
            // Si shootLeft está activado, la planta mantiene su orientación original hacia la izquierda
            // Si shootLeft está desactivado, se activa Flip X para mirar hacia la derecha
            spriteRenderer.flipX = !shootLeft;
        }
    }

    // Método encargado de crear (instanciar) la bala
    public void LaunchBullet()
    {
        // Mensaje para comprobar en consola que el disparo se está ejecutando
        Debug.Log("La planta ha disparado.");

        // Verificamos que el prefab de la bala y el punto de disparo estén asignados
        if (bulletPrefab != null && launchSpawnPoint != null)
        {
            // Creamos una nueva bala en la posición y rotación del punto de disparo
            GameObject newBullet = Instantiate(
                bulletPrefab,
                launchSpawnPoint.position,
                launchSpawnPoint.rotation
            );

            // Obtenemos el script BulletPlant de la bala recién creada
            BulletPlant bulletScript = newBullet.GetComponent<BulletPlant>();

            // Si la bala tiene el script BulletPlant, le pasamos la dirección marcada en la planta
            if (bulletScript != null)
            {
                // La bala usará la misma dirección que la planta
                bulletScript.left = shootLeft;
            }
        }
        else
        {
            // Mensaje de advertencia en consola si falta alguna referencia
            Debug.LogWarning("bulletPrefab o launchSpawnPoint no estan asignados o han sido destruidos.");
        }
    }
}