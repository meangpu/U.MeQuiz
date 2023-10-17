using EasyButtons;
using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizButtonSOHolder : MonoBehaviour
    {
        [SerializeField] SOQuizExam _data;
        [Button]
        public void OnChooseThisDataGroup()
        {
            ActionQuiz.OnChooseQuizGroup?.Invoke(_data);
            QuizStateManager.Instance.UpdateGameState(QuizState.Playing);
        }
    }
}