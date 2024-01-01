using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //Variable pour les images qui gère la barre de vie et la barre de mana
    Image hpImage;
    Image manaImage;

    //variable pour les valeur maximum de la vie, le mana, les dégats, la protection du joueur et le nombre d'ennemis tuer
    float maxHealth = 100;
    float maxMana = 100;
    float maxDamage = 0;
    float maxArmor = 0;
    public static float EnemyKillNumber;

    //variable pour les valeur instantané pour de la vie, le mana, les dégats, la protection du joueur, le nombre d'ennemis tuer ainsi que son pseudo  
    public float currentHealth;
    public float currentMana;
    public float currentDamage = 0;
    public float currentArmor = 0;
    public Text EnemyKillInGame;
    public Text PlayerNameTag;

    //pour la gestion du NameTag
    TouchScreenKeyboard clavier; //pour le clavier

    private CharacterMotor charactermotor;
    private Animator playerAnimations;
    private AudioSource audioSource;

    public AudioClip Die;
    public AudioClip GetHit;

    void Start()
    {
        currentHealth = 100;
        currentMana = 100;
        EnemyKillNumber = 0; 

        hpImage = GameObject.Find("currentHP").GetComponent<Image>();
        manaImage = GameObject.Find("currentMana").GetComponent<Image>();

        charactermotor = gameObject.GetComponent<CharacterMotor>();
        playerAnimations = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();

        if(MainMenu.NameTag.text != null)
        {
            PlayerNameTag.text = MainMenu.NameTag.text;
        }  
    }

    public void ApplyDamage(float TheDamage)
    {
        //verifie d'abord si le joueur est toujours en vie (pour ne pas lancer l'animation de mort plusieurs fois)
        if (!charactermotor.isDead)
        {
            // PDV = PDV - (damage - ((armor*damage) /100)) pour gerer la vie du joueur (PDV) en prenant en compte sa resistance (armor) lorsqu'il prend des dégats (TheDamage)
            currentHealth = currentHealth - (TheDamage - ((currentArmor * TheDamage) / 100));

            if (currentHealth <= 0)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        // à la mort du perso 
        playerAnimations.SetTrigger("Die");
        audioSource.PlayOneShot(Die);
        charactermotor.isDead = true;
    }

    //fonction pour gérer l'ouverture du clavier
    public void OpenKeyboard()
    {
        clavier = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default); //ouverture du clavier par defaut du téléphone 
    }

    // Update is called once per frame
    void Update()
    {
        if (!TouchScreenKeyboard.visible && clavier != null) //lorsque le clavier est visible et qu'il y'a une saisi 
        {
            if (clavier.done) //si on valide la saisi 
            {
                MainMenu.NameTag.text = clavier.text; //On concerve la saisi comme étant le Name tag 
                clavier = null; //on vide le clavier 
            }
        }

        //pour les ennemis tuées 
        EnemyKillInGame.text = "Kill : " + EnemyKillNumber.ToString();

        //pour la barre de pv
        float pourcentageHP = ((currentHealth * 100) / maxHealth) / 100;
        hpImage.fillAmount = pourcentageHP;

        //pour la barre de mana
        float pourcentageMana = ((currentMana * 100) / maxMana) / 100;
        manaImage.fillAmount = pourcentageMana;
    }

}
