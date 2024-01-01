using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //pour la camera 
    public Camera Camera;
    //pour le fondu 
    public SceneFader sceneFader;
    //definition du village
    public Transform village;
    //limite du village
    public float range = 80;
    //V�rifie si le jeu est termin� 
    private static bool gameIsOver;
    //Ennemi de village
    private Transform target;
    //le UI du gameover 
    public GameObject gameOverUI;
    //le UI de fin de niveau 
    public GameObject completeLevelUI;
    //le UI de l'�tat critique 
    public GameObject criticalState;
    //variable pour g�rer le joueur 
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;
        //pour app�l� la fonction de la recherche d'ennemi qui on traverser les fronti�res du village de fa�on optimis� 
        InvokeRepeating("UpdateEnemyFind", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Player>().currentHealth <= 30)
        {
            criticalState.SetActive(true);
            Camera.GetComponent<AudioSource>().volume = 0.1f;
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            criticalState.SetActive(false);
            gameObject.GetComponent<AudioSource>().mute = true;
            Camera.GetComponent<AudioSource>().volume = 0.5f;
        }
        if (gameIsOver)
        {
            GameEnded();
        }
    }

    //verifie si un ennemi a p�n�trer le village si c'est le cas le jeu est termin� 
    void UpdateEnemyFind()
    {
        //definition d'un tableau d'ennemi
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //au depart 
        float shortestDistance = Mathf.Infinity;
        GameObject EnemyInVillage = null;

        //Recherche de l'ennemi qui a pen�tre dans le village
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(village.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                EnemyInVillage = enemy;
            }
        }

        if (EnemyInVillage != null && shortestDistance <= range)
        {
            target = EnemyInVillage.transform;
            gameIsOver = true;
            gameOverUI.SetActive(true);
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

    //fonction lorsque la partie est termin�
    public void GameEnded()
    {
        criticalState.SetActive(false);
    }

    //fonction pour la victoire du joueur 
    public void WinLevel()
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
