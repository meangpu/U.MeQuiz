using System.Collections.Generic;
using EasyButtons;
using Meangpu.Audio;
using Meangpu.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.QuizExam
{
    public class QuizDataHolder : MonoBehaviour
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

        void OnEnable()
        {
            QuizStateManager.OnUpdateGameState += OnGameUpdateState;
            ActionQuiz.OnAnswerQuiz += UpdateAnswerCorrect;
            ActionQuiz.OnChooseQuizGroup += UpdateQuizGroup;
        }

        void OnDisable()
        {
            QuizStateManager.OnUpdateGameState -= OnGameUpdateState;
            ActionQuiz.OnAnswerQuiz -= UpdateAnswerCorrect;
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

        private void OnGameUpdateState(QuizState state)
        {
            switch (state)
            {
                case QuizState.Playing:
                    StartNextQuiz();
                    break;
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
            if (_quizList.Count == 0)
            {
                QuizStateManager.Instance.UpdateGameState(QuizState.Waiting);
            }
            QuizObject nowObj = _quizList[0];
            _quizList.RemoveAt(0);
            return nowObj;
        }

        [Button]
        public void StartNextQuiz()
        {
            if (_quizList.Count == 0)
            {
                QuizStateManager.Instance.UpdateGameState(QuizState.Waiting);
                return;
            }

            ActionQuiz.OnStartQuiz?.Invoke(GetNextQuiz());
        }
    }
}