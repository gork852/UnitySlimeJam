using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    public static GameDirector Director;
    public GameObject enemyPrefab;
    public GameObject Player;
    private float lastSpawn = 0;
	// Use this for initialization
	void Start () {
        Director = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time-lastSpawn > 2)
        {
            Instantiate(enemyPrefab, new Vector3(Random.value, Random.value, Random.value), this.transform.rotation,this.transform);
            lastSpawn = Time.time;
        }
	}
}
