using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizChoiceSpecific : QuizChoiceBase
    {
        void OnEnable() => ActionQuiz.OnSpecificAnswerCorrect += OnQuizAnswer;
        void OnDisable() => ActionQuiz.OnSpecificAnswerCorrect -= OnQuizAnswer;

        public override void InvokeAnswerIsCorrect()
        {
            ActionQuiz.OnSpecificAnswerCorrect?.Invoke(_isCorrectAns);
        }
    }
}