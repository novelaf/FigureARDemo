using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BinControl : MonoBehaviour {
	WebSocketTest bin;
	private int last_bin;
	private GameObject bin1;
	private GameObject bin2;
	private GameObject bin3;
	private GameObject bin4;

	private TextMesh bin1_text;
	private TextMesh bin2_text;
	private TextMesh bin3_text;
	private TextMesh bin4_text;

	private bool binbool;
	private Vector3 textPosition;
	private int textTP;

	//Time taken for each letter to appear (The lower it is, the faster each letter appear)
	public float letterPaused = 0.003f;
	//Message that will displays till the end that will come out letter by letter
	private string message1;
	private string message2;
	private string message3;
	private string message4;
	//Text for the message to display
	public TextMesh textComp1;
	public TextMesh textComp2;
	public TextMesh textComp3;

	public GameObject text3d;
	// Use this for initialization
	void Start () {
		GameObject binGO = GameObject.Find ("WS");
		bin = binGO.GetComponent<WebSocketTest> ();

		bin1 = this.transform.GetChild (0).gameObject;
		bin2 = this.transform.GetChild (1).gameObject;
		bin3 = this.transform.GetChild (2).gameObject;
		bin4 = this.transform.GetChild (3).gameObject;

		GameObject bin1_child = bin1.transform.GetChild (9).gameObject;
		GameObject bin2_child = bin2.transform.GetChild (9).gameObject;
		GameObject bin3_child = bin3.transform.GetChild (9).gameObject;
		GameObject bin4_child = bin4.transform.GetChild (9).gameObject;

		bin1_text = bin1_child.GetComponent<TextMesh> ();
		bin2_text = bin2_child.GetComponent<TextMesh> ();
		bin3_text = bin3_child.GetComponent<TextMesh> ();
		bin4_text = bin4_child.GetComponent<TextMesh> ();

		bin1_text.text = "";
		bin2_text.text = "";
		bin3_text.text = "";
		bin4_text.text = "";

		bin1.SetActive (true);
		bin2.SetActive (true);
		bin3.SetActive (true);
		bin4.SetActive (true);
		//this.transform.DOScaleY (0.0f, 1.0f);


		message1 = "";
		message2 = "";
		//Set the text to be blank first
		textComp1.text = "";
		textComp2.text = "";
		textComp3.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (bin.bin_destination != last_bin) {
			// no animation

			//Sequence mySequence = DOTween.Sequence();
			//mySequence.Append(this.transform.DOScaleY (0.0f, 0.75f));
			//mySequence.Append(this.transform.DOScaleY (0.003f, 0.75f));

			StartCoroutine (HideBins(bin.bin_destination, 0.75f));

			if (bin.bin_destination != 5) {
				textComp1.text = "";
				textComp2.text = "";
				textComp3.text = "";

				StartCoroutine (TypeText (bin.bin_destination));
			} else {
				textComp1.text = "";
				textComp2.text = "";
				textComp3.text = "";
			}
		}

		Debug.Log ("bin destination : " + bin.bin_destination);


		last_bin = bin.bin_destination;
	}

	IEnumerator HideBins(int bin_num, float wait_time){
		
		yield return new WaitForSeconds(wait_time);

		/*
		var assets = GameObject.FindGameObjectsWithTag("3dtext");
		foreach(GameObject item in assets)
		{
			Destroy(item);
		}*/

		bin1_text.text = "0" + Mathf.Round (bin.tpC1);
		bin2_text.text = "0" + Mathf.Round (bin.tpC2);
		bin3_text.text = "0" + Mathf.Round (bin.tpC3);
		bin4_text.text = "0" + Mathf.Round (bin.tpC4);


		if (bin_num == 1) {
			/*
			bin1.SetActive (true);
			bin2.SetActive (false);
			bin3.SetActive (false);
			bin4.SetActive (false);*/

			textPosition = new Vector3 (bin1.transform.position.x, bin1.transform.position.y + .1f, bin1.transform.position.z + .07f);
			binbool = true;
			textTP = (int)bin.tpC1;

		} else if (bin_num == 2) {
			/*
			bin1.SetActive (false);
			bin2.SetActive (true);
			bin3.SetActive (false);
			bin4.SetActive (false);*/


			textPosition = new Vector3 (bin2.transform.position.x, bin2.transform.position.y + .1f, bin2.transform.position.z + .07f);
			binbool = true;
			textTP = (int)bin.tpC2;


		} else if (bin_num == 3) {
			/*
			bin1.SetActive (false);
			bin2.SetActive (false);
			bin3.SetActive (true);
			bin4.SetActive (false);*/

			textPosition = new Vector3 (bin3.transform.position.x, bin3.transform.position.y + .1f, bin3.transform.position.z + .07f);
			binbool = true;
			textTP = (int)bin.tpC3;


		} else if (bin_num == 4) {
			/*
			bin1.SetActive (false);
			bin2.SetActive (false);
			bin3.SetActive (false);
			bin4.SetActive (true);*/

			textPosition = new Vector3 (bin4.transform.position.x, bin4.transform.position.y + .1f, bin4.transform.position.z + .07f);
			binbool = true;
			textTP = (int)bin.tpC4;

		} else {
			/*
			bin1.SetActive (false);
			bin2.SetActive (false);
			bin3.SetActive (false);
			bin4.SetActive (false);*/

			binbool = false;
		}

		/*
		if (binbool == true) {
			GameObject textclone = Instantiate (text3d);
			textclone.tag = ("3dtext");
			textclone.transform.position = textPosition;
			Debug.Log ("text TP " + textTP);
			int children = textclone.transform.childCount;
			for (int i = 0; i < children; ++i) {
				GameObject child = textclone.transform.GetChild (i).gameObject;
				if (i == textTP) {
					child.SetActive (true);
				} else {
					child.SetActive (false);
				}
			}
		} */

		yield break;

	}


	IEnumerator TypeText(int bin_num)
	{
		if (bin_num == 1) {
			message2 = "Navy Blue Tile";
			textComp2.color = new Color(38f/255f, 122f/255f, 191f/255f);
		} else if (bin_num == 2) {
			message2 = "Light Blue Tile";
			textComp2.color = new Color(109f/255f, 194f/255f, 252f/255f);
		} else if (bin_num == 3) {
			message2 = "Red Tile";
			textComp2.color = new Color(255f/255f, 62f/255f, 62f/255f);
		} else if (bin_num == 4) {
			message2 = "Pink Tile";
			textComp2.color = new Color(237f/255f, 110f/255f, 110f/255f);
		}

		message1 = "Searching....";
		//Split each char into a char array
		foreach (char letter1 in message1.ToCharArray()) 
		{
			//Add 1 letter each
			textComp1.text += letter1;
			yield return 0;
			yield return new WaitForSeconds(letterPaused);
		}
			
		//yield return new WaitForSeconds(1.0f);
		foreach (char letter2 in message2.ToCharArray()) 
		{
			//Add 1 letter each
			textComp2.text += letter2;
			yield return 0;
			yield return new WaitForSeconds(letterPaused);
		}

		yield return new WaitForSeconds(0.75f);
		textComp1.text = "";
		message3 = "Sorting....";
		foreach (char letter3 in message3.ToCharArray()) 
		{
			//Add 1 letter each
			textComp1.text += letter3;
			yield return 0;
			yield return new WaitForSeconds(letterPaused);
		}

		textComp3.text = "";
		double percentage = Math.Round (bin.highestPercentage * 100, 1);
		message4 = percentage.ToString() + "% confidence";
		yield return new WaitForSeconds(0.5f);
		foreach (char letter4 in message4.ToCharArray()) 
		{
			//Add 1 letter each
			textComp3.text += letter4;
			yield return 0;
			yield return new WaitForSeconds(letterPaused);
		}

		yield break;

	}
}
