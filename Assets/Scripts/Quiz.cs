using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    // variables serialized

    [Header("Preguntas:")] // variables relacionadas con las preguntas
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>(); // lista que contiene todas las preguntas
    QuestionSO currentQuestion; // objeto que tiene la pregunta actual

    [Header("Respuestas:")] // variables relacionadas con las respuestas
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Colores de botones:")] // variables relacionadas con los botones
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer:")] //variables relacionadas con el timer
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>(); //sacado del script 'timer'
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction; // bajamos la barra del timer constantemente
        if(timer.loadNextQuestion) //al cargar la siguiente pregunta obtenemos el texto y reiniciamos el timer
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false; // la hacemos falso para no obtener preguntas cada frame
        }
        // si hacemos click cuando estamos respondiendo
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) 
        {
            DisplayAnswer(-1); // ponemos un numero cualquiera porque no va a ser correcta
            SetButtonState(false); // le quitamos el control al jugador
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true; 
        DisplayAnswer(index); // mostramos las respuestas y cambiamos los sprites
        SetButtonState(false); // desactivamos la interactable para no presionar más veces el boton
        timer.CancelTimer(); // si le hacemos click a un boton hacemos que el timer sea 0
    }

    // funcion que muestra las respuesta cambiando los sprites de los botones
    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex()) //si la respuesta es correcta...
        {
            questionText.text = "¡Correcto!";
            buttonImage = answerButtons[index].GetComponent<Image>(); // hacemos highlight a la respuesta correcta
            buttonImage.sprite = correctAnswerSprite; // le cambiamos el sprite para hacer el highlight
        }
        else // cuando la respuesta no es correcta
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex(); //obtenemos el índice de la respuesta correcta
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex); // obtenemos el string de la respuesta correcta
            questionText.text = "Qué pena :( la respuesta correcta es:\n" + correctAnswer; // imprimimos mensaje de error con la respuesta correcta
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite; // hacemos highlight a la respuesta correcta
        }
    }

    // función que obtiene la siguiente pregunta y hace que los botones sean interactable
    void GetNextQuestion() 
    {
        if(questions.Count > 0) // Checamos que la lista de preguntas no esté vacia para conseguir la siguiente
        {
            SetButtonState(true); // hacemos que el boton se pueda interactuar
            SetDefaultButtonSprites(); // cambiamos a los sprites default
            GetRandomQuestion(); // cambiamos a una pregunta random
            DisplayQuestion(); // mostramos el texto de la pregunta
        }
        
    }

    // función que obtiene la siguiente pregunta en orden aleatorio
    // y quita la pregunta anterior de la lista de preguntas posibles
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    // función que obtiene el texto del objeto pregunta y muestra la pregunta y las respuestas
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion(); // obtenemos el texto del objeto pregunta

        for (int i = 0; i < answerButtons.Length; i++) // pasamos por cada uno de los botones y obtenemos el texto de las respuestas
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    // función que cambia el estado de los botones para que sean o no interactable
    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    // función que cambia los sprites de los botones a su estado default
    void SetDefaultButtonSprites()
    {

        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
