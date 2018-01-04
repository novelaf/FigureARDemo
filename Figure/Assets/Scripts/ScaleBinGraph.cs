using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleBinGraph : MonoBehaviour {
	private WebSocketTest binStats;
	private GameObject navy;
	private GameObject lightblue;
	private GameObject red;
	private GameObject pink;

	// Use this for initialization
	void Start () {
		GameObject binGO = GameObject.Find ("WS");
		binStats = binGO.GetComponent<WebSocketTest> ();

		navy = this.transform.GetChild (0).gameObject;
		lightblue = this.transform.GetChild (1).gameObject;
		red = this.transform.GetChild (2).gameObject;
		pink = this.transform.GetChild (3).gameObject;

		navy.SetActive (false);
		lightblue.SetActive (false);
		red.SetActive (false);
		pink.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		float duration = 0.75f;
		float max_tiles = 5f;

		if (binStats.bin_destination == 1) {
			navy.SetActive (true);
			lightblue.SetActive (false);
			red.SetActive (false);
			pink.SetActive (false);
		} else if (binStats.bin_destination == 2) {
			navy.SetActive (false);
			lightblue.SetActive (true);
			red.SetActive (false);
			pink.SetActive (false);
		} else if (binStats.bin_destination == 3) {
			navy.SetActive (false);
			lightblue.SetActive (false);
			red.SetActive (true);
			pink.SetActive (false);
		} else if (binStats.bin_destination == 4) {
			navy.SetActive (false);
			lightblue.SetActive (false);
			red.SetActive (false);
			pink.SetActive (true);
		} else {
			navy.SetActive (false);
			lightblue.SetActive (false);
			red.SetActive (false);
			pink.SetActive (false);
		}

		/*
		Vector3 temp1 = new Vector3 (1, 1, binStats.tpC1 / max_tiles);
		Vector3 temp2 = new Vector3 (1, 1, binStats.tpC2 / max_tiles); 
		Vector3 temp3 = new Vector3 (1, 1, binStats.tpC3 / max_tiles); 
		Vector3 temp4 = new Vector3 (1, 1, binStats.tpC4 / max_tiles); 

		navy.transform.DOScale (temp1, duration); 
		lightblue.transform.DOScale (temp2, duration); 
		red.transform.DOScale (temp3, duration); 
		pink.transform.DOScale (temp4, duration); */

	}
}
