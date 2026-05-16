using UnityEngine;

public class JumpDebugger : MonoBehaviour
{
    private float startY;
    private float maxY;
    private bool isJumping = false;

    void Update()
    {
        // Cuando empieza a saltar (sale del suelo)
        if (!CheckGround.isGrounded && !isJumping)
        {
            isJumping = true;
            startY = transform.position.y;
            maxY = startY;
        }

        // Mientras está en el aire, guardamos la altura máxima
        if (isJumping)
        {
            if (transform.position.y > maxY)
            {
                maxY = transform.position.y;
            }
        }

        // Cuando vuelve al suelo → mostramos resultado
        if (CheckGround.isGrounded && isJumping)
        {
            isJumping = false;

            float jumpHeight = maxY - startY;

            //Debug.Log("ALTURA SALTO: " + jumpHeight);// Muestra la altura del salto en la consola
        }
    }
}