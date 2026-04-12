using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded; // Variable para verificar si el jugador está en el suelo

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))// Verificar si el jugador ha tocado un objeto con la etiqueta "Ground" para determinar si está en el suelo
        {
        isGrounded = true; // El jugador ha tocado el suelo 
            
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) // Verificar si el jugador ha dejado de tocar un objeto con la etiqueta "Ground" para determinar si ha dejado el suelo
        {       
        isGrounded = false; // El jugador ha dejado de tocar el suelo
        }
    }
    

}
