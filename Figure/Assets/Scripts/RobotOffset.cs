using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotOffset : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float sliderValueX = GameObject.Find ("Slider_robotX").GetComponent <Slider>().value;
		float sliderValueY = GameObject.Find ("Slider_robotY").GetComponent <Slider>().value;
		float sliderValueZ = GameObject.Find ("Slider_robotZ").GetComponent <Slider>().value;

		Vector3 offset = new Vector3 (sliderValueX, sliderValueY, sliderValueZ);
		this.transform.localPosition = offset;
	}
}
