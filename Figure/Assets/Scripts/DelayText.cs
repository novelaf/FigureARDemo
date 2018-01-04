using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayText : MonoBehaviour {
	private Text uitext;

	// Use this for initialization
	void Start () {
		uitext = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		float sliderValue = GameObject.Find ("Slider").GetComponent <Slider>().value;
		float rangetime = (sliderValue * .1f) * 5;
		uitext.text = "delay: " + rangetime.ToString ();
	}
}
