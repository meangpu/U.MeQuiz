using Meangpu.SOEvent;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] GameObject _scoreSummary;
        [Header("Event")]
        [SerializeField] SOVoidEvent _eventToRaiseWhenStart;
        [Header("Event On Start New state")]
        [SerializeField] UnityEvent _OnWaitingEvent;
        [SerializeField] UnityEvent _OnPlayingEvent;
        [SerializeField] UnityEvent _OnFinishChooseAnsEvent;
        [SerializeField] UnityEvent _OnNoMoreQuizShowSummaryEvent;

        void OnEnable() => ActionQuiz.OnNoMoreQuizToPlay += SetShowSummary;
        void OnDisable() => ActionQuiz.OnNoMoreQuizToPlay -= SetShowSummary;

        void Start() => _eventToRaiseWhenStart?.Raise();

        void SetActive(GameObject obj, bool state)
        {
            if (obj == null) return;
            obj.SetActive(state);
        }

        private void SetShowSummary()
        {
            SetActive(_scoreSummary, true);
            SetActive(_startBtn, false);
            _OnNoMoreQuizShowSummaryEvent?.Invoke();
        }

        public void SetStateOnWaiting()
        {
            SetActive(_choice, false);
            SetActive(_question, false);
            SetActive(_startBtn, true);
            SetActive(_answerCorrectWrongStatus, false);
            SetActive(_nextBtn, false);
            SetActive(_progressUI, false);
            SetActive(_scoreSummary, false);
            _OnWaitingEvent?.Invoke();
        }
        public void SetStateOnPlaying()
        {
            SetActive(_choice, true);
            SetActive(_question, true);
            SetActive(_startBtn, false);
            SetActive(_answerCorrectWrongStatus, false);
            SetActive(_nextBtn, false);
            SetActive(_progressUI, true);
            SetActive(_scoreSummary, false);
            _OnPlayingEvent?.Invoke();
        }
        public void SetStateOnFinish()
        {
            SetActive(_choice, true);
            SetActive(_question, true);
            SetActive(_startBtn, false);
            SetActive(_answerCorrectWrongStatus, true);
            SetActive(_nextBtn, true);
            SetActive(_progressUI, true);
            SetActive(_scoreSummary, false);
            _OnFinishChooseAnsEvent?.Invoke();
        }
    }
}