using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectifDirection : MonoBehaviour
{
    //la variable qui va contenir la cible
    private Transform target;
    //vitesse de changement d'un ennemis à l'autre 
    public float turnSpeed = 6.5f;
    //pour recuperer la position du joueur 
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        //on invoque la fonction UpdateTarget à des intervalle de temps regulier
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        //definition d'un tableau d'ennemi
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

        //si il y'a déjà un ennemi ciblé 
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform; //ennemi ciblé devient le nouvelle ennemi le plus proche
        }
        else
        {
            target = null; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Gestion du suivi du sur l'axe x et z 
        transform.position = new Vector3(player.transform.position.x + 0.15f, player.transform.position.y + 36, player.transform.position.z - 8);

        if (target == null)
        {
            return;
        }
        LockOnTarget();
    }

    //fonction pour lancé des projectile sur la cible 
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position; //on recupère la position entre l'ennemi et la flèche 
        Quaternion lookRotation = Quaternion.LookRotation(dir); 
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; //on crée un vector3 qui va recupére les angle de rotation 
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f); //on passe l'angle de rotation à l'objet 
    }
}
