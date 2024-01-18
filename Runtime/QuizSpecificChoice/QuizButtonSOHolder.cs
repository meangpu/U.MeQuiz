using VInspector;
using Meangpu.SOEvent;
using UnityEngine;

namespace Meangpu.QuizExam
{
    // for if have multiple quiz group that need to be able to choose using button
    public class QuizButtonSOHolder : MonoBehaviour
    {
        [Expandable][SerializeField] SOQuizExam _data;
        [SerializeField] SOVoidEvent _OnPlayingEvent;

        [Button]
        public void OnChooseThisButtonDataGroup()
        {
            ActionQuiz.OnChooseNewQuizGroup?.Invoke(_data);
            _OnPlayingEvent?.Raise();
        }
    }
}