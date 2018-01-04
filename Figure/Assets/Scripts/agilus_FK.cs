using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agilus_FK : MonoBehaviour {
	public float A1;
	public float A2;
	public float A3;
	public float A4;
	public float A5;
	public float A6;

	private GameObject goA1;
	private GameObject goA2;
	private GameObject goA3;
	private GameObject goA4;
	private GameObject goA5;
	private GameObject goA6;


	// Use this for initialization
	void Start () {
		goA1 = this.transform.GetChild (0).gameObject;
		Debug.Log (goA1);
		goA2 = goA1.transform.GetChild (0).gameObject;
		goA3 = goA2.transform.GetChild (0).gameObject;
		goA4 = goA3.transform.GetChild (0).gameObject;
		goA5 = goA4.transform.GetChild (0).gameObject;
		goA6 = goA5.transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempA1 = new Vector3 (0, A1, 0);
		goA1.transform.localEulerAngles = tempA1;

		Vector3 tempA2 = new Vector3 (0, 0, A2);
		goA2.transform.localEulerAngles = tempA2;

		Vector3 tempA3 = new Vector3 (0, 0, A3);
		goA3.transform.localEulerAngles = tempA3;

		Vector3 tempA4 = new Vector3 (A4, 0, 0);
		goA4.transform.localEulerAngles = tempA4;

		Vector3 tempA5 = new Vector3 (0, 0, A5);
		goA5.transform.localEulerAngles = tempA5;

		Vector3 tempA6 = new Vector3 (A6, 0, 0);
		goA6.transform.localEulerAngles = tempA6;
	}
}
