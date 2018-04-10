using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    public GameObject targets;
    public GameObject player;

    private Vector3 currentAim;
    private float fadeDist = 10;
	// Use this for initialization
	void Start () {
        currentAim = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        int count = 0;
        float cheapDist = 0;
        Vector3 avgPos = new Vector3();
        foreach (Transform target in targets.transform)
        {
            float newDist = (target.position - avgPos).magnitude;
            cheapDist = cheapDist > newDist ? cheapDist : newDist;
            avgPos += target.position;
            count++;
        }
        //object to player distance weight
        cheapDist *= .3f;
        //weight the player extra
        avgPos = (avgPos * 1 + player.transform.position * 1)/2;

        cheapDist = cheapDist > 10 ? 10 : cheapDist;
        if(count>0) avgPos /= count;
        currentAim = currentAim*(1-Time.deltaTime) + avgPos * Time.deltaTime;
        fadeDist = fadeDist * (1 - Time.deltaTime) + cheapDist * Time.deltaTime;
        
        this.transform.position = player.transform.position+new Vector3(0,fadeDist,0);
        this.transform.LookAt(currentAim,player.transform.up);
        
        
	}
}
