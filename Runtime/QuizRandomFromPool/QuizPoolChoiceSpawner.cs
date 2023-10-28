using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using EasyButtons;
using Meangpu.Util;

namespace Meangpu.QuizExam
{
    public class QuizPoolChoiceSpawner : MonoBehaviour
    {
        [SerializeField] TMP_Text _questionTxt;
        [SerializeField] List<QuizPoolChoiceTemplate> _choice;

        [SerializeField] Transform _choiceParent;
        [SerializeField] QuizPoolChoiceTemplate _quizChoiceTemplatePrefab;

        void OnEnable()
        {
            ActionQuiz.OnStartQuizPool += SpawnQuizObject;
        }
        void OnDisable()
        {
            ActionQuiz.OnStartQuizPool -= SpawnQuizObject;
        }

        public void DisableAllChoiceBtn()
        {
            foreach (Transform child in _choiceParent) child.gameObject.SetActive(false);
        }

        public void SpawnChoiceToCount(int questionCount)
        {
            while (_choice.Count < questionCount)
            {
                QuizPoolChoiceTemplate newChoice = Instantiate(_quizChoiceTemplatePrefab, _choiceParent);
                _choice.Add(newChoice);
            }
        }

        [Button]
        private void SpawnQuizObject(SOQuizPoolItem question, List<SOQuizPoolItem> choice)
        {
            _questionTxt.SetText(question.Question);

            List<SOQuizPoolItem> shuffleChoiceList = new(choice);
            ListOP.Shuffle(shuffleChoiceList);

            SpawnChoiceToCount(choice.Count);
            DisableAllChoiceBtn();

            for (var i = 0; i < shuffleChoiceList.Count; i++)
            {
                _choice[i].ClearDisplayCorrect();
                _choice[i].gameObject.SetActive(true);
                if (shuffleChoiceList[i].Equals(question))
                {
                    _choice[i].SetIsCorrectAnswer(true);
                }
                else
                {
                    _choice[i].SetIsCorrectAnswer(false);
                }
                _choice[i].SetText(shuffleChoiceList[i].QuizName);
                _choice[i].DisplayData(shuffleChoiceList[i]);
            }
        }
    }
}