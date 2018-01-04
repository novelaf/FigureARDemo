using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour {
	public float c1;
	public float c2;
	public float c3;
	public float c4;
	public float c5;
	List<float> confidenceList;
	List<GradientColorKey> keyList;
	private Color color;

	// Use this for initialization
	void Start () {
		c1 = 0.90f;
		c2 = 0.05f;
		c3 = 0.021f;
		c4 = 0.019f;
		c5 = 0.01f;

		float count = 0f;
		confidenceList = new List<float>();
		keyList = new List<GradientColorKey>();

		confidenceList.Add(c1);
		confidenceList.Add(c2);
		confidenceList.Add(c3);
		confidenceList.Add(c4);
		confidenceList.Add(c5);
		confidenceList.Sort ();
		Debug.Log (confidenceList[0]);
		//confidenceList.Reverse ();

		LineRenderer lineRenderer = this.gameObject.GetComponent<LineRenderer>();
		Gradient colorGrad = new Gradient ();

		for (int i = 0; i < 5; i++) {
			count = count + confidenceList [i];

			if (confidenceList [i] == c1) {
				Debug.Log ("RED");
				GradientColorKey key = new GradientColorKey (Color.red, count);
				keyList.Add (key);
			} else if(confidenceList [i] == c2) {
				Debug.Log ("BLUE");
				GradientColorKey key = new GradientColorKey (Color.blue, count);
				keyList.Add (key);
			} else if(confidenceList [i] == c3) {
				Debug.Log ("GREEN");
				GradientColorKey key = new GradientColorKey (Color.green, count);
				keyList.Add (key);
			} else if(confidenceList [i] == c4) {
				Debug.Log ("GRAY");
				GradientColorKey key = new GradientColorKey (Color.gray, count);
				keyList.Add (key);
			} else if(confidenceList [i] == c5) {
				Debug.Log ("YELLOW");
				GradientColorKey key = new GradientColorKey (Color.yellow, count);
				keyList.Add (key);
			}

		}

		//lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		colorGrad.SetKeys(
			new GradientColorKey[] { keyList[0], keyList[1], keyList[2], keyList[3], keyList[4] },
			new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
			);
			
		lineRenderer.colorGradient = colorGrad;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
