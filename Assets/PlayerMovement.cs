using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody phys;
    private GameObject curentCamera;
    public float attackRange = 5;
    public float moveSpeed = 4;
    private float nextAttackTime = 0;
    public float attackCoolDown = 1;
    private float energy = 0;
    public float maxEnergy = 10;
    private float enthusiasim = 0;
    private float moveDownTime = 1;
    private float moveTime = 0;
    public GameObject lightningLine;
    public GameObject spookPrefab;

    private RectTransform Q, W, E, R;
	// Use this for initialization
	void Start () {
        phys = this.GetComponent<Rigidbody>();
        curentCamera = GameDirector.Director.currentCamera;
        Q = GameDirector.Director.Qability.GetComponent<RectTransform>();
        W = GameDirector.Director.Wability.GetComponent<RectTransform>();
        E = GameDirector.Director.Eability.GetComponent<RectTransform>();
        R = GameDirector.Director.Rability.GetComponent<RectTransform>();
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

        Vector2 localClick = new Vector2();
        Camera cam = GameDirector.Director.currentCamera.GetComponent<Camera>();
        if (RectTransformUtility.RectangleContainsScreenPoint(Q,Input.mousePosition) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Q skill");
            foreach (GameObject enemy in GameDirector.Director.enemiesSpawned)
            {
                enemy.GetComponent<SlimeMovement>().stun(this.gameObject);
                LightningSegment.spawnLightningChain(this.gameObject, enemy, lightningLine);
                //Debug.Break();
            }
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(W, Input.mousePosition) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("W skill");
            
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(E, Input.mousePosition) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("E skill");
            
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(R, Input.mousePosition) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("R skill");
            Instantiate(spookPrefab, this.transform);
            GameDirector.Director.mainLight.GetComponent<Light>().intensity = 0;
            Invoke("lightBackOn", 10);
            
        }
        else {
            RaycastHit retVal = new RaycastHit();
            Physics.Raycast(curentCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out retVal);
            Vector3 wantDir = new Vector3();
            SlimeMovement attackTarget = null;

            if (retVal.collider != null)
            {
                SlimeMovement enemyHit = retVal.transform.gameObject.GetComponent<SlimeMovement>();
                if (enemyHit != null)
                {
                    wantDir = enemyHit.transform.position - this.transform.position;
                    if ((enemyHit.transform.position - this.transform.position).magnitude < attackRange && Input.GetMouseButtonDown(0))
                    {
                        attackTarget = enemyHit;
                    }
                }
                else
                {
                    wantDir = retVal.point + new Vector3(0, this.transform.position.y, 0) - this.transform.position;
                }
                wantDir = wantDir.normalized * moveSpeed;
                /*if (wantDir.magnitude > moveSpeed)
                {
                    wantDir = wantDir.normalized * moveSpeed;
                }
                else if (wantDir.magnitude < moveSpeed / 2)
                {
                    wantDir = wantDir.normalized * moveSpeed/2;
                }*/
                Debug.DrawRay(this.transform.position, wantDir, Color.cyan);
                if (Time.time - moveTime > 0 && Input.GetMouseButtonDown(0))
                {
                    float jump = Random.value > .7 ? moveSpeed : 0;
                    phys.velocity = new Vector3(wantDir.x, phys.velocity.y + jump, wantDir.z);
                    moveTime = Time.time + moveDownTime;
                    //Debug.Log("moved!");
                }

            }

            if (attackTarget && Time.time - nextAttackTime > 0)
            {
                energy += attack(attackTarget);
                nextAttackTime = Time.time + attackCoolDown;
                

            }

        }


        /*Vector3 newMove = new Vector3();
        newMove.y = phys.velocity.y;
        if (forward) newMove += moveSpeed*camForward;
        if (back) newMove += -moveSpeed * camForward;
        if (left) newMove += -moveSpeed * camRight;
        if (right) newMove += moveSpeed * camRight;
        */
        
        //Debug.DrawRay(this.transform.position, camRight, Color.green);
        //Debug.DrawRay(this.transform.position, camForward, Color.red);
        


        //phys.velocity = newMove;
	}
    private float attack(SlimeMovement enemy)
    {
        LightningSegment.spawnLightningChain(this.gameObject, enemy.gameObject, lightningLine);
        enemy.damage(1);
        return 1;
    }
    private void lightBackOn()
    {
        GameDirector.Director.mainLight.GetComponent<Light>().intensity = 1;
        foreach (GameObject enemy in GameDirector.Director.enemiesSpawned){
            enemy.GetComponent<SlimeMovement>().damage(2);
        }
    }

}
