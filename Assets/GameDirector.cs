using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    public static GameDirector Director;
    public GameObject enemyPrefab;
    public GameObject Player;
    private float lastSpawn = 0;
    public GameObject currentCamera;
    public float spawnRate = 10;
    public List<GameObject> enemiesSpawned;
    public GameObject Qability, Wability, Eability, Rability;
    public GameObject mainLight;
    // Use this for initialization
    void Start () {
        Director = this;
        lastSpawn = 15 - spawnRate;
        enemiesSpawned = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time-lastSpawn > spawnRate && enemiesSpawned.Count<7)
        {
            for(int i = 0; i < 4; i++)
            {
                enemiesSpawned.Add(
                    Instantiate(enemyPrefab,
                        new Vector3(Random.value * 5 + Player.transform.position.x + 5, 2, Random.value * 10 - 5),
                        this.transform.rotation,
                        this.transform)
                    );
            }
            
            lastSpawn = Time.time;
        }
	}
}
