using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //variable pour le changement de scene 
    public string levelToLoad = "Level1";

    //variable pour le fondu
    public SceneFader sceneFader;

    //pour le panel des option
    public GameObject OptionPanel;
    //pour la musique
    public AudioSource MusicAudioSource;
    public Slider MusicSlider;
    public Text VolumeMusicText;
    //pour les effets
    public AudioSource EffectAudioSource;
    public Slider EffectSlider;
    public Text VolumeEffectText;

    //pour la gestion du NameTag
    TouchScreenKeyboard clavier; //pour le clavier 
    public static string NameTag; //le pseudo du joueur 


    //
    void Start()
    {
        VolumeMusicChange();
        NameTag = PlayerPrefs.GetString("nameTag", "INCONU");
    }

    private void Update()
    {
        if(!TouchScreenKeyboard.visible && clavier != null) //lorsque le clavier est visible et qu'il y'a une saisi 
        {
            if(clavier.done) //si on valide la saisi 
            {
                PlayerPrefs.SetString("nameTag", clavier.text);//On concerve la saisi
                //NameTag.text = clavier.text; //On concerve la saisi comme étant le Name tag 
                clavier = null; //on vide le clavier 
            }
        }
    }

    //fonction pour lancer le jeu 
    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    //fonction pour affiché le menu des options
    public void Option()
    {
        //lancement du menu des option (controle du volume, la qualité graphique)
        OptionPanel.SetActive(true);
    }

    //fonction pour lire le tutoriel du jeu 
    public void Guide()
    {
        sceneFader.FadeTo("GameGuide 0");
    }

    //fonction pour modifier la valeur de la music
    public void VolumeMusicChange()
    {
        MusicAudioSource.volume = MusicSlider.value;
        VolumeMusicText.text = "Volume " + (MusicAudioSource.volume * 100).ToString("00") + " %";
    }

    //fonction pour gérer l'ouverture du clavier
    public void OpenKeyboard()
    {
        clavier = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default); //ouverture du clavier par defaut du téléphone 
    }

    //fonction pour sortir du menu des option
    public void QuitOption()
    {
        OptionPanel.SetActive(false);
    }

    //fonction pour quitter 
    public void Quit()
    {
        Debug.Log("Fermeture du jeu...");
        Application.Quit();
    }

}
