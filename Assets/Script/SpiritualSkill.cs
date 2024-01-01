using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritualSkill : MonoBehaviour
{
    //variable du joueur 
    public Transform Player;

    //pour le spiritual shoot
    public float shootRate = 1f;
    private float shootCountdown = 0f;
    
    public Transform shootPoint;
    public float spritualShotEnergy = 5f;
    public float spritualExplosionEnergy = 15f;

    //les sons des attaques spirituele 
    public AudioClip SpiritualExplosionSound;
    public AudioClip SpiritualShotSound;

    //pour les boutons des attacks spirituel 
    public Button Shot;
    public Button Explosion;

    //fonction pour le SpiritualShot
    public void SpiritualShot(GameObject spiritualBall)
    {
        if (!Player.GetComponent<CharacterMotor>().isDead && Player.GetComponent<Player>().currentMana >= spritualShotEnergy)
        {
            if (Player.GetComponent<CharacterMotor>().target == null)
            {
                return;
            }

            if (shootCountdown <= 0)
            {
                //création de la ball 
                GameObject ballGO = (GameObject)Instantiate(spiritualBall, shootPoint.position, shootPoint.rotation);
                SpiritBaall spiritBall = ballGO.GetComponent<SpiritBaall>();

                //verifie si la ball possède le bien le script SpiritBaall
                if (spiritBall != null)
                {
                    spiritBall.Seek(Player.GetComponent<CharacterMotor>().target);
                }

                shootCountdown = 1 / shootRate;
            }

            shootCountdown -= 10;
            //shootCountdown -= Time.deltaTime;
            Player.GetComponent<Animator>().SetTrigger("Shot");
            Player.GetComponent<AudioSource>().PlayOneShot(SpiritualShotSound);
            Player.GetComponent<Player>().currentMana -= spritualShotEnergy;

            //deactivation temporaire du bouton 
            StartCoroutine(ShotColdown());
        }
    }

    //fonction pour le SpiritualExplosion
    public void SpiritualExplosion(GameObject spiritualBall)
    {
        if (!Player.GetComponent<CharacterMotor>().isDead && Player.GetComponent<Player>().currentMana >= spritualExplosionEnergy)
        {
            if (Player.GetComponent<CharacterMotor>().target == null)
            {
                return;
            }

            if (shootCountdown <= 0)
            {
                //création de la ball 
                GameObject ballGO = (GameObject)Instantiate(spiritualBall, shootPoint.position, shootPoint.rotation);
                SpiritBaall spiritBall = ballGO.GetComponent<SpiritBaall>();

                //verifie si la ball possède le bien le script SpiritBaall
                if (spiritBall != null)
                {
                    spiritBall.Seek(Player.GetComponent<CharacterMotor>().target);
                }

                shootCountdown = 1 / shootRate;
            }

            shootCountdown -= 10;
            //shootCountdown -= Time.deltaTime;
            Player.GetComponent<Animator>().SetTrigger("Shot");
            Player.GetComponent<AudioSource>().PlayOneShot(SpiritualExplosionSound);
            Player.GetComponent<Player>().currentMana -= spritualExplosionEnergy;

            //deactivation temporaire du bouton
            StartCoroutine(ExplosionColdown());
        }
    }

    //création de la couroutin pour désactivé le spiritual Shot 
    IEnumerator ShotColdown()
    {
        Shot.interactable = false;
        yield return new WaitForSeconds(1f);
        Shot.interactable = true;
    }

    //création de la couroutin pour désactivé le spiritual Explosion
    IEnumerator ExplosionColdown()
    {
        Explosion.interactable = false;
        yield return new WaitForSeconds(2.5f);
        Explosion.interactable = true;
    }
}
