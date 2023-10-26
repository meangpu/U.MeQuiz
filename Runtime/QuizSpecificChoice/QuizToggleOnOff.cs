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
        }
        void OnDisable()
        {
            QuizStateManager.OnUpdateGameState -= UpdateState;
        }

        private void UpdateState(QuizState state)
        {
            switch (state)
            {
                case QuizState.Waiting:
                    _choice.SetActive(false);
                    _question.SetActive(false);
                    _startBtn.SetActive(true);
                    _answerCorrectWrong.SetActive(false);
                    _nextBtn.SetActive(false);
                    _progressUI.SetActive(false);
                    break;
                case QuizState.Playing:
                    _choice.SetActive(true);
                    _question.SetActive(true);
                    _startBtn.SetActive(false);
                    _answerCorrectWrong.SetActive(false);
                    _nextBtn.SetActive(false);
                    _progressUI.SetActive(true);
                    break;
                case QuizState.Finish:
                    _choice.SetActive(true);
                    _question.SetActive(true);
                    _startBtn.SetActive(false);
                    _answerCorrectWrong.SetActive(true);
                    _nextBtn.SetActive(true);
                    _progressUI.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}