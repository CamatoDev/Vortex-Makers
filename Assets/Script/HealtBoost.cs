using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBoost : MonoBehaviour
{
    //distance entre le joueur et le boost 
    private float Distance;
    //ditance o� on re�oit les effets du boost
    public float BoostRange = 1.5f;
    //valeur du boost
    public float TheBoost = 50f;

    //cible 
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        //on recup�re le joueur comme �tant la cible
        Target = GameObject.Find("Player").transform;

        //on recup�re la distance entre le joueur et le boost
        Distance = Vector3.Distance(Target.position, transform.position);

        //si le joueur touche le boost il re�oit son effet 
        if(Distance < BoostRange && !Target.GetComponent<CharacterMotor>().isDead)
        {
            Boost();
        }
    }

    //fonction pour le boost
    public void Boost()
    {
        Target.GetComponent<Animator>().SetTrigger("Boost");
        if(Target.GetComponent<Player>().currentHealth <= 50)
        {
            Target.GetComponent<Player>().currentHealth += TheBoost;
        }
        else
        {
            Target.GetComponent<Player>().currentHealth = 100;
        }
        Debug.Log("Le joueur a re�u " + TheBoost + " points de vie");
        Destroy(gameObject);
    }
}
