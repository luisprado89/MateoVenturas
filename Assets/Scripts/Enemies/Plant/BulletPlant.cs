using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 2f;// Su velocidad de la bola en el eje horizontal
    public float lifeTime = 2f;// Tiempo en segundos en el que el objeto existirá antes de destruirse
    public bool left;//Indica la dirección del movimiento de la bola true -> se mueve a la izquierda, false -> se mueve a la derecha
    void Start()
    {
        Destroy(gameObject, lifeTime);//Destruye el objeto despues de "lifeTime" segundos, evitando objetos innecesarios en la escena
    }

    // Update is called once per frame
    void Update()
    {
        if (left)//Si la variable es "left" es verdadera el objeto se mueve hacia la izquierda
        {//Movimiento hacia la izquierda teniendo en cuenta el tiempo entre frames
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {//Si "left" es falsa, se mueve hacia la derecha
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
