using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesPlaced : MonoBehaviour {
	private GameObject graph;
	private ScaleGraph graphScript;
	private Text navy_text;
	private Text lightblue_text;
	private Text red_text;
	private Text pink_text;
	private WebSocketTest binStats;

	// Use this for initialization
	void Start () {
		graph = GameObject.Find ("graph");
		graphScript = graph.GetComponent<ScaleGraph> ();

		GameObject navy = this.transform.GetChild (1).gameObject;
		GameObject lightblue = this.transform.GetChild (2).gameObject;
		GameObject red = this.transform.GetChild (3).gameObject;
		GameObject pink = this.transform.GetChild (4).gameObject;

		navy_text = navy.GetComponent<Text> ();
		lightblue_text = lightblue.GetComponent<Text> ();
		red_text = red.GetComponent<Text> ();
		pink_text = pink.GetComponent<Text> ();

		GameObject binGO = GameObject.Find ("WS");
		binStats = binGO.GetComponent<WebSocketTest> ();


		
	}
	
	// Update is called once per frame
	void Update () {

		if (graphScript.placed_c1 < 10) {
			navy_text.text = "0" + Mathf.Round (binStats.tpC1) + "  Navy Blue";
		} else {
			navy_text.text = Mathf.Round (binStats.tpC1) + "  Navy Blue";
		}

		if (graphScript.placed_c2 < 10) {
			lightblue_text.text = "0" + Mathf.Round (binStats.tpC2) + "  Light Blue";
		} else {
			lightblue_text.text = Mathf.Round (binStats.tpC2) + "  Light Blue";
		}

		if (graphScript.placed_c3 < 10) {
			red_text.text = "0" + Mathf.Round (binStats.tpC3) + "  Red";
		} else {
			red_text.text = Mathf.Round (binStats.tpC3) + "  Red";
		}

		if (graphScript.placed_c4 < 10) {
			pink_text.text = "0" + Mathf.Round (binStats.tpC4) + "  Pink";
		} else {
			pink_text.text = Mathf.Round (binStats.tpC4) + "  Pink";
		}


		
	}
}
