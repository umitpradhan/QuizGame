using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Timer : MonoBehaviour
{
    public static Timer timerInstance;

    public Text timerText;
    private float startTime;
    public QuestionData questionData;

    public void Awake()
    {
        timerInstance = this;
    }
    
    public void BeginTimer()
    {
        startTime = 60.0f;       
        StartCoroutine(TimeCalc());
        
    }    

    IEnumerator TimeCalc()
    {
        while (startTime >= 0.0f)
        {
            startTime -= Time.deltaTime;

            string minutes = ((int)startTime / 60).ToString();
            string seconds = (startTime % 60).ToString("f");

            timerText.text = minutes + ":" + seconds;
            yield return null;
        }
        
        TimeEndCheck(startTime);
    }
    public float RemainingTime()
    {
        return startTime;
    }

    public void TimeEndCheck(float timeInput)
    {
        if (timeInput <= 0.0f)
        {
            SceneManager.LoadSceneAsync("Scoreboard");
        }
    }
}
