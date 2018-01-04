using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float pp = Mathf.PingPong (Time.time/20f, .05f);
		this.transform.localScale = new Vector3 (pp + .2f, pp + .2f, pp + .2f);
	}
}
