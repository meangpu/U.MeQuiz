using System;
using System.Collections.Generic;

namespace Meangpu.QuizExam
{
    public static class ActionQuiz
    {
        public static Action<SOQuizExam> OnChooseNewQuizGroup;
        public static Action<QuizObject> OnStartQuiz;
        public static Action OnNoMoreQuizToPlay;

        public static Action<SOQuizPoolItem, List<SOQuizPoolItem>> OnStartQuizPool;
        public static Action<bool> OnSpecificAnswerCorrect;
        public static Action<bool> OnPoolAnswerCorrect;
    }
}