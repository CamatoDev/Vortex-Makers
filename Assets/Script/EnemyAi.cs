using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    //distance entre le joueur et l'ennemi 
    private float Distance; 

    //cible 
    public Transform Target;

    //distance � laquelle la poursuite s'active 
    public float chaseRange = 10;

    //ditance � laquelle on attaque 
    public float attackRange = 2.9f;

    //port� des attques 
    public float attackRepeatTime = 1;
    private float attackTime;

    //puissance des d�gats
    public float TheDamage;

    //agent de nav
    private UnityEngine.AI.NavMeshAgent agent;

    //animation 
    private Animator animations;
    //pour g�rer la source audio 
    private AudioSource audioSource;

    //Les diff�rents sons 
    public AudioClip Walk;
    public AudioClip Hit;
    public AudioClip Damage;
    public AudioClip Die;


    //pour g�rer l'attaque de l'ennemi
    public float enemyHealth;
    private bool isDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //si il est vivant 
        if (!isDeath)
        {
            //on cherche le joueur tout le temps 
            Target = GameObject.Find("Player").transform;

            // distance entre le joueur et l'ennemi pour les diff�rentes actions � effectuer 
            Distance = Vector3.Distance(Target.position, transform.position);

            // Quand le jouer est trop loin ou qu'il est mort : Les ennemis vont vers le village 
            if (Distance > chaseRange || Target.GetComponent<CharacterMotor>().isDead)
            {
                ChaseVillage();
            }

            // Quand le joueur est detect� � la distance de chasse et qu'il est vivant
            if (Distance < chaseRange && Distance > attackRange && !Target.GetComponent<CharacterMotor>().isDead)
            {
                Chase();
            }

            // Quand le joueur est � la port� d'attaque 
            if (Distance < attackRange && !Target.GetComponent<CharacterMotor>().isDead)
            {
                Attack();
            }
        }
    }

    //pour la chase 
    void Chase()
    {
        animations.SetFloat("Walk", 1.0f);
        agent.destination = Target.position;
    }

    void ChaseVillage()
    {
        animations.SetFloat("Walk", 1.0f);
        agent.destination = GameObject.Find("Village").transform.position;
    }

    //pour l'attaque 
    void Attack()
    {
        //ne pas entrer dans le joueur 
        agent.destination = transform.position;

        //pas de cooldown
        if (Time.time > attackTime) //au moment de l'attque 
        {
            animations.SetFloat("Walk", 0.0f); //on arr�te la marche 
            animations.SetTrigger("Hit"); //on lance l'animation de l'attaque 
            audioSource.PlayOneShot(Hit); // on lance le son de l'attaque 
            Target.GetComponent<Player>().ApplyDamage(TheDamage); //le joueur re�oit les d�gats 
            Debug.Log("L'ennemi a envoy� " + TheDamage + " points de d�g�ts");
            if(Target.GetComponent<Player>().currentHealth > 0) //si les pv du joueur sont vide 
            {
                Target.GetComponent<Animator>().SetTrigger("GetHit"); //le joueur lance l'animation de d�gat 
                Target.GetComponent<AudioSource>().PlayOneShot(Target.GetComponent<Player>().GetHit);
            }
            attackTime = Time.time + attackRepeatTime;
        }
    }

    //pour le repos 
    void Idle()
    {
        animations.SetFloat("Walk", 0.0f);
    }

    //fonction pour appliqu� les d�gats sur l'enemi
    public void ApplyDamage(float TheDamage)
    {
        if (!isDeath)
        {
            animations.SetTrigger("Damage");
            audioSource.PlayOneShot(Damage);
            enemyHealth = enemyHealth - TheDamage;
            print(gameObject.name + " � subit" + TheDamage + " points de d�g�ts");

            if(enemyHealth <= 0 && !isDeath)
            {
                animations.SetFloat("Walk", 0.0f);
                Dead();
            }
        }
    }

    //fonction pour la mort de l'enemi 
    public void Dead()
    {
        isDeath = true;
        animations.SetTrigger("Die");
        audioSource.PlayOneShot(Die);
        Destroy(transform.gameObject, 1);
        EnemySpawn.EnemiesAlive--;
        Player.EnemyKillNumber++;
    }
}
