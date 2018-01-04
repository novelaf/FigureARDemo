using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using System.IO;
using System.Net.Sockets;

public class WebSocketTest : MonoBehaviour {
	public GameObject AnnoPrefab;
	public float tpC1;
	public float tpC2;
	public float tpC3;
	public float tpC4;

	public float lowestPercentage;
	public float highestPercentage;

	public int bin_destination;
	private int bin;



	IEnumerator Start () {

		// Connect to Ros (websocket) server
		WebSocket w = new WebSocket(new Uri("ws://10.1.10.14:9011/"));
		yield return StartCoroutine(w.Connect());



		//w.SendString("Hi there");
		int i=0;

		// Listen for ROS data on websocket
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{
				

				//Debug.Log ("Received: "+reply);
				TileClass tiles = JsonUtility.FromJson<TileClass>(reply);
				if (tiles.tiles.Count != 0) {
					var assets = GameObject.FindGameObjectsWithTag("confidence_asset");
					foreach(GameObject item in assets)
					{
						Destroy(item);
					}

					var assets_picked = GameObject.FindGameObjectsWithTag("confidence_asset_picked");
					foreach(GameObject item_picked in assets_picked)
					{
						Destroy(item_picked);
					}


					int c1_count = 0;
					int c2_count = 0;
					int c3_count = 0;
					int c4_count = 0;
					int c5_count = 0;


					tpC1 = tiles.bins.c1;
					tpC2 = tiles.bins.c2;
					tpC3 = tiles.bins.c3;
					tpC4 = tiles.bins.c4;



					List<float> percentageList = new List<float> ();

					/// get range of confidence
					for (int j = 0; j < tiles.tiles.Count; j++) {
						List<float> confidenceHighList = new List<float> ();

						confidenceHighList.Add (tiles.tiles [j].c1);
						confidenceHighList.Add (tiles.tiles [j].c2);
						confidenceHighList.Add (tiles.tiles [j].c3);
						confidenceHighList.Add (tiles.tiles [j].c4);
						confidenceHighList.Add (tiles.tiles [j].c5);

						confidenceHighList.Sort ();
						percentageList.Add (confidenceHighList [4]);
					}

					percentageList.Sort ();
					lowestPercentage = percentageList [0];
					highestPercentage = percentageList [percentageList.Count - 1];

					List<GameObject> annoList = new List<GameObject> ();
					int rightmost_index = 0;
					float rightmost_xposition = tiles.tiles [0].x;

					for (int j = 0; j < tiles.tiles.Count; j++) {
						GameObject confidence_location = GameObject.Find ("InterventionLocation");
						Vector3 table_location = new Vector3 (tiles.tiles [j].x, tiles.tiles [j].y, 0f);
						confidence_location.transform.localPosition = table_location;
						Vector3 table_world_location = confidence_location.transform.position;

						GameObject image_target1 = GameObject.Find ("MultiTarget");
						GameObject goAnno = GameObject.Instantiate (AnnoPrefab);
						goAnno.tag = "confidence_asset";
						goAnno.transform.position = table_world_location;
						goAnno.transform.SetParent (image_target1.transform);
						annoList.Add (goAnno);

						ConfidenceAttributes goConfidence = goAnno.GetComponent<ConfidenceAttributes> ();

						goConfidence.c1 = tiles.tiles [j].c1;
						goConfidence.c2 = tiles.tiles [j].c2;
						goConfidence.c3 = tiles.tiles [j].c3;
						goConfidence.c4 = tiles.tiles [j].c4;
						goConfidence.c5 = tiles.tiles [j].c5;

						List<float> confidenceList = new List<float> ();

						confidenceList.Add (tiles.tiles [j].c1);
						confidenceList.Add (tiles.tiles [j].c2);
						confidenceList.Add (tiles.tiles [j].c3);
						confidenceList.Add (tiles.tiles [j].c4);
						confidenceList.Add (tiles.tiles [j].c5);

						confidenceList.Sort ();
						float percent_remap = Remap (confidenceList [4], highestPercentage, lowestPercentage, 0f, 1f);
						goConfidence.range = percent_remap;



						if (confidenceList [4] == tiles.tiles [j].c1) {
							c1_count = c1_count + 1;
							bin = 1;
						} else if (confidenceList [4] == tiles.tiles [j].c2) {
							c2_count = c2_count + 1;
							bin = 2;
						} else if (confidenceList [4] == tiles.tiles [j].c3) {
							c3_count = c3_count + 1;
							bin = 3;
						} else if (confidenceList [4] == tiles.tiles [j].c4) {
							c4_count = c4_count + 1;
							bin = 4;
						} else if (confidenceList [4] == tiles.tiles [j].c5) {
							c5_count = c5_count + 1;
							bin = 5;
						}

						if (j == 0) {
							bin_destination = bin;
							Debug.Log("bin destination web sock: " + bin_destination);
						}

						// find out which tile is going to be placed next and which bin it is going to be placed in
						if (confidence_location.transform.localPosition.x < rightmost_xposition) {
							rightmost_index = j;
							rightmost_xposition = confidence_location.transform.localPosition.x;
							bin_destination = bin;
							Debug.Log("bin destination web sock: " + bin_destination);

						}
					}

					annoList [rightmost_index].tag = "confidence_asset_picked";


					/// send counts to graph
					GameObject graphGO = GameObject.Find ("graph_vertical");
					ScaleGraph scaleGraph = graphGO.GetComponent<ScaleGraph> ();
					scaleGraph.count_c1 = c1_count;
					scaleGraph.count_c2 = c2_count;
					scaleGraph.count_c3 = c3_count;
					scaleGraph.count_c4 = c4_count;
					scaleGraph.count_c5 = c5_count;
				}
			}
			if (w.error != null)
			{
				Debug.LogError ("Error: "+w.error);
				break;
			}
			yield return 0;
		}
		w.Close();
	}

	float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}


}
