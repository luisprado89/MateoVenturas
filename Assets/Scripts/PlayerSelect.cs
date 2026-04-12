using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public bool enableSelectCharacter; // Variable para habilitar la selección de personaje
    public enum Player { Mat, Dino }// Para seleccionar el personaje
    public Player playerSelected; // Variable para almacenar el personaje seleccionado
    public Animator animator; // Referencia al componente Animator
    public SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer para cambiar la imagen del personaje
    public RuntimeAnimatorController[] playersControllers; // Array de controladores de animación para cada personaje
    public Sprite[] playersRenderer; // Array de sprites para cada personaje
    void Start()
    {
        if (!enableSelectCharacter) // Si la selección de personaje no está habilitada, asignamos el personaje por defecto
        {
            ChangePlayerInMenu(); // Llamar al método para cambiar la apariencia del jugador en el menú
        }
        else
        {

            switch (playerSelected)
            {
                case Player.Mat: // Si el personaje seleccionado es Mat, asignamos su controlador de animación y sprite
                    animator.runtimeAnimatorController = playersControllers[0];
                    spriteRenderer.sprite = playersRenderer[0];
                    break;
                case Player.Dino: // Si el personaje seleccionado es Dino, asignamos su controlador de animación y sprite
                    animator.runtimeAnimatorController = playersControllers[1];
                    spriteRenderer.sprite = playersRenderer[1];
                    break;
            }
        }
    }

    public void ChangePlayerInMenu() // Método para cambiar la apariencia del jugador en el menú
    {

        switch (PlayerPrefs.GetString("PlayerSelected")) // Obtener el personaje seleccionado de PlayerPrefs
        {
            case "Mat": // Si el personaje seleccionado es Mat, asignamos su controlador de animación y sprite
                animator.runtimeAnimatorController = playersControllers[0];
                spriteRenderer.sprite = playersRenderer[0];
                break;
            case "Dino": // Si el personaje seleccionado es Dino, asignamos su controlador de animación y sprite
                animator.runtimeAnimatorController = playersControllers[1];
                spriteRenderer.sprite = playersRenderer[1];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
