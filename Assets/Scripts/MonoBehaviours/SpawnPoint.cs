using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    public float repeatInterval;
    public int quantityEnemy = 0;
    public int maxQuantityEnemy = 4;
    public void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }
    public GameObject SpawnObject()
    {
        Vector3 vector = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        if (prefabToSpawn != null)
        {
            if (maxQuantityEnemy > quantityEnemy && prefabToSpawn.gameObject.CompareTag("Enemy"))
            {

                quantityEnemy += 1;
                                
                return Instantiate(prefabToSpawn,transform.position + vector, Quaternion.identity);
                
            }
            else if (prefabToSpawn.gameObject.CompareTag("Player"))
            {

                return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
                
            }

        }
        return null;
    }
    
}
