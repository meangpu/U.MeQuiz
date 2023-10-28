using UnityEngine;
using Meangpu.Util;
using System.Collections.Generic;
using Meangpu.Audio;
using Meangpu.SOEvent;

namespace Meangpu.QuizExam
{
    public class QuizPoolInfoHolder : MonoBehaviour, IGameEventListener<Void>
    {
        [SerializeField] List<SOQuizPoolItem> _quizQuestionGroup;

        List<SOQuizPoolItem> _questionListPool = new();
        List<SOQuizPoolItem> _choiceListPool = new();

        [SerializeField] ButtonUIStatus _answerStatus;
        [SerializeField] int _choiceCount = 4;
        [Header("audio")]
        [SerializeField] SOSound _startQuizSound;
        [SerializeField] SOSound _correctSound;
        [SerializeField] SOSound _wrongSound;

        [Header("Event")]
        [SerializeField] SOVoidEvent _OnPlayEvent;

        void OnEnable()
        {
            ActionQuiz.OnPoolAnswerCorrect += UpdateAnswerCorrect;
            _OnPlayEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            ActionQuiz.OnPoolAnswerCorrect -= UpdateAnswerCorrect;
            _OnPlayEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Void data) => StartQuiz();

        public void StartQuiz()
        {
            _startQuizSound?.Play();
            SOQuizPoolItem question = GetRandomQuestion();
            List<SOQuizPoolItem> choiceList = GetChoiceList(question);
            ActionQuiz.OnStartQuizPool?.Invoke(question, choiceList);
        }

        private void UpdateAnswerCorrect(bool isAnswerCorrect)
        {
            _answerStatus.SetButtonByStatus(isAnswerCorrect);
            if (isAnswerCorrect)
            {
                _correctSound?.Play();
            }
            else
            {
                _wrongSound?.Play();
            }
        }

        private void ResetChoicePool() => _choiceListPool = new(_quizQuestionGroup);
        private void ResetQuestionPool() => _questionListPool = new(_quizQuestionGroup);

        List<SOQuizPoolItem> GetChoiceList(SOQuizPoolItem correctAnswer)
        {
            ResetChoicePool();
            List<SOQuizPoolItem> outputChoice = new();
            AddCorrectAnswer(correctAnswer, outputChoice);
            AddWrongAnswerRandomly(outputChoice);

            return outputChoice;

            void AddToChoiceByIndex(List<SOQuizPoolItem> outputChoice, int indexList)
            {
                SOQuizPoolItem nowObj = _choiceListPool[indexList];
                outputChoice.Add(nowObj);
                _choiceListPool.RemoveAt(indexList);
            }

            void AddCorrectAnswer(SOQuizPoolItem correctAnswer, List<SOQuizPoolItem> outputChoice)
            {
                int correctAnswerPosition = _choiceListPool.IndexOf(correctAnswer);
                AddToChoiceByIndex(outputChoice, correctAnswerPosition);
            }

            void AddWrongAnswerRandomly(List<SOQuizPoolItem> outputChoice)
            {
                for (var i = 0; i < _choiceCount - 1; i++)
                {
                    int indexList = Random.Range(0, _choiceListPool.Count);
                    AddToChoiceByIndex(outputChoice, indexList);
                }
            }
        }

        SOQuizPoolItem GetRandomQuestion()
        {
            if (_questionListPool.Count == 0) ResetQuestionPool();

            int indexList = Random.Range(0, _questionListPool.Count);
            SOQuizPoolItem nowObj = _questionListPool[indexList];
            _questionListPool.RemoveAt(indexList);
            return nowObj;
        }
    }
}