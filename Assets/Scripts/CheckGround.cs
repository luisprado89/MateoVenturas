using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded; // Variable para verificar si el jugador está en el suelo

    void OnTriggerEnter2D(Collider2D collision)
    {

        isGrounded = true; // El jugador ha tocado el suelo

    }

    void OnTriggerExit2D(Collider2D collision)
    {

        isGrounded = false; // El jugador ha dejado de tocar el suelo
    }

}
