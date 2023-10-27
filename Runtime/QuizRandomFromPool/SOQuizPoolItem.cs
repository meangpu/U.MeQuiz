using UnityEngine;

namespace Meangpu.QuizExam
{
    // the creation should be [CreateAssetMenu(menuName = "Meangpu/SOQuiz/...thisObjectName")]
    public abstract class SOQuizPoolItem : ScriptableObject
    {
        [TextArea]
        public string Question;
    }
}