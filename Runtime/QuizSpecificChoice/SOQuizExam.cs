using System.Collections.Generic;
using System.Linq;
using VInspector;
using Meangpu.Datatype;
using UnityEngine;

namespace Meangpu.QuizExam
{
    [CreateAssetMenu(menuName = "Meangpu/SOQuiz/QuizExam")]
    public class SOQuizExam : ScriptableObject
    {
        [Header("Optional")]
        [TextArea(3, 3)] public string QuizPassage;
        [Header("AutoSetup")]
        [TextArea(8, 8)]
        public string QuizLongString;
        [ReadOnly][SerializeField] List<string> CheckQuizSeparateByNewLine;

        [Header("REAL DATA")]
        public List<QuizObject> QuestionList = new();


        public void UpdateQuizToThisDataGroup() => ActionQuiz.OnChooseNewQuizGroup?.Invoke(this);

#if UNITY_EDITOR
        [Button]
        public void CreateQuizFromString()
        {
            QuestionList.Clear();
            TrimByNewLine();
            UpdateQuizFromString();
            Debug.Log("Don't forget to tick <color=#4ec9b0>CORRECT</color> one by hand");
        }

        public QuizObject CreateQuizData(List<string> QuizList)
        {
            List<StringWithBool> answerList = new();
            for (int i = 0, length = QuizList.Count; i < length; i++)
            {
                if (i != 0 && QuizList[i].Trim().Length != 0)
                {
                    answerList.Add(new(QuizList[i], false));
                }
            }
            Debug.Log($"ADD: <color=#4ec9b0>{QuizList[0]}</color>");
            return new(QuizList[0], answerList);
        }

        public void TrimByNewLine()
        {
            /// <summary>
            /// rules first is question, then all following until new line is answer
            /// </summary>
            CheckQuizSeparateByNewLine = QuizLongString.Split("\r\n").ToList();
            for (int i = 0, length = CheckQuizSeparateByNewLine.Count; i < length; i++) CheckQuizSeparateByNewLine[i] = CheckQuizSeparateByNewLine[i].Trim();
        }

        private void UpdateQuizFromString()
        {
            List<string> nowQuizGroup = new();
            int lastQuizSeparatePos = 0;

            for (int i = 0; i < CheckQuizSeparateByNewLine.Count; i++)
            {
                if (EndOrFindNewLineQuiz(i))
                {
                    int numberOfQuiz = i - lastQuizSeparatePos;
                    for (int j = 0, length = numberOfQuiz; j < length; j++)
                    {
                        int nowIndex = lastQuizSeparatePos + j;
                        nowQuizGroup.Add(CheckQuizSeparateByNewLine[nowIndex]);
                    }
                    nowQuizGroup = nowQuizGroup.Where(stringName => stringName.Trim().Length != 0).ToList();
                    QuestionList.Add(CreateQuizData(nowQuizGroup));
                    nowQuizGroup.Clear();
                    lastQuizSeparatePos = i;
                }
            }
        }

        private bool EndOrFindNewLineQuiz(int i) => CheckQuizSeparateByNewLine[i].Trim().Length == 0 || i == CheckQuizSeparateByNewLine.Count;

#endif
    }
}