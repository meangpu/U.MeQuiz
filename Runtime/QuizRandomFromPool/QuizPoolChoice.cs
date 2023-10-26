using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using TMPro;
using Meangpu.Util;
using System.Collections.Generic;

namespace Meangpu.QuizPool
{
    public class QuizPoolChoice : MonoBehaviour
    {
        [SerializeField] TMP_Text _txt;
        [SerializeField] ButtonUIStatus _status;
        [SerializeField] Image _imageYouChoose;
        [SerializeField] Button _btn;
        [SerializeField] Color _chooseColor;
        [SerializeField] List<GameObject> _childObj;

        public bool _isCorrectAns;
        void OnEnable() => QuizPoolAction.OnAnswerCorrect += OnQuizAnswer;
        void OnDisable() => QuizPoolAction.OnAnswerCorrect -= OnQuizAnswer;

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
            ActionQuizLocation.OnAnswerCorrect?.Invoke(_isCorrectAns);
            LocationQuizStateManager.Instance.UpdateGameState(LocationQuizState.Finish);
            _imageYouChoose.color = _chooseColor;
        }

        [Button]
        public void DisplaySceneBySOData(SOU4 data)
        {
            DisableAllScene();
            GameObject sceneNow = _childObj.Find((x) => x.name == data.name);
            sceneNow?.SetActive(true);
        }

        void DisableAllScene()
        {
            foreach (GameObject child in _childObj)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
