using TMPro;
using UnityEngine;
using Meangpu.Util;
using UnityEngine.UI;
using EasyButtons;
using Meangpu.SOEvent;

namespace Meangpu.QuizExam
{
    public class QuizChoice : MonoBehaviour
    {
        [SerializeField] TMP_Text _txt;
        [SerializeField] ButtonUIStatus _status;
        [SerializeField] Image _imageShowYouChoose;
        [SerializeField] Button _btn;
        [SerializeField] Color _chooseColor;

        [Header("Event")]
        [SerializeField] SOVoidEvent _OnFinishEvent;

        public bool _isCorrectAns;

        void OnEnable() => ActionQuiz.OnAnswerCorrect += OnQuizAnswer;
        void OnDisable() => ActionQuiz.OnAnswerCorrect -= OnQuizAnswer;

        private void OnQuizAnswer(bool isCorrect)
        {
            _btn.interactable = false;
            if (!isCorrect) DisplayIsAnswerCorrect();
        }

        void Start()
        {
            _btn.onClick.AddListener(SelectThisChoice);
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
            ActionQuiz.OnAnswerCorrect?.Invoke(_isCorrectAns);
            _OnFinishEvent?.Raise();
            _imageShowYouChoose.color = _chooseColor;
        }
    }
}