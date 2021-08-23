using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;
    public SpawnPoint playerSpawnPoint;
    public SpawnPoint enemySpawnPoint;
    public RPGCameraManager cameraManager;

    void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }
    void Start()
    { 
        SetupScene();
    }
    public void SetupScene()
    {
        SpawnPlayer();
        SpawnEnemy();
    }
    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
    public void SpawnEnemy()
    {
        if (enemySpawnPoint != null)
        {

            GameObject enemy = enemySpawnPoint.SpawnObject();
        }      
    }
    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
 
