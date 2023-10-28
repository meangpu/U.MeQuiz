using System.Collections.Generic;
using EasyButtons;
using Meangpu.Audio;
using Meangpu.SOEvent;
using Meangpu.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.QuizExam
{
    public class QuizDataHolder : MonoBehaviour, IGameEventListener<Void>
    {
        [Expandable][SerializeField] SOQuizExam _data;
        [ReadOnly][SerializeField] List<QuizObject> _quizList = new();
        [SerializeField] ButtonUIStatus _answerStatus;
        [Header("audio")]
        [SerializeField] SOSound _correctSound;
        [SerializeField] SOSound _wrongSound;
        [Header("progress")]
        [SerializeField] Slider _uiSlider;
        [SerializeField] TMP_Text _scoreNow;
        [SerializeField] TMP_Text _scoreMax;
        int _score;
        [Header("Event")]
        [SerializeField] SOVoidEvent _OnWaitEvent;
        [SerializeField] SOVoidEvent _OnPlayingEvent;

        public void OnEventRaised(Void data) => StartNextQuiz();

        void OnEnable()
        {
            _OnPlayingEvent.RegisterListener(this);
            ActionQuiz.OnSpecificAnswerCorrect += UpdateAnswerCorrect;
            ActionQuiz.OnChooseQuizGroup += UpdateQuizGroup;
        }

        void OnDisable()
        {
            _OnPlayingEvent.UnregisterListener(this);
            ActionQuiz.OnSpecificAnswerCorrect -= UpdateAnswerCorrect;
            ActionQuiz.OnChooseQuizGroup -= UpdateQuizGroup;
        }

        private void UpdateQuizGroup(SOQuizExam exam)
        {
            _data = exam;
            SetupQuizPool();
            _score = 0;
            SetupUI(_data);
        }

        private void UpdateAnswerCorrect(bool isAnswerCorrect)
        {
            _answerStatus.SetButtonByStatus(isAnswerCorrect);

            if (isAnswerCorrect)
            {
                _correctSound?.PlayOneShot();
                _score++;
                UpdateNowScoreUI();
                UpdateProgressUI();
            }
            else
            {
                _wrongSound?.PlayOneShot();
                UpdateNowScoreUI();
                UpdateProgressUI();
            }
        }

        private void SetupQuizPool() => _quizList = new(_data.QuestionList);

        void UpdateNowScoreUI()
        {
            if (_scoreNow == null) return;
            _scoreNow.SetText(_score.ToString());
        }

        void SetupUI(SOQuizExam _quizData)
        {
            if (_uiSlider == null) return;
            _uiSlider.value = 0;
            _uiSlider.maxValue = _quizData.QuestionList.Count;
            _scoreNow.SetText("0");
            _scoreMax.SetText($"/{_quizData.QuestionList.Count}");
        }

        void UpdateProgressUI()
        {
            if (_uiSlider == null) return;
            _uiSlider.value = _data.QuestionList.Count - _quizList.Count;
        }

        public QuizObject GetRandomQuiz()
        {
            if (_quizList.Count == 0) SetupQuizPool();
            int indexList = Random.Range(0, _quizList.Count);
            QuizObject nowObj = _quizList[indexList];
            _quizList.RemoveAt(indexList);
            return nowObj;
        }

        public QuizObject GetNextQuiz()
        {
            IsQuizIsFinish();

            QuizObject nowObj = _quizList[0];
            _quizList.RemoveAt(0);
            return nowObj;
        }

        private bool IsQuizIsFinish()
        {
            if (_quizList.Count == 0)
            {
                _OnWaitEvent?.Raise();
                return true;
            }
            return false;
        }

        [Button]
        public void StartNextQuiz()
        {
            if (IsQuizIsFinish()) return;
            ActionQuiz.OnStartQuiz?.Invoke(GetNextQuiz());
        }
    }
}