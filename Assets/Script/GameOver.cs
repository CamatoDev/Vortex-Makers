using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //pour le fondu 
    public SceneFader sceneFader;

    public string levelToLoad = "MainMenu";

    //pour le text du nombre d'ennemis tués 
    public Text EnemyKillNumberText;

    // Start is called before the first frame update
    void OnEnable()
    {
        EnemyKillNumberText.text = Player.EnemyKillNumber.ToString() + " Ennemis vaincu.";
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
