using UnityEngine;

namespace Meangpu.QuizExam
{
    public class QuizToggleOnOff : MonoBehaviour
    {
        [SerializeField] GameObject _question;
        [SerializeField] GameObject _choice;
        [SerializeField] GameObject _startBtn;
        [SerializeField] GameObject _answerCorrectWrong;
        [SerializeField] GameObject _nextBtn;
        [SerializeField] GameObject _progressUI;

        void OnEnable()
        {
            QuizStateManager.OnUpdateGameState += UpdateState;
            ActionQuiz.OnStartQuiz += DisableCorrectWrong;
        }
        void OnDisable()
        {
            QuizStateManager.OnUpdateGameState -= UpdateState;
            ActionQuiz.OnStartQuiz -= DisableCorrectWrong;
        }

        void SetActive(GameObject obj, bool state)
        {
            if (obj == null) return;
            obj.SetActive(state);
        }

        private void DisableCorrectWrong(QuizObject @object)
        {
            SetActive(_choice, true);
            SetActive(_question, true);
            SetActive(_startBtn, false);
            SetActive(_answerCorrectWrong, false);
            SetActive(_nextBtn, false);
            SetActive(_progressUI, true);
        }

        private void UpdateState(QuizState state)
        {
            switch (state)
            {
                case QuizState.Waiting:
                    SetActive(_choice, false);
                    SetActive(_question, false);
                    SetActive(_startBtn, true);
                    SetActive(_answerCorrectWrong, false);
                    SetActive(_nextBtn, false);
                    SetActive(_progressUI, false);
                    break;
                case QuizState.Playing:
                    SetActive(_choice, true);
                    SetActive(_question, true);
                    SetActive(_startBtn, false);
                    SetActive(_answerCorrectWrong, false);
                    SetActive(_nextBtn, false);
                    SetActive(_progressUI, true);
                    break;
                case QuizState.Finish:
                    SetActive(_choice, true);
                    SetActive(_question, true);
                    SetActive(_startBtn, false);
                    SetActive(_answerCorrectWrong, true);
                    SetActive(_nextBtn, true);
                    SetActive(_progressUI, true);
                    break;
                default:
                    break;
            }
        }
    }
}