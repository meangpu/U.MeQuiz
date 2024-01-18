using System.Collections;
using System.Collections.Generic;
using VInspector;
using Meangpu.Audio;
using Meangpu.SOEvent;
using Meangpu.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.QuizExam
{
    public class QuizDataHolder : MonoBehaviour, IGameEventListener<Void>
    {
        [Expandable][SerializeField] SOQuizExam _data;
        [ReadOnly][SerializeField] List<QuizObject> _quizList = new();
        [SerializeField] ButtonUIStatus _answerStatus;
        [Header("audio")]
        [SerializeField] SOSound _correctSound;
        [SerializeField] SOSound _wrongSound;
        [Header("Event")]
        [SerializeField] SOVoidEvent _OnWaitEvent;
        [SerializeField] SOVoidEvent _OnPlayingEvent;
        [SerializeField] QuizScoreManager _scoreScpt;

        public void OnEventRaised(Void data) => StartNextQuiz();
        public void DoStartGame() => _OnPlayingEvent?.Raise();

        void OnEnable()
        {
            _OnPlayingEvent.RegisterListener(this);
            ActionQuiz.OnSpecificAnswerCorrect += UpdateAnswerCorrect;
            ActionQuiz.OnChooseNewQuizGroup += UpdateQuizGroup;
        }

        void OnDisable()
        {
            _OnPlayingEvent.UnregisterListener(this);
            ActionQuiz.OnSpecificAnswerCorrect -= UpdateAnswerCorrect;
            ActionQuiz.OnChooseNewQuizGroup -= UpdateQuizGroup;
        }

        public void UpdateQuizGroup(SOQuizExam exam)
        {
            _data = exam;
            InitNowHoldingQuiz();
        }

        private void InitNowHoldingQuiz()
        {
            SetupQuizPool();
            _scoreScpt.InitScoreAndUI(_data);
        }

        public void StartGameWithCurrentQuizGroup()
        {
            InitNowHoldingQuiz();
            DoStartGame();
        }

        private void UpdateAnswerCorrect(bool isAnswerCorrect)
        {
            _answerStatus.SetButtonByStatus(isAnswerCorrect);

            if (isAnswerCorrect)
            {
                _correctSound?.PlayOneShot();
            }
            else
            {
                _wrongSound?.PlayOneShot();
            }
        }

        private void SetupQuizPool() => _quizList = new(_data.QuestionList);

        public QuizObject GetRandomQuiz()
        {
            if (_quizList.Count.Equals(0)) SetupQuizPool();
            int indexList = Random.Range(0, _quizList.Count);
            QuizObject nowObj = _quizList[indexList];
            _quizList.RemoveAt(indexList);
            return nowObj;
        }

        IEnumerator FinishQuiz()
        {
            yield return new WaitForSeconds(0.1f);
            _OnWaitEvent?.Raise();
            ActionQuiz.OnNoMoreQuizToPlay?.Invoke();
        }

        [Button]
        public void StartNextQuiz()
        {
            if (_quizList.Count.Equals(0))
            {
                StartCoroutine(FinishQuiz());
                return;
            }

            QuizObject nowObj = _quizList[0];
            _quizList.RemoveAt(0);
            ActionQuiz.OnStartQuiz?.Invoke(nowObj);
        }
    }
}