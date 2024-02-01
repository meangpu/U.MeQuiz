using TMPro;
using UnityEngine;
using Meangpu.Util;
using UnityEngine.UI;
using VInspector;
using Meangpu.SOEvent;

namespace Meangpu.QuizExam
{
    public abstract class QuizChoiceBase : MonoBehaviour
    {
        [SerializeField] TMP_Text _txt;
        [SerializeField] ButtonUIStatus _status;
        [SerializeField] Image _imageShowYouChoose;
        [SerializeField] Button _btn;
        [SerializeField] Color _chooseColor;

        [Header("Event")]
        [SerializeField] SOVoidEvent _OnFinishEvent;

        [Header("If choose correct")]
        [SerializeField] bool _overrideSelectColorWithCorrectAnswerColorIfCorrect;
        [ShowIf("_overrideSelectColorWithCorrectAnswerColorIfCorrect")]
        [SerializeField] Color _chooseAndCorrectAnswer;
        [EndIf]

        public bool _isCorrectAns;

        protected void OnQuizAnswer(bool isCorrect)
        {
            _btn.interactable = false;
            if (!isCorrect) DisplayIsAnswerCorrect();
        }

        void Start() => _btn.onClick.AddListener(SelectThisChoice);

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

        public abstract void InvokeAnswerIsCorrect();

        [Button]
        public void SelectThisChoice()
        {
            InvokeAnswerIsCorrect();
            _OnFinishEvent?.Raise();
            _imageShowYouChoose.color = _chooseColor;
            if (_isCorrectAns && _overrideSelectColorWithCorrectAnswerColorIfCorrect) _imageShowYouChoose.color = _chooseAndCorrectAnswer;
        }
    }
}