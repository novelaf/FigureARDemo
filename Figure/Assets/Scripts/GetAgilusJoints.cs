using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAgilusJoints : MonoBehaviour {
	public float A1;
	public float A2;
	public float A3;
	public float A4;
	public float A5;
	public float A6;

	private GameObject A1_go;
	private GameObject A2_go;
	private GameObject A3_go;
	private GameObject A4_go;
	private GameObject A5_go;
	private GameObject A6_go;

	// Use this for initialization
	void Start () {
		A1_go = GameObject.Find ("agilus_A1_GEO");
		A2_go = GameObject.Find ("agilus_A2_GEO");
		A3_go = GameObject.Find ("agilus_A3_GEO");
		A4_go = GameObject.Find ("agilus_A4_GEO");
		A5_go = GameObject.Find ("agilus_A5_GEO");
		A6_go = GameObject.Find ("agilus_A6_GEO");
	}
	
	// Update is called once per frame
	void Update () {
		A1 = A1_go.transform.localEulerAngles.z;
		A2 = A1_go.transform.localEulerAngles.y;
		A3 = A1_go.transform.localEulerAngles.y;
		A4 = A1_go.transform.localEulerAngles.x;
		A5 = A1_go.transform.localEulerAngles.y;
		A6 = A1_go.transform.localEulerAngles.x;
	}
}
