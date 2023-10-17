using System;
using EasyButtons;
// using MoreMountains.Feedbacks;
using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizStateManager : MonoBehaviour
    {
        public static QuizStateManager Instance;
        public static Action<QuizState> OnUpdateGameState;
        public QuizState State;
        // [Header("mmf player")]
        // [SerializeField] MMF_Player _fbWait;
        // [SerializeField] MMF_Player _fbPlaying;
        // [SerializeField] MMF_Player _fbFinish;
        // [Header("Event all")]
        // [SerializeField] MMF_Player _fbEvent;

        private void Awake() => Instance = this;
        private void Start() => UpdateGameState(QuizState.Waiting);

        [Button]
        public void StartGame() => UpdateGameState(QuizState.Playing);

        [Button]
        public void UpdateGameState(QuizState newState)
        {
            Debug.Log($"{newState}");
            State = newState;

            OnUpdateGameState?.Invoke(newState);
            // _fbEvent?.PlayFeedbacks();
            switch (newState)
            {
                case QuizState.Waiting:
                    // _fbWait?.PlayFeedbacks();
                    break;
                case QuizState.Playing:
                    // _fbPlaying?.PlayFeedbacks();
                    break;
                case QuizState.Finish:
                    // _fbFinish?.PlayFeedbacks();
                    break;
            }
        }
    }

    public enum QuizState
    {
        Waiting, Playing, Finish
    }
}