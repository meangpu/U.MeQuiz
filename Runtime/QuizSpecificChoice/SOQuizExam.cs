using System.Collections.Generic;
using UnityEngine;

namespace Meangpu.QuizExam
{
    [CreateAssetMenu(menuName = "Meangpu/SOQuiz/QuizExam")]
    public class SOQuizExam : ScriptableObject
    {
        public List<QuizObject> QuestionList;
    }
}