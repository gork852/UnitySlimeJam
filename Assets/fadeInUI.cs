using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInUI : MonoBehaviour {
    public float fadeInTime = 0;
    public float fadeInRate = 2;
    public float fadeOutTime = 1;
    public float fadeOutRate = 5;
    private CanvasRenderer rend;
	// Use this for initialization
	void Start () {
        rend = this.GetComponent<CanvasRenderer>();
        rend.SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > fadeOutTime && fadeOutTime>0)
        {
            float fade = 1-(Time.time - fadeOutTime) * fadeOutRate;
            rend.SetAlpha(fade > 0 ? fade : 0);
        }
        else if (Time.time > fadeInTime)
        {
            float fade = (Time.time - fadeInTime) * fadeInRate;
            rend.SetAlpha(fade > 1 ? 1 : fade);
        }
	}
}
