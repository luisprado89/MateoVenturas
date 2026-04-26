using UnityEngine;

// Clase que gestiona todos los sonidos y la música del juego utilizando Singleton
public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance; // Instancia global del GameAudioManager

    public AudioSource musicAudioSource; // AudioSource para música de fondo y música de power-up
    public AudioSource sfxAudioSource; // AudioSource para sonidos cortos

    public AudioClip music; // Música normal del juego
    public AudioClip powerUpActiveLoop; // Música que suena mientras el power-up está activo

    public AudioClip fruitCollectedSound; // Sonido al recoger fruta
    public AudioClip buttonClickSound; // Sonido de botón
    public AudioClip powerUpPickupSound; // Sonido al recoger el power-up
    public AudioClip plantBulletShootSound; // Sonido del disparo de la planta

    private bool isMuted = false; // Indica si el juego está silenciado

    private void Awake()
    {
        if (Instance == null) // Si no existe otro GameAudioManager
        {
            Instance = this; // Esta instancia será la principal
            DontDestroyOnLoad(gameObject); // No se destruye al cambiar de escena
        }
        else // Si ya existe otro GameAudioManager
        {
            Destroy(gameObject); // Destruye este duplicado
        }
    }

    private void Start()
    {
        AudioListener.volume = 1f; // Activa el volumen general del juego
        PlayMusic(); // Empieza la música normal
    }

    public void ToggleMute()
    {
        isMuted = !isMuted; // Cambia el estado de mute
        AudioListener.volume = isMuted ? 0f : 1f; // Silencia o activa todo el audio
    }

    public bool IsMuted()
    {
        return isMuted; // Devuelve si el juego está muteado
    }

    public void PlayMusic()
    {
        if (musicAudioSource != null && music != null) // Comprueba que existe AudioSource y música
        {
            musicAudioSource.Stop(); // Detiene cualquier música anterior
            musicAudioSource.clip = music; // Asigna la música normal
            musicAudioSource.loop = true; // Activa bucle
            musicAudioSource.Play(); // Reproduce música normal
        }
    }

    public void StartPowerUpMusic()
    {
        if (!isMuted && musicAudioSource != null && powerUpActiveLoop != null) // Comprueba que puede reproducirse
        {
            musicAudioSource.Stop(); // Detiene la música normal
            musicAudioSource.clip = powerUpActiveLoop; // Asigna la música del power-up
            musicAudioSource.loop = true; // La reproduce en bucle
            musicAudioSource.Play(); // Empieza la música del power-up
        }
    }

    public void StopPowerUpMusic()
    {
        PlayMusic(); // Vuelve a reproducir la música normal
    }

    public void PlayFruitCollectedSound()
    {
        if (!isMuted && sfxAudioSource != null && fruitCollectedSound != null) // Comprueba sonido
        {
            sfxAudioSource.PlayOneShot(fruitCollectedSound); // Reproduce sonido de fruta
        }
    }

    public void PlayButtonClickSound()
    {
        if (!isMuted && sfxAudioSource != null && buttonClickSound != null) // Comprueba sonido
        {
            sfxAudioSource.PlayOneShot(buttonClickSound); // Reproduce sonido de botón
        }
    }

    public void PlayPowerUpPickupSound()
    {
        if (!isMuted && sfxAudioSource != null && powerUpPickupSound != null) // Comprueba sonido
        {
            sfxAudioSource.PlayOneShot(powerUpPickupSound); // Reproduce sonido al recoger power-up
        }
    }

    public void PlayPlantBulletShootSound()
    {
        if (!isMuted && sfxAudioSource != null && plantBulletShootSound != null) // Comprueba sonido
        {
            sfxAudioSource.PlayOneShot(plantBulletShootSound); // Reproduce sonido del disparo
        }
    }
}