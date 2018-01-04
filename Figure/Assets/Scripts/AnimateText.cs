﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimateText : MonoBehaviour
{
	//Time taken for each letter to appear (The lower it is, the faster each letter appear)
	public float letterPaused = 0.005f;
	//Message that will displays till the end that will come out letter by letter
	public string message1;
	public string message2;
	public string message3;
	public string message4;
	//Text for the message to display
	public TextMesh textComp1;
	public TextMesh textComp2;
	public TextMesh textComp3;

	// Use this for initialization
	void Start ()
	{
		//Get text component
		//textComp1 = GetComponent<TextMesh> ();
		//textComp2 = GetComponent<TextMesh> ();
		//Message will display will be at Text
		message1 = "Searching....";
		message2 = "Red Tile";
		//Set the text to be blank first
		textComp1.text = "";
		textComp2.text = "";
		textComp3.text = "";
		//Call the function and expect yield to return
		StartCoroutine (TypeText ());
	}

	IEnumerator TypeText()
	{
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

		yield return new WaitForSeconds(2.0f);
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
		message4 = "98% confidence";
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

