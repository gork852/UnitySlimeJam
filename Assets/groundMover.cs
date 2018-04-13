using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMover : MonoBehaviour {

    // Use this for initialization
    Vector3 startPos;
	void Start () {
        startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x + 50 < GameDirector.Director.Player.transform.position.x)
        {
            this.transform.position = this.transform.position + new Vector3(200, 0, 0);
        }
	}
}
