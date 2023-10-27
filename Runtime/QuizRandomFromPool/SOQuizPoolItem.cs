using UnityEngine;

namespace Meangpu.QuizExam
{
    public abstract class SOQuizPoolItem : ScriptableObject
    {
        [TextArea]
        public string Question;
    }
}