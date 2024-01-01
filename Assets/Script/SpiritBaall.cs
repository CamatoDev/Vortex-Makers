using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBaall : MonoBehaviour
{
    //definition de la cible 
    private Transform target;
    //vitesse de la balle
    public float speed = 70f;
    //d�gat de la ball 
    public float theDamage = 5f;
    //Variable pour le rayon de l'exploision (si on appelle le spiritual explosion)
    public float explosionRadius = 0f;
    //effet de la competence utilis�
    public GameObject impactEffect;
    //temps de destruction de la ball
    public float destroyTime = 3f;

    //pour gerer les sons 
    private AudioSource audioSource;
    //pour le son de l'explosion
    public AudioClip ExplosionSound;


    //
    void Start()
    {
        //on recuo�re le component qui va port� les son 
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    //fonction pour le recherche de la cible 
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //si la ball n'a pas de cible on l'a d�truit 
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //direction de la ball 
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //verifie si on touche la cible 
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //d�placer la ball avec une vitesse constante 
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    //fonction losrqu'on touche l'ennemie
    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 4f);

        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        //detruire la ball
        Destroy(gameObject, destroyTime);
    }

    //Fonction pour l'explosion 
    void Explode()
    {
        //on recup�re le collider de tous les objets dans le rayon de l'explosion 
        Collider[] collides = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in collides)
        {
            if (collider.tag == "Enemy") //on tri en fonction des objets dont le tag est Enemy  
            {
                Damage(collider.transform);//on applique les d�gat sur tous les enemis dans le rayon 
            }
        }
        audioSource.PlayOneShot(ExplosionSound);
    }

    //pour faire les d�g�ts 
    void Damage(Transform enemy)
    {
        enemy.GetComponent<EnemyAi>().ApplyDamage(theDamage);
    }

    //pour affich� le rayon de l'explosion (dans l'�diteur)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
