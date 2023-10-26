using System.Collections.Generic;
using Meangpu.Datatype;
using UnityEngine;

namespace Meangpu.QuizExam
{
    [System.Serializable]
    public struct QuizObject
    {
        [TextArea]
        public string Question;
        public List<StringWithBool> Answers;
    }
}