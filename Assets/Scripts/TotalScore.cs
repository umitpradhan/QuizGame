using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class TotalScore : MonoBehaviour
{
    public Text totalScoreContent;
    public Text extraScoreContent;
    public Text quizScoreContent;
    void Start()
    {
        int quizScore= (QuizContentLog.answers.Where(answer => answer).Count()) * 10;
        
        int extraScore = ((int)Timer.timerInstance.RemainingTime());
        
        int totalScore = quizScore  + extraScore;

        quizScoreContent.text = quizScore.ToString();
        extraScoreContent.text = extraScore.ToString();
        totalScoreContent.text = totalScore.ToString();

    }

}
        
