using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public enum Player { Mat, Dino }// Para seleccionar el personaje
    public Player playerSelected; // Variable para almacenar el personaje seleccionado
    public Animator animator; // Referencia al componente Animator
    public SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer para cambiar la imagen del personaje
    public RuntimeAnimatorController[] playersControllers; // Array de controladores de animación para cada personaje
    public Sprite[] playersRenderer; // Array de sprites para cada personaje
    void Start()
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

    // Update is called once per frame
    void Update()
    {

    }
}
