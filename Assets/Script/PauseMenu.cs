using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //pour le fondu 
    public SceneFader sceneFader;
    public string levelToLoad = "MainMenu";

    public GameObject pauseUI;

    //fonction pour basculer entre pause et continuer 
    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        //mettre le temps sur pause si on est en pause et le faire continuer sinon 
        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f; //temps en pause 
        }
        else
        {
            Time.timeScale = 1f; //temps sur play 
        }

    }

    //fonction pour recommencé le niveau depuis le menu de pause 
    public void Retry()
    {
        Toggle(); //remet le temps en vitesse normal
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); 
    }

    //fonction pour retourner au menu principale 
    public void Menu()
    {
        Time.timeScale = 1f; //temps sur play
        sceneFader.FadeTo(levelToLoad);
    }
}
