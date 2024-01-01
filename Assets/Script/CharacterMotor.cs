using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]

public class CharacterMotor : MonoBehaviour
{
    //variable du joueur 
    Player PlayerUI;

    //Pour le joystick
    public FixedJoystick joystick;
    //pour les animations 
    private Animator animations;
    //pour le rigidbody 
    private Rigidbody rigidbody;
    //POUR gérer les différents sons 
    private AudioSource audioSource;
    //pour la camera 
    public Camera camera;
    //verifie si le joueur est mort
    public bool isDead = false;

    //gérer les différents sons
    public AudioClip Walk;
    public AudioClip Punch;

    //attaque du personnage
    public float attackColdown;
    private float currentColdown;
    public float attackRange;
    private bool isAttacking;
    //Pour la ligne d'attaque 
    public GameObject rayHit;

    //Vitesse de mouvement 
    public float moveSpeed;
    private float turnSpeed = 6.5f;

    //variable pour la gestion de l'ennemi 
    public Transform target;
    public float range = 15f;

    // Start is called before the first frame update
    void Start()
    {
        //recuperer toutes les animation du joueur 
        animations = gameObject.GetComponent<Animator>();
        //recuperer le rigidbody du joueur 
        rigidbody = gameObject.GetComponent<Rigidbody>();
        //pour récupérer les sources audios
        audioSource = gameObject.GetComponent<AudioSource>();
        //recuperer le script Player du joueur 
        PlayerUI = gameObject.GetComponent<Player>();
        //Recupère l'object du nom de RayHit 
        rayHit = GameObject.Find("RayHit");

        //pour appélé la fonction de la recherche d'ennemi de façon optimisé 
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        //definition d'une array d'ennemi
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //au depart 
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        //Recherche de l'ennemi le plus proche 
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    //fonction pour les mouvement avec le Joystick 
    private void Update()
    {
        if (!isDead)
        {
            //création du déplacement du joueur 
            rigidbody.velocity = new Vector3(joystick.Horizontal * moveSpeed, rigidbody.velocity.y, joystick.Vertical * moveSpeed);

            //Gestion de la rotation si le joueur ne vas pas tout droit 
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
                if (!isAttacking)
                {
                    animations.SetFloat("Walk1", 2.0f);
                    audioSource.PlayOneShot(Walk);
                }
            }
            else
            {
                animations.SetFloat("Walk1", 0f);
            }
        

            //Gestion du suivi de la camera sur l'axe x et z 
            camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - 12);

            //Pour éviter le spame des attaques
            if (isAttacking)
            {
                currentColdown -= Time.deltaTime; 
            }
            if(currentColdown <= 0)
            {
                currentColdown = attackColdown;
                isAttacking = false;
            }
            if (target == null)
            {
                return;
            }
            LockOnTarget();

            //Récupération auto de l'energie spirituel
            //if(PlayerUI.GetComponent<Player>().currentMana <= 0)
            //{
            //    while(PlayerUI.GetComponent<Player>().currentMana <= 40)
            //    {
            //        //faire remonter progressivement la barre d'énergie spirituel 
            //        PlayerUI.GetComponent<Player>().currentMana += 1;
            //    } 
            //}
        }
    }

    //fonction pour lancé des projectile sur la cible 
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    

    //fonction pour l'attaque 
    public void Attack()
    {
        if (!isDead)
        {
            if (!isAttacking)
            {
                animations.SetTrigger("Punch");
                audioSource.PlayOneShot(Punch);

                RaycastHit hit;

                if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
                {
                    Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<EnemyAi>().ApplyDamage(PlayerUI.currentDamage);
                    }
                    isAttacking = true;
                }
            }
        }  
    }

    //pour la porté d'attaque 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
