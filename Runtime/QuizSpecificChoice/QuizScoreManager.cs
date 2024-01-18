using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meangpu.QuizExam
{
    public class QuizScoreManager : MonoBehaviour
    {
        [Header("Slider Group Progress")]
        [SerializeField] Slider _uiSlider;
        [SerializeField] TMP_Text _scoreNow;
        [SerializeField] TMP_Text _scoreMax;
        [Header("Finish Game Group")]
        [SerializeField] TMP_Text _finPlayerScore;
        [SerializeField] TMP_Text _finMaxScore;
        [SerializeField] string _finMaxScoreStartWord = "out of";

        [SerializeField] UnityEvent _OnGetMaxScore;
        [SerializeField] UnityEvent _ResetMaxScoreEvent;

        int _score;
        int _maxScore;
        int _nowQuizIndex;

        void OnEnable() => ActionQuiz.OnSpecificAnswerCorrect += UpdateAnswerCorrect;
        void OnDisable() => ActionQuiz.OnSpecificAnswerCorrect -= UpdateAnswerCorrect;

        private void UpdateAnswerCorrect(bool isAnswerCorrect)
        {
            if (isAnswerCorrect) _score++;

            _nowQuizIndex++;
            UpdateNowScoreUI();
            UpdateProgressUI();
            CheckIfGotMaxScore();
        }

        void CheckIfGotMaxScore()
        {
            if (_score != _maxScore) return;
            _OnGetMaxScore?.Invoke();
        }

        void UpdateNowScoreUI()
        {
            if (_scoreNow == null) return;
            string scoreStr = _score.ToString();
            _scoreNow.SetText(scoreStr);
            _finPlayerScore.SetText(scoreStr);
        }

        public void InitScoreAndUI(SOQuizExam _quizData)
        {
            _score = 0;
            _nowQuizIndex = 0;
            _maxScore = _quizData.QuestionList.Count;
            _ResetMaxScoreEvent?.Invoke();

            if (_uiSlider == null) return;
            _uiSlider.value = 0;
            _uiSlider.maxValue = _maxScore;

            _scoreNow.SetText("0");
            _scoreMax.SetText($"/{_maxScore}");
            _finMaxScore.SetText($"{_finMaxScoreStartWord} {_maxScore}");
        }

        void UpdateProgressUI()
        {
            if (_uiSlider == null) return;
            _uiSlider.value = _nowQuizIndex;
        }
    }
}