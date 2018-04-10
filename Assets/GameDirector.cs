using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    public static GameDirector Director;
    public GameObject enemyPrefab;
    public GameObject Player;
    private float lastSpawn = 0;
    public GameObject currentCamera;
	// Use this for initialization
	void Start () {
        Director = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time-lastSpawn > 10)
        {
            for(int i = 0; i < 4; i++)
            {
                Instantiate(enemyPrefab, new Vector3(Random.value * 5 + Player.transform.position.x + 5, 2, Random.value * 10 - 5), this.transform.rotation, this.transform);
            }
            
            lastSpawn = Time.time;
        }
	}
}
