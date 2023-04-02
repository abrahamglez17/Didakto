using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    // variables serialized
    [TextArea(2, 6)]
    [SerializeField] string question = "Escriba la nueva pregunta aquí";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    // variables

    // métodos

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex; //obtenemos el indice del elemento que contiene la respuesta
    }

    public string GetAnswer(int index)
    {
        return answers[index]; // regresamos la respuesta correcta del array de respuestas con el index
    }
}
