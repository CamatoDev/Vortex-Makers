using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchPage : MonoBehaviour
{
    public string NextScene;
    public string PreviousScene;

    //fonction pour allez � la sc�ne suivante 
    public void Next()
    {
        if (NextScene != null)
        {
            Debug.Log("Pas de sc�ne.");
        }

        SceneManager.LoadScene(NextScene);
    }

    //fonction pour allez � ma sc�ne pr�cedente 
    public void Previous()
    {
        if(PreviousScene != null)
        {
            Debug.Log("Pas de sc�ne");
        }

        SceneManager.LoadScene(PreviousScene);
    }
}
