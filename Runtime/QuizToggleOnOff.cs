using Meangpu.SOEvent;
using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizToggleOnOff : MonoBehaviour
    {
        [Header("Object")]
        [SerializeField] GameObject _question;
        [SerializeField] GameObject _choice;
        [SerializeField] GameObject _startBtn;
        [SerializeField] GameObject _answerCorrectWrongStatus;
        [SerializeField] GameObject _nextBtn;
        [SerializeField] GameObject _progressUI;
        [Header("Event")]
        [SerializeField] SOVoidEvent _eventToRaiseWhenStart;

        void OnEnable()
        {
            ActionQuiz.OnStartQuiz += DisableCorrectWrong;
        }
        void OnDisable()
        {
            ActionQuiz.OnStartQuiz -= DisableCorrectWrong;
        }

        void Start() => _eventToRaiseWhenStart?.Raise();

        void SetActive(GameObject obj, bool state)
        {
            if (obj == null) return;
            obj.SetActive(state);
        }

        private void DisableCorrectWrong(QuizObject @object)
        {
            SetActive(_choice, true);
            SetActive(_question, true);
            SetActive(_startBtn, false);
            SetActive(_answerCorrectWrongStatus, false);
            SetActive(_nextBtn, false);
            SetActive(_progressUI, true);
        }

        public void SetStateOnWaiting()
        {
            SetActive(_choice, false);
            SetActive(_question, false);
            SetActive(_startBtn, true);
            SetActive(_answerCorrectWrongStatus, false);
            SetActive(_nextBtn, false);
            SetActive(_progressUI, false);
        }
        public void SetStateOnPlaying()
        {
            SetActive(_choice, true);
            SetActive(_question, true);
            SetActive(_startBtn, false);
            SetActive(_answerCorrectWrongStatus, false);
            SetActive(_nextBtn, false);
            SetActive(_progressUI, true);
        }
        public void SetStateOnFinish()
        {
            SetActive(_choice, true);
            SetActive(_question, true);
            SetActive(_startBtn, false);
            SetActive(_answerCorrectWrongStatus, true);
            SetActive(_nextBtn, true);
            SetActive(_progressUI, true);
        }
    }
}