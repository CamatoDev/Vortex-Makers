using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoost : MonoBehaviour
{
    //ditance où on reçoit les effets du boost
    public float BoostRange = 2.5f;
    //valeur du boost
    public float TheBoost = 2f;

    //cible 
    public Transform Target;
    //les attaques à distances du joueur
    public Transform SpiritualBall;

    private void Start()
    {
        //on recupère le joueur comme étant la cible
        Target = GameObject.Find("Player").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //si le joueur touche le boost il reçoit son effet 
        if (collision.transform.tag == "Player")
        {
            Boost();
        }
    }

    //fonction pour le boost
    public void Boost()
    {
        Target.GetComponent<Animator>().SetTrigger("Boost");
        Target.GetComponent<Player>().currentDamage *= TheBoost;
        SpiritualBall.GetComponent<SpiritBaall>().theDamage *= TheBoost;
        Debug.Log("Le joueur a reçu un boost de force x" + TheBoost);
        Destroy(gameObject);

        //marquer une pause puis désactiver le boost
    }
}
