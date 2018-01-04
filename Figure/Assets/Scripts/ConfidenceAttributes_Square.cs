using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfidenceAttributes_Square : MonoBehaviour {
	public float c1;
	public float c2;
	public float c3;
	public float c4;
	public float c5;
	public float range;
	List<float> confidenceList;
	List<GradientColorKey> keyList;
	private Color color;

	// Use this for initialization
	void Start () {

		float lineScaleZ = Remap (range, 0f, 1f, .5f, 1f);

		Vector3 lineScale = new Vector3 (1, 1, lineScaleZ);
		GameObject line = this.transform.GetChild (1).gameObject;
		line.transform.localScale = lineScale;

		float squareTY = Remap (range, 0f, 1f, .65f, 1.15f);
		Vector3 squareTranslate = new Vector3 (0, squareTY, 0);
		GameObject square_outline = this.transform.GetChild (2).gameObject;
		GameObject square = this.transform.GetChild (3).gameObject;
		square_outline.transform.localPosition = squareTranslate;
		square.transform.localPosition = squareTranslate;

		float textTY = Remap (range, 0f, 1f, .68f, 1.18f);
		Vector3 textTranslate = new Vector3 (0, textTY, 0);
		GameObject text = this.transform.GetChild (0).gameObject;
		text.transform.localPosition = textTranslate;

		TextMesh textmesh = text.GetComponent<TextMesh> ();
		SpriteRenderer sprite_outline = square_outline.GetComponent<SpriteRenderer> ();
		SpriteRenderer sprite_square = square.GetComponent<SpriteRenderer> ();

		float squareAlpha = Remap (range, 0f, 1f, 50f, 255f);

		float lineAlpha = Remap (range, 0f, 1f, .5f, 1f);


		float count = 0f;
		confidenceList = new List<float>();
		keyList = new List<GradientColorKey>();

		confidenceList.Add(c1);
		confidenceList.Add(c2);
		confidenceList.Add(c3);
		confidenceList.Add(c4);
		confidenceList.Add(c5);
		confidenceList.Sort ();

		textmesh.text = Math.Round(confidenceList [4] * 100,1) + "%";

		LineRenderer lineRenderer = line.gameObject.GetComponent<LineRenderer>();
		Gradient colorGrad = new Gradient ();

		for (int i = 0; i < 5; i++) {
			count = count + confidenceList [i];

			if (confidenceList [i] == c1) {
				// navy blue
				Color color = new Color (25f/255f, 25f/255f, 112f/255f, squareAlpha/255f);
				GradientColorKey key = new GradientColorKey (color, count);
				keyList.Add (key);
				if (i == 4) {
					sprite_outline.color = color;
				}
			} else if(confidenceList [i] == c2) {
				// light blue
				Color color = new Color (173f/255f, 216f/255f, 230f/255f, squareAlpha/255f);
				GradientColorKey key = new GradientColorKey (color, count);
				keyList.Add (key);
				if (i == 4) {
					sprite_outline.color = color;
				}
			} else if(confidenceList [i] == c3) {
				// red
				Color color = new Color (220f/255f, 20f/255f, 60f/255f, squareAlpha/255f);
				GradientColorKey key = new GradientColorKey (color, count);
				keyList.Add (key);
				if (i == 4) {
					sprite_outline.color = color;
				}
			} else if(confidenceList [i] == c4) {
				// pink
				Color color = new Color (250f/255f, 128f/255f, 114f/255f, squareAlpha/255f);
				GradientColorKey key = new GradientColorKey (color, count);
				keyList.Add (key);
				if (i == 4) {
					sprite_outline.color = color;
				}
			} else if(confidenceList [i] == c5) {
				// upside down
				Color color = new Color (178f/255f, 131f/255f, 96f/255f, squareAlpha/255f);
				GradientColorKey key = new GradientColorKey (color, count);
				keyList.Add (key);
				if (i == 4) {
					sprite_outline.color = color;
				}
			}

		}

		colorGrad.SetKeys(
			new GradientColorKey[] { keyList[0], keyList[1], keyList[2], keyList[3], keyList[4] },
			new GradientAlphaKey[] { new GradientAlphaKey(0.25f, 0.0f), new GradientAlphaKey(lineAlpha, 1.0f) }
		);

		lineRenderer.colorGradient = colorGrad;

	}

	float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
