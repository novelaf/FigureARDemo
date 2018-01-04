using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeSinceEvent : MonoBehaviour {
	private TextMesh time_text;
	private OpenMenu om;

	private float timer;
	private float last_intervention;
	// Use this for initialization
	void Start () {
		time_text = this.gameObject.GetComponent<TextMesh> ();
		GameObject menu = GameObject.Find ("Menu");
		om = menu.GetComponent<OpenMenu> ();

		last_intervention = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (om.intervention_status == true) {
			last_intervention = Time.time;
		}

		timer = Time.time - last_intervention;

		string minutes = Mathf.Floor(timer / 60).ToString("00");
		string seconds = (timer % 60).ToString("00");

		time_text.text = minutes + ":" + seconds;
	}
}
