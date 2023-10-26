using System;
using EasyButtons;
using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizStateManager : MonoBehaviour
    {
        public static QuizStateManager Instance;
        public static Action<QuizState> OnUpdateGameState;
        public QuizState State;

        private void Awake() => Instance = this;
        private void Start() => UpdateGameState(QuizState.Waiting);

        [Button]
        public void StartGame() => UpdateGameState(QuizState.Playing);

        [Button]
        public void UpdateGameState(QuizState newState)
        {
            State = newState;
            OnUpdateGameState?.Invoke(newState);
        }
    }

    public enum QuizState
    {
        Waiting, Playing, Finish
    }
}