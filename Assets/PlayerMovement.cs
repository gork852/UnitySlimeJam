using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody phys;
    private GameObject curentCamera;
    public float moveSpeed = 4;
	// Use this for initialization
	void Start () {
        phys = this.GetComponent<Rigidbody>();
        curentCamera = GameDirector.Director.currentCamera;
	}
	
	// Update is called once per frame
	void Update () {
        bool forward, back, left, right;
        forward = Input.GetKey(KeyCode.W);
        back = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        Vector3 camRight = Vector3.Cross(new Vector3(0, 1, 0), curentCamera.transform.forward).normalized;
        Vector3 camForward = -Vector3.Cross(new Vector3(0, 1, 0), camRight).normalized;

        Vector3 newMove = new Vector3();
        newMove.y = phys.velocity.y;
        if (forward) newMove += moveSpeed*camForward;
        if (back) newMove += -moveSpeed * camForward;
        if (left) newMove += -moveSpeed * camRight;
        if (right) newMove += moveSpeed * camRight;

        
        //Debug.DrawRay(this.transform.position, camRight, Color.green);
        //Debug.DrawRay(this.transform.position, camForward, Color.red);
        


        phys.velocity = newMove;
	}
}
