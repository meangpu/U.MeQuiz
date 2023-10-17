using System.Collections.Generic;
using EasyButtons;
using TMPro;
using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizSpawner : MonoBehaviour
    {
        [SerializeField] TMP_Text _questionTxt;
        [SerializeField] List<QuizChoice> _answerTxt;

        void OnEnable()
        {
            ActionQuiz.OnStartQuiz += SpawnQuizObject;
        }
        void OnDisable()
        {
            ActionQuiz.OnStartQuiz -= SpawnQuizObject;
        }

        [Button]
        private void SpawnQuizObject(QuizObject quizObj)
        {
            _questionTxt.SetText(quizObj.Question);
            for (var i = 0; i < quizObj.Answers.Count; i++)
            {
                _answerTxt[i].SetText(quizObj.Answers[i].stringValue);
                _answerTxt[i].SetIsCorrectAnswer(quizObj.Answers[i].boolValue);
                _answerTxt[i].ClearDisplayCorrect();
            }
        }
    }
}