using TMPro;
using UnityEngine;
using Meangpu.Util;
using System;
using UnityEngine.UI;
using EasyButtons;

namespace Meangpu.QuizExam
{
    public class QuizChoice : MonoBehaviour
    {
        [SerializeField] TMP_Text _txt;
        [SerializeField] ButtonUIStatus _status;
        [SerializeField] Image _imageYouChoose;
        [SerializeField] Button _btn;
        [SerializeField] Color _chooseColor;

        public bool _isCorrectAns;
        void OnEnable() => ActionQuiz.OnAnswerQuiz += OnQuizAnswer;
        void OnDisable() => ActionQuiz.OnAnswerQuiz -= OnQuizAnswer;

        private void OnQuizAnswer(bool isCorrect)
        {
            _btn.interactable = false;
            if (!isCorrect) DisplayIsAnswerCorrect();
        }

        public void SetText(string txt) => _txt.SetText(txt);

        public bool GetIsCorrectAnswer() => _isCorrectAns;
        public bool SetIsCorrectAnswer(bool newBool) => _isCorrectAns = newBool;

        public void ClearDisplayCorrect()
        {
            _status.SetButtonByStatus(false);
            _btn.interactable = true;
        }

        public void DisplayIsAnswerCorrect()
        {
            _status.SetButtonByStatus(_isCorrectAns);
            _btn.interactable = false;
        }

        [Button]
        public void SelectThisChoice()
        {
            ActionQuiz.OnAnswerQuiz?.Invoke(_isCorrectAns);
            QuizStateManager.Instance.UpdateGameState(QuizState.Finish);
            _imageYouChoose.color = _chooseColor;
        }
    }
}