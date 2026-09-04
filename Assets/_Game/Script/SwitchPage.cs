using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchPage : MonoBehaviour
{
    public string NextScene;
    public string PreviousScene;

    //fonction pour allez à la scène suivante 
    public void Next()
    {
        if (NextScene != null)
        {
            Debug.Log("Pas de scène.");
        }

        SceneManager.LoadScene(NextScene);
    }

    //fonction pour allez à ma scène précedente 
    public void Previous()
    {
        if(PreviousScene != null)
        {
            Debug.Log("Pas de scène");
        }

        SceneManager.LoadScene(PreviousScene);
    }
}
