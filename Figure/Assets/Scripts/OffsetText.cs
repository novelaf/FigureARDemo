using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffsetText : MonoBehaviour {
	private Text uitext;

	// Use this for initialization
	void Start () {
		uitext = this.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		float sliderValueX = GameObject.Find ("Slider_robotX").GetComponent <Slider>().value;
		float sliderValueY = GameObject.Find ("Slider_robotY").GetComponent <Slider>().value;
		float sliderValueZ = GameObject.Find ("Slider_robotZ").GetComponent <Slider>().value;


		uitext.text = "offset: x:" + sliderValueX.ToString() + ", y:" + sliderValueY.ToString() + ", z:" + sliderValueZ.ToString();
	}
}
