using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageBoostEffect : MonoBehaviour
{
    //definition du village
    public Transform village;
    //limite du village
    public float range = 80;
    //
    public Transform player;
    //
    private Transform target;

    
    // Start is called before the first frame update
    void Start()
    {
        //pour app�l� la fonction de la recherche d'ennemi qui on traverser les fronti�res du village de fa�on optimis� 
        InvokeRepeating("UpdatePlayerFind", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //si le joueur est hors du village 
        if (target == null)
        {
            return;
        }
        //si le joueur est dans le village 
        if (target.GetComponent<Player>().currentHealth <= 99)
        {
            target.GetComponent<Player>().currentHealth += 1;
        }
        if (target.GetComponent<Player>().currentMana <= 99)
        {
            target.GetComponent<Player>().currentMana += 1;
        }
        Debug.Log("Le joueur re�oit les effet du village");
    }

    //verifie si un ennemi a p�n�trer le village si c'est le cas le jeu est termin� 
    void UpdatePlayerFind()
    {
        float distanceToPlayer = Vector3.Distance(village.position, player.position);
        if (distanceToPlayer <= range)
        {
            target = player;
        }
        else
        {
            target = null;
        }
    }

    //D�finition des limite du village (que les ennemis ne doivent pas franchir)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(village.position, range);
    }
}
