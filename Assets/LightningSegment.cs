using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSegment : MonoBehaviour {
    private float spawnTime;
    public float timeToLive = .1f;
	// Use this for initialization
	void Start () {
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnTime + timeToLive < Time.time)
        {
            Destroy(this.gameObject);
        }
	}
    public static GameObject spawnLightningChain(GameObject origin, GameObject target, GameObject prefabLine)
    {
        GameObject LightningSkill = new GameObject();
        LightningSkill.AddComponent<LightningSegment>();
        Vector3 startPoint = origin.transform.position;

        LineRenderer segment = Instantiate(prefabLine, LightningSkill.transform).GetComponent<LineRenderer>();

        
        

        
        
        segment.useWorldSpace = true;
        segment.positionCount = 4;
        segment.SetPositions(new[] { origin.transform.position,
            (origin.transform.position*4 + target.transform.position) / 5 + new Vector3(Random.value/2-1/4f, 1+Random.value, Random.value/2-1/4f),
            (origin.transform.position + target.transform.position*3) / 4 + new Vector3(Random.value/2-1/4f, 1+Random.value, Random.value/2-1/4f),
            target.transform.position });

        return LightningSkill;
    }
}
