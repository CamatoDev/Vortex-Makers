using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;

    //Tableau qui vas contenir tous les bouton
    public Button[] levelButtons;
    //Tableau qui vas contenir tous les images lock
    public GameObject[] locks;

    //
    void Start()
    {
        //sauvegarde du niveau atteint 
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for(int i = 0; i < levelButtons.Length; i++)
        {
            //deverouiller le niveau si il est atteint
            if(i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
                locks[i].SetActive(true);
            }
            else
            {
                locks[i].SetActive(false);
            }
        }
    }

    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }

    //fonction pour retourner au menu
    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }
}
