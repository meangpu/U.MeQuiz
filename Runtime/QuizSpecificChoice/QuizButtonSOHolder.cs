using EasyButtons;
using Meangpu.SOEvent;
using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizButtonSOHolder : MonoBehaviour
    {
        [Expandable][SerializeField] SOQuizExam _data;
        [SerializeField] SOVoidEvent _OnPlayingEvent;

        [Button]
        public void OnChooseThisDataGroup()
        {
            ActionQuiz.OnChooseQuizGroup?.Invoke(_data);
            _OnPlayingEvent?.Raise();
        }
    }
}