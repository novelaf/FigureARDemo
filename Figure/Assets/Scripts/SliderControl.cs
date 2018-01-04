using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour {
	private float A1;
	private float A2;
	private float A3;
	private float A4;
	private float A5;
	private float A6;

	private float rangeX;

	private float A1_range;
	private float A2_range_pos;
	private float A2_range_neg;
	private float A3_range_pos;
	private float A3_range_neg;
	private float A4_range;
	private float A5_range;
	private float A6_range;

	private GameObject A1_go;
	private GameObject A2_go;
	private GameObject A3_go;
	private GameObject A4_go;
	private GameObject A5_go;
	private GameObject A6_go;

	private GameObject A1_pill;
	private GameObject A2_pill;
	private GameObject A3_pill;
	private GameObject A4_pill;
	private GameObject A5_pill;
	private GameObject A6_pill;

	// Use this for initialization
	void Start () {
		
		A1_go = GameObject.Find ("agilus_A1_GEO");
		A2_go = GameObject.Find ("agilus_A2_GEO");
		A3_go = GameObject.Find ("agilus_A3_GEO");
		A4_go = GameObject.Find ("agilus_A4_GEO");
		A5_go = GameObject.Find ("agilus_A5_GEO");
		A6_go = GameObject.Find ("agilus_A6_GEO"); 

		A1_pill = GameObject.Find ("pillA1");
		A2_pill = GameObject.Find ("pillA2");
		A3_pill = GameObject.Find ("pillA3");
		A4_pill = GameObject.Find ("pillA4");
		A5_pill = GameObject.Find ("pillA5");
		A6_pill = GameObject.Find ("pillA6");


		A1_range = 170f;
		A2_range_pos = -45f;
		A2_range_neg = 120f;
		A3_range_pos = -156f;
		A3_range_neg = 120f;
		A4_range = 360f;
		A5_range = 120f;
		A6_range = 350f;

		rangeX = .0852f;
	}

	// Update is called once per frame
	void Update () {
		A1 = A1_go.transform.localEulerAngles.z;
		A2 = A2_go.transform.localEulerAngles.y;
		A3 = A3_go.transform.localEulerAngles.y;
		A4 = A4_go.transform.localRotation.eulerAngles.x;
		A5 = A5_go.transform.localEulerAngles.y;
		A6 = A6_go.transform.localEulerAngles.x;

		A1 = (A1 > 180) ? A1 - 360 : A1;
		A2 = (A2 > 180) ? A2 - 360 : A2;
		A3 = (A3 > 180) ? A3 - 360 : A3;
		A4 = (A4 > 180) ? A4 - 360 : A4;
		A5 = (A5 > 180) ? A5 - 360 : A5;
		A6 = (A6 > 180) ? A6 - 360 : A6;


		float A1_remapped = Remap (A1, -A1_range, A1_range, -rangeX, rangeX);
		float A2_remapped = Remap (A2, A2_range_neg, A2_range_pos, -rangeX, rangeX);
		float A3_remapped = Remap (A3, A3_range_neg, A3_range_pos, -rangeX, rangeX);
		float A4_remapped = Remap (A4, -A4_range, A4_range, -rangeX, rangeX);
		float A5_remapped = Remap (A5, -A5_range, A5_range, -rangeX, rangeX);
		float A6_remapped = Remap (A6, -A6_range, A6_range, -rangeX, rangeX);

		Vector3 tempA1 = new Vector3 (A1_remapped, A1_pill.transform.localPosition.y, A1_pill.transform.localPosition.z);
		Vector3 tempA2 = new Vector3 (A2_remapped, A2_pill.transform.localPosition.y, A2_pill.transform.localPosition.z);
		Vector3 tempA3 = new Vector3 (A3_remapped, A3_pill.transform.localPosition.y, A3_pill.transform.localPosition.z);
		Vector3 tempA4 = new Vector3 (A4_remapped, A4_pill.transform.localPosition.y, A4_pill.transform.localPosition.z);
		Vector3 tempA5 = new Vector3 (A5_remapped, A5_pill.transform.localPosition.y, A5_pill.transform.localPosition.z);
		Vector3 tempA6 = new Vector3 (A6_remapped, A6_pill.transform.localPosition.y, A6_pill.transform.localPosition.z);



		A1_pill.transform.localPosition = tempA1;
		A2_pill.transform.localPosition = tempA2;
		A3_pill.transform.localPosition = tempA3;
		A4_pill.transform.localPosition = tempA4;
		A5_pill.transform.localPosition = tempA5;
		A6_pill.transform.localPosition = tempA6;


	}

	float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
