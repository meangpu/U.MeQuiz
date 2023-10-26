using System;

namespace Meangpu.QuizExam
{
    public static class ActionQuiz
    {
        public static Action<SOQuizExam> OnChooseQuizGroup;
        public static Action<QuizObject> OnStartQuiz;
        public static Action<bool> OnAnswerQuiz;
    }
}