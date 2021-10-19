using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SubmitButton : MonoBehaviour
{
    
    public void SubmitManager()
    {
        
        SceneManager.LoadSceneAsync("Scoreboard");
        
    }
}
