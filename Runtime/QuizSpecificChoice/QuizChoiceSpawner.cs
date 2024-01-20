using System.Collections.Generic;
using VInspector;
using TMPro;
using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizChoiceSpawner : MonoBehaviour
    {
        [SerializeField] TMP_Text _questionTxt;
        [SerializeField] List<QuizChoiceSpecific> _answerTxt;
        [SerializeField] Transform _choiceParent;
        [SerializeField] QuizChoiceSpecific _quizChoiceTemplate;

        void OnEnable()
        {
            ActionQuiz.OnStartQuiz += SpawnQuizObject;
        }
        void OnDisable()
        {
            ActionQuiz.OnStartQuiz -= SpawnQuizObject;
        }

        public void DisableAllChoiceBtn()
        {
            foreach (Transform child in _choiceParent) child.gameObject.SetActive(false);
        }

        public void SpawnChoiceToCount(int questionCount)
        {
            while (_answerTxt.Count < questionCount)
            {
                QuizChoiceSpecific newChoice = Instantiate(_quizChoiceTemplate, _choiceParent);
                _answerTxt.Add(newChoice);
            }
        }

        [Button]
        private void SpawnQuizObject(QuizObject quizObj)
        {
            _questionTxt.SetText(quizObj.Question);
            DisableAllChoiceBtn();
            SpawnChoiceToCount(quizObj.Answers.Count);

            for (var i = 0; i < quizObj.Answers.Count; i++)
            {
                QuizChoiceBase nowChoice = _answerTxt[i];
                nowChoice.gameObject.SetActive(true);
                nowChoice.SetText(quizObj.Answers[i].stringValue);
                nowChoice.SetIsCorrectAnswer(quizObj.Answers[i].boolValue);
                nowChoice.ClearDisplayCorrect();
            }
        }
    }
}