using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Meangpu.QuizExam;
using EasyButtons;
using Meangpu.Util;

namespace Meangpu.QuizPool
{
    public class QuizPoolChoiceSpawner : MonoBehaviour
    {
        [SerializeField] TMP_Text _questionTxt;
        [SerializeField] List<QuizPoolChoice> _answerTxt;

        void OnEnable()
        {
            QuizPoolAction.OnStartQuiz += SpawnQuizObject;
        }
        void OnDisable()
        {
            QuizPoolAction.OnStartQuiz -= SpawnQuizObject;
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
                _answerTxt[i].DisplaySceneBySOData(shuffleChoiceList[i]);
                _answerTxt[i].SetText(shuffleChoiceList[i].Names[0]);
            }
        }
    }
}