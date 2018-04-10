using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    public GameObject targets;
    public GameObject player;

    private Vector3 currentAim;
    private Vector3 currentPos;
    private float fadeDist = 10;
	// Use this for initialization
	void Start () {
        currentAim = player.transform.position;
        currentPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        int count = 0;
        float cheapDist = 0;
        Vector3 avgPos = new Vector3();
        Vector3 maxPos = player.transform.position;
        Vector3 minPos = player.transform.position;
        foreach (Transform target in targets.transform)
        {
            maxPos.x = maxPos.x > target.position.x ? maxPos.x : target.position.x;
            maxPos.y = maxPos.y > target.position.y ? maxPos.y : target.position.y;
            maxPos.z = maxPos.z > target.position.z ? maxPos.z : target.position.z;

            minPos.x = minPos.x < target.position.x ? minPos.x : target.position.x;
            minPos.x = minPos.y < target.position.y ? minPos.y : target.position.y;
            minPos.z = minPos.z < target.position.z ? minPos.z : target.position.z;
            
            avgPos += target.position;
            count++;

        }
        cheapDist = (maxPos - minPos).magnitude;
        //prevent division by zero if player and enemies are missing
        if (count > 0) avgPos /= count;
        //object to player distance weight
        cheapDist *= .7f;
        //weight the player extra
        Vector3 middlePos = (maxPos + minPos) / 2;
        middlePos = (middlePos * 1 + player.transform.position * 2) / 3;
        avgPos = (avgPos * 1 + player.transform.position * 2) / 3;
        //min and max distances
        cheapDist = cheapDist < 3 ? 3 : cheapDist;
        cheapDist = cheapDist > 10 ? 10 : cheapDist;
        currentAim = currentAim * (1 - Time.deltaTime) + middlePos * Time.deltaTime;
        //currentAim = currentAim*(1-Time.deltaTime) + avgPos * Time.deltaTime;
        fadeDist = fadeDist * (1 - Time.deltaTime) + cheapDist * Time.deltaTime;
        Debug.DrawRay(currentAim, new Vector3(0, 10, 0),Color.yellow);
        Debug.DrawLine(currentAim, this.transform.position, Color.blue);
        Debug.DrawRay(this.transform.position, new Vector3(0, -fadeDist, 0),Color.red);
        currentPos = currentPos * .7f+currentAim*.3f +new Vector3(-fadeDist/10,0,0);
        currentPos.y = fadeDist;
        if (currentPos.x + 2 > currentAim.x)
        {
            currentPos = new Vector3(currentAim.x-2, currentPos.y, currentPos.z);
        }
        this.transform.position = this.transform.position*(1-Time.deltaTime)+currentPos*Time.deltaTime;
        this.transform.LookAt(currentAim,player.transform.up);
        
        
	}
}
