using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using System.IO;
using System.Net.Sockets;

public class WebSocketStreaming : MonoBehaviour {

	IEnumerator Start () {

		// Connect to Ros (websocket) server
		WebSocket w = new WebSocket(new Uri("ws://10.1.10.14:9013/"));
		yield return StartCoroutine(w.Connect());

		GameObject A1go = GameObject.Find ("agilus_A1_GEO_stream");
		GameObject A2go = GameObject.Find ("agilus_A2_GEO_stream");
		GameObject A3go = GameObject.Find ("agilus_A3_GEO_stream");
		GameObject A4go = GameObject.Find ("agilus_A4_GEO_stream");
		GameObject A5go = GameObject.Find ("agilus_A5_GEO_stream");
		GameObject A6go = GameObject.Find ("agilus_A6_GEO_stream");

		// Listen for ROS data on websocket
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{
				

				//Debug.Log ("Received Joints: "+reply);
				JointClass joints = JsonUtility.FromJson<JointClass>(reply);
				//Debug.Log ("A1: "+ joints.position[0]);

				float degA1 = joints.position [0] * Mathf.Rad2Deg;
				float degA2 = -joints.position[1] * Mathf.Rad2Deg;
				float degA3 = -joints.position[2] * Mathf.Rad2Deg;
				float degA4 = -joints.position[3] * Mathf.Rad2Deg;
				float degA5 = -joints.position[4] * Mathf.Rad2Deg;
				float degA6 = -joints.position[5] * Mathf.Rad2Deg;

				Vector3 tempA1 = new Vector3 (0f, 0f, degA1);
				Vector3 tempA2 = new Vector3 (0f, degA2, 0f);
				Vector3 tempA3 = new Vector3 (0f, degA3, 0f);
				Vector3 tempA4 = new Vector3 (degA4, 0f, 0f);
				Vector3 tempA5 = new Vector3 (0f, degA5, 0f);
				Vector3 tempA6 = new Vector3 (degA6, 0f, 0f);

				A1go.transform.localEulerAngles = tempA1;
				A2go.transform.localEulerAngles = tempA2;
				A3go.transform.localEulerAngles = tempA3;
				A4go.transform.localEulerAngles = tempA4;
				A5go.transform.localEulerAngles = tempA5;
				A6go.transform.localEulerAngles = tempA6;


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
}

