using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    // Singleton instance
    private static MenuMusicManager instance;

    private void Awake()
    {
        // Si une instance existe et que ce n'est pas celle-ci on la supprime 
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Sinon c'est le premier qu'on charge
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void StopMusic()
    {
        // Cherche TOUS les objets dans le jeu qui possèdent le script MenuMusicManager
        MenuMusicManager[] toutesLesMusiques = FindObjectsOfType<MenuMusicManager>();

        // On parcourt la liste et on les détruit TOUS de force
        foreach (MenuMusicManager zik in toutesLesMusiques)
        {
            Destroy(zik.gameObject);
        }

        // On réinitialise la variable de sécurité
        instance = null;
    }
}
