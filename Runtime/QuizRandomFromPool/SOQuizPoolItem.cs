using UnityEngine;

namespace Meangpu.QuizExam
{
    // the creation should be [CreateAssetMenu(menuName = "Meangpu/SOQuiz/...thisObjectName")]
    public abstract class SOQuizPoolItem : ScriptableObject
    {
        public string QuizName;
        [TextArea]
        public string Question;

#if UNITY_EDITOR
        private void OnValidate()
        {
            QuizName ??= name;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}