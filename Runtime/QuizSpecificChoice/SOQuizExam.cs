using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using Meangpu.Datatype;
using UnityEngine;

namespace Meangpu.QuizExam
{
    [CreateAssetMenu(menuName = "Meangpu/SOQuiz/QuizExam")]
    public class SOQuizExam : ScriptableObject
    {
        [Header("AutoSetup")]
        [TextArea(8, 8)]
        public string QuizLongString;
        public List<string> ManualCleanUp;

        [Header("real data")]
        public List<QuizObject> QuestionList = new();

#if UNITY_EDITOR

        [Button]
        public void CreateQuizFromString()
        {
            QuestionList.Clear();
            TrimByNewLine();
            UpdateQuizFromString();
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
            return new(QuizList[0], answerList);
        }

        public void TrimByNewLine()
        {
            /// <summary>
            /// rules first is question, then all following until new line is answer
            /// </summary>
            ManualCleanUp = QuizLongString.Split("\r\n").ToList();
            for (int i = 0, length = ManualCleanUp.Count; i < length; i++) ManualCleanUp[i] = ManualCleanUp[i].Trim();
        }

        private void UpdateQuizFromString()
        {
            List<string> nowQuizGroup = new();
            int lastQuizSeparatePos = 0;

            for (int i = 0; i < ManualCleanUp.Count; i++)
            {
                if (EndOrFindNewLineQuiz(i))
                {
                    int numberOfQuiz = i - lastQuizSeparatePos;
                    for (int j = 0, length = numberOfQuiz; j < length; j++)
                    {
                        int nowIndex = lastQuizSeparatePos + j;
                        nowQuizGroup.Add(ManualCleanUp[nowIndex]);
                    }
                    nowQuizGroup = nowQuizGroup.Where(stringName => stringName.Trim().Length != 0).ToList();
                    QuestionList.Add(CreateQuizData(nowQuizGroup));
                    nowQuizGroup.Clear();
                    lastQuizSeparatePos = i;
                }
            }
        }

        private bool EndOrFindNewLineQuiz(int i) => ManualCleanUp[i].Trim().Length == 0 || i == ManualCleanUp.Count;

#endif
    }
}