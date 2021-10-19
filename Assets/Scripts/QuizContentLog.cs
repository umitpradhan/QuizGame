using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;



public class QuizContentLog : MonoBehaviour
{
    
    
    [SerializeField]
    private GameObject questionTemplate;
    [SerializeField]
    private GameObject optionTemplate;
    [SerializeField]
    private GameObject quizContent;

    public List<Button> selectedAnswers;
    public static List<QuestionData> questionList;
    public static List<bool> answers;
    
    void Start()
    {
        
        
        StartCoroutine(getData());
    }

    public  IEnumerator getData()
    {
        Debug.Log("Processing Data, Please Wait");

        UnityWebRequest _www = new UnityWebRequest("https://jsonkeeper.com/b/CIV6");
        _www.SetRequestHeader("Content-Type", "application/json");
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();


        _www.downloadHandler = dH;
        yield return _www.SendWebRequest();

        if (_www.error == null)
        {
            
            quizText(JsonUtility.FromJson<JSONData>(_www.downloadHandler.text));
        }
        else
        {
            Debug.Log("Oops");
        }
    }

    public void quizText(JSONData quizData)
    {
        questionList = quizData.questionlist;
        selectedAnswers = new List<Button>();
        answers = new List<bool>();
        
        Timer.timerInstance.BeginTimer();
        
        for (int j=0; j< quizData.questionlist.Count; j++)
        {
            selectedAnswers.Add(null);
            answers.Add(false);
            QuestionData questionData = quizData.questionlist[j];
            GameObject newQuestContent = Instantiate(questionTemplate) as GameObject;
            newQuestContent.SetActive(true);
            
            newQuestContent.GetComponent<Text>().text = questionData.question;
            
            newQuestContent.transform.SetParent(quizContent.transform, false);
            for (int i = 0; i < questionData.options.Count; i++)
            {

                GameObject newOptContent = Instantiate(optionTemplate) as GameObject;
                newOptContent.SetActive(true);
                
                GameObject newOptContentText = newOptContent.transform.GetChild(0).gameObject;
                
                string option = questionData.options[i];
                string answer = questionData.options[questionData.correctOptionIndex];
                Button optionButton = newOptContent.GetComponent<Button>();
                int questionIndex = j;
                newOptContent.GetComponent<Button>().onClick.AddListener(() => 
                {
                    
                    if(selectedAnswers[questionIndex] != null)
                    {
                        
                        selectedAnswers[questionIndex].colors = ColorBlock.defaultColorBlock;
                        selectedAnswers[questionIndex].interactable=true;
                    }
                    selectedAnswers[questionIndex] = optionButton;
                    ColorBlock optCB = optionButton.colors;
                    optCB.selectedColor = Color.grey;
                    optionButton.colors = optCB;
                    optionButton.interactable = false;
                    answers[questionIndex] = (option == answer);
                });

                
                
                newOptContentText.GetComponent<Text>().text = questionData.options[i];
              
                newOptContent.transform.SetParent(quizContent.transform, false);
            }
            
        }
        questionTemplate.SetActive(false);
        optionTemplate.SetActive(false);
    }

    
    

       
}
