using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOption : MonoBehaviour
{
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

    //
    void Start()
    {
        VolumeMusicChange();
        EffectMusicChange();
    }

    //fonction pour affiché le menu des options
    public void Option()
    {
        //lancement du menu des option (controle du volume, la qualité graphique)
        OptionPanel.SetActive(true);
    }

    //fonction pour modifier la valeur de la music
    public void VolumeMusicChange()
    {
        MusicAudioSource.volume = MusicSlider.value;
        VolumeMusicText.text = "Volume " + (MusicAudioSource.volume * 100).ToString("00") + " %";
    }

    //fonction pour modifier le volume des effets 
    public void EffectMusicChange()
    {
        EffectAudioSource.volume = EffectSlider.value;
        VolumeEffectText.text = "Effets " + (EffectAudioSource.volume * 100).ToString("00") + " %";
    }

    //fonction pour sortir du menu des option
    public void QuitOption()
    {
        OptionPanel.SetActive(false);
    }
}
