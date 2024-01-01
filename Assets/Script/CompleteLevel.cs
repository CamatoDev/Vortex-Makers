using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    //pour le fondu 
    public SceneFader sceneFader;

    public string levelToLoad = "MainMenu";

    //pour le text du nombre d'ennemis tués 
    public Text EnemyKillNumberText;

    //pour le deverouillage du niveau suivant
    public string nextLevel = "level2";
    public int levelToUnlock = 2;

    // Start is called before the first frame update
    void OnEnable()
    {
        EnemyKillNumberText.text = Player.EnemyKillNumber.ToString() + " Ennemis vaincu.";

        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
    }

    //Pour passer au niveau suivant 
    public void NextLevel()
    {
        sceneFader.FadeTo(nextLevel);
    }

    //Pour réessayer 
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    //Pour revenir au menu principal 
    public void Menu()
    {
        sceneFader.FadeTo(levelToLoad);
    }
}
