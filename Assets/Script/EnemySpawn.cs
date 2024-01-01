using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    //reference au gameManager
    public GameManager gameManager;

    //nombre d'ennemi en vie 
    public static int EnemiesAlive = 0;

    //Array pour la liste des vagues 
    public Wave[] waves;

    [SerializeField]
    //point d'apparition des ennemis 
    private Transform spawnPoint;
    [SerializeField]
    //point d'apparition des ennemis 
    private Transform spawnPoint2;
    [SerializeField]
    //point d'apparition des ennemis 
    private Transform spawnPoint3;

    [SerializeField]
    //temps avant chaque vagues
    private float timeBetweenWaves = 5.5f;

    //temps avant l'arrivé des premier ennemis  
    private float countdown = 5f;

    [SerializeField]
    //text pour informer sur le temps avant l'arrivé des ennemis 
    private Text WaveCountdownTimer;

    //numéro des vagues
    private int waveIndex = 0;

    private void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {
        //si il n'y a plus d'ennemi 
        if (EnemiesAlive > 0)
        {
            return;
        }

        // lorsqu'on termine la dernière vague le script est désactivé
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        //apparition des ennemis 
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime; //on décrémente la valeur du countdown 

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        WaveCountdownTimer.text = string.Format("{0:00.00}", countdown); //text du temps restant pour une vague affiché à l'écran 
    }

    //corroutine pour l'appariion des vagues 
    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        //EnemiesAlive = wave.count; // définition du nombre d'ennemi de la vague 

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
            SpawnEnemy2(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
            SpawnEnemy3(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    //apparition des ennemis 
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
    void SpawnEnemy2(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint2.position, spawnPoint2.rotation);
        EnemiesAlive++;
    }
    void SpawnEnemy3(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint3.position, spawnPoint3.rotation);
        EnemiesAlive++;
    }
}
