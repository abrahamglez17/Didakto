using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // variables serialized
    [SerializeField] float timeToCompleteQuestion = 5f; // 5 segundos para responder la pregunta
    [SerializeField] float timeToShowCorrectAnswer = 10f; // 10 segundos para ver la respuesta correcta

    // variables públicas
    public bool loadNextQuestion; // estado para cargar la siguiente pregunta
    public float fillFraction; // va tomar el valor del tiempo y lo va a normalizar entre 1 y 0 

    // variables privadas
    public bool isAnsweringQuestion; //estado del jugador actualmente
    float timerValue; // valor del timer

    void Update()
    {
        UpdateTimer(); // actualizamos el timer todo el tiempo
    }

    //función que llamaremos desde quiz.cs para poner el timer en 0
    public void CancelTimer()
    {
        timerValue = 0;
    }

    // función que actualiza el valor del timer dependiendo si está respondiendo preguntas o
    // viendo la respuesta correcta tras responder la pregunta
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime; // reducimos el valor del tiempo siempre.

        if(isAnsweringQuestion) // bloque de código cuando estamos respondiendo las preguntas
        {
            if(timerValue > 0) // normalizamos el fill para la llenar imagen
            {
                fillFraction = timerValue / timeToCompleteQuestion; // valores entre 0 y 1
            }
            else // cuando se acaba el timpo cambiamos de estado y ponemos el tiempo para ver las respuestas
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else // bloque de código cuando estamos viendo la respuesta correcta
        {
            if(timerValue > 0) // normalizamos el fill para llenar la imagen
            {
                fillFraction = timerValue / timeToShowCorrectAnswer; // valores entre 0 y 1
            }
            else // cuando se acaba el tiempo pasamos a la siguiente pregunta y cambiamos el tiempo para responder la pregunta
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
