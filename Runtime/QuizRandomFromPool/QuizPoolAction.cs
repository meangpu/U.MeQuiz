using System;
using System.Collections.Generic;

namespace Meangpu.QuizPool
{
    public static class QuizPoolAction
    {
        public static Action<SOU4> OnChooseAnswer;
        public static Action<SOU4, List<SOU4>> OnStartQuiz;
        public static Action<bool> OnAnswerCorrect;
    }
}