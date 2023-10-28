namespace Meangpu.QuizExam
{
    public abstract class QuizPoolChoiceTemplate : QuizChoiceBase
    {
        public abstract void DisplayData(SOQuizPoolItem soQuizItem);

        void OnEnable() => ActionQuiz.OnPoolAnswerCorrect += OnQuizAnswer;
        void OnDisable() => ActionQuiz.OnPoolAnswerCorrect -= OnQuizAnswer;

        public override void InvokeAnswerIsCorrect()
        {
            ActionQuiz.OnPoolAnswerCorrect?.Invoke(_isCorrectAns);
        }
    }
}
