using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour {
	public GameObject ButtonPrefab;
	public GameObject NRPrefab;
	public GameObject AnnoPrefab;
	private GameObject canvas;
	private int last_id;
	public bool intervention_status;

	string postUrl;


	void Start () {
		intervention_status = false;

		Vector3 temp = this.transform.localScale;
		temp.y = 0;
		this.transform.localScale = temp;

		GameObject elliotGO = GameObject.Find("Elliot");
		GetJsonData jsonData = elliotGO.GetComponent<GetJsonData>();
		last_id = jsonData.id;

		GameObject canvas = GameObject.Find("Canvas");

		/*
		GameObject goIS = GameObject.Instantiate(NRPrefab);
		goIS.name = "image_target_status";
		Text IS = goIS.GetComponent<Text> ();
		Color figureblue = new Color (167f, 209f, 218f);
		IS.color = figureblue;
		IS.text = "Image Target 1 Found: ";
		Vector3 tempITF = goIS.transform.localPosition;
		tempITF.x = 604f;
		tempITF.y = -75f;
		goIS.transform.localPosition = tempITF;
		goIS.transform.SetParent (canvas.transform, false);*/

	}
	
	// Update is called once per frame
	void Update () {
		GameObject elliotGO = GameObject.Find("Elliot");
		GetJsonData jsonData = elliotGO.GetComponent<GetJsonData>();
		if (jsonData.id != last_id && jsonData.id > last_id)
		{
			var assets = GameObject.FindGameObjectsWithTag("intervention_asset");
			foreach(GameObject item in assets)
			{
				Destroy(item);
			}
			StartCoroutine (ScaleMenu (0.5f));
			last_id = jsonData.id;
		}
		/*
		GameObject imageT1 = GameObject.Find("ImageTarget1");
		ImageTargetFound imagestatus1 = imageT1.GetComponent<ImageTargetFound>();
		GameObject imageT2 = GameObject.Find("ImageTarget2");
		ImageTargetFound imagestatus2 = imageT2.GetComponent<ImageTargetFound>();

		GameObject status = GameObject.Find("image_target_status");
		Text IS = status.GetComponent<Text> ();
		IS.text = "Target1: " + imagestatus1.found + " , Target2: " + imagestatus2.found; */

	}

	IEnumerator ScaleMenu(float totalTime){
		intervention_status = true;

		GameObject canvas = GameObject.Find("Canvas");
		float elapsedTime = 0f;
		while (elapsedTime < totalTime) {
			this.transform.localScale = Vector3.Lerp (new Vector3 (1, 0, 1), new Vector3 (1, 1.7f, 1), elapsedTime/totalTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		GameObject elliotGO = GameObject.Find("Elliot");
		GetJsonData jsonData = elliotGO.GetComponent<GetJsonData>();

		GameObject goNR = GameObject.Instantiate(NRPrefab);
		goNR.tag = "intervention_asset";
		Text NRid = goNR.GetComponent<Text> ();
		NRid.text = "NEW REQUEST: " + jsonData.id;
		Vector3 tempNR = goNR.transform.localPosition;
		tempNR.x = .41f;
		tempNR.y = -75f;
		goNR.transform.localPosition = tempNR;
		goNR.transform.SetParent (canvas.transform, false);


		//// Instantiate Annotation
		GameObject intervention_location = GameObject.Find("InterventionLocation");
		Vector3 table_location = new Vector3 (jsonData.tileLocation.x, jsonData.tileLocation.y, jsonData.tileLocation.z);
		intervention_location.transform.localPosition = table_location;
		Vector3 table_world_location = intervention_location.transform.position;

		GameObject image_target1 = GameObject.Find("MultiTarget");
		GameObject goAnno1 = GameObject.Instantiate(AnnoPrefab);
		goAnno1.tag = "intervention_asset";
		goAnno1.transform.position = table_world_location;
		goAnno1.transform.SetParent (image_target1.transform);

		/*
		GameObject image_target2 = GameObject.Find("ImageTarget2");
		GameObject goAnno2 = GameObject.Instantiate(AnnoPrefab);
		goAnno2.tag = "intervention_asset";
		goAnno2.transform.position = table_world_location;
		goAnno2.transform.SetParent (image_target2.transform);*/

		Transform canvasTrans = canvas.gameObject.transform;

		for(int i = 0; i < jsonData.choices.Count; i++)
		{
			GameObject go = GameObject.Instantiate(ButtonPrefab);
			go.tag = "intervention_asset";
			Button aButton = go.GetComponent<Button>();
			SetButton(aButton, jsonData.choices_index[i]);
			//SetButton(aButton, jsonData.choices[i]);
			aButton.GetComponentInChildren<Text> ().text = jsonData.choices[i];
			Vector3 tempY = go.transform.localPosition;
			tempY.y = -100f-(i*50);
			go.transform.localPosition = tempY;
			go.transform.SetParent (canvas.transform, false);
		}



		yield break;
	}

	void SetButton(Button button, int choice)
	{
		button.onClick.AddListener (() => { TaskOnClick(choice); });

	}

	void TaskOnClick(int choice_index)
	{
		GameObject elliotGO = GameObject.Find("Elliot");
		GetJsonData jsonData = elliotGO.GetComponent<GetJsonData>();

		string postStr = @"{""requestId"":" + jsonData.id + @",""fieldValues"":[{""multipleChoiceValue"":" + choice_index + "}]}";
		Debug.Log ("json id:" + jsonData.id);
		postUrl = "http://elliot-env.ny4dsiyrsm.us-west-1.elasticbeanstalk.com/v1/intervention/" + jsonData.id + "/respond";

		Hashtable postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");

		var form = System.Text.Encoding.UTF8.GetBytes(postStr);

		WWW www = new WWW(postUrl, form, postHeader);
		StartCoroutine(PostdataEnumerator(www));

		DestroyAllAssets ();
		intervention_status = false;

	}

	void DestroyAllAssets()
	{
		var assets = GameObject.FindGameObjectsWithTag("intervention_asset");
		foreach(GameObject item in assets)
		{
			Destroy(item);
			Vector3 menuZero = new Vector3(1,0,1);
			this.transform.localScale = menuZero;
		}
	}

	void PostData(int Id,string Name)
	{
		
	}

	IEnumerator PostdataEnumerator(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			Debug.Log("Data Submitted");
		}
		else
		{
			Debug.Log(www.error);
		}
	}
}
