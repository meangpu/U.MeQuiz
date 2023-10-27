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
        [SerializeField] List<QuizPoolChoiceTemplate> _answerTxt;

        void OnEnable()
        {
            ActionQuiz.OnStartQuizPool += SpawnQuizObject;
        }
        void OnDisable()
        {
            ActionQuiz.OnStartQuizPool -= SpawnQuizObject;
        }

        [Button]
        private void SpawnQuizObject(SOQuizPoolItem question, List<SOQuizPoolItem> choice)
        {
            _questionTxt.SetText(question.Question);

            List<SOQuizPoolItem> shuffleChoiceList = new(choice);
            ListOP.Shuffle(shuffleChoiceList);

            for (var i = 0; i < shuffleChoiceList.Count; i++)
            {
                _answerTxt[i].ClearDisplayCorrect();
                if (shuffleChoiceList[i] == question)
                {
                    _answerTxt[i].SetIsCorrectAnswer(true);
                }
                else
                {
                    _answerTxt[i].SetIsCorrectAnswer(false);
                }
                _answerTxt[i].ClearDisplayCorrect();
                _answerTxt[i].SetText(shuffleChoiceList[i].name);
                _answerTxt[i].DisplayData(shuffleChoiceList[i]);
            }
        }
    }
}