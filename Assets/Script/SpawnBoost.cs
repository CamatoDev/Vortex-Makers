using UnityEngine;

public class SpawnBoost : MonoBehaviour
{
    public GameObject cubePrefab;
    public Vector3 size;

    void Start()
    {
        //if ()
        //{
            GameObject instantiated = GameObject.Instantiate(cubePrefab);
            instantiated.transform.position = new Vector3(
                Random.Range(transform.position.x - size.x / 2, transform.position.x + size.x / 2),
                Random.Range(transform.position.y - size.y / 2, transform.position.y + size.y / 2),
                Random.Range(transform.position.z - size.z / 2, transform.position.z + size.z / 2)
                );
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}