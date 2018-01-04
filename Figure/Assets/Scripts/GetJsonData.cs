using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GetJsonData : MonoBehaviour
{
	string Url;
	ElliotInterventions el;
	public List<string> choices;
	public List<int> choices_index;
	public int id;
	public Vector3 tileLocation;
	public string imageLocation;
	private int frames = 0;

	void Start()
	{
		Url = "http://elliot-env.ny4dsiyrsm.us-west-1.elasticbeanstalk.com/v1/interventions?status=pending&count=1";
		GetData();
		/*
		StreamReader reader = new StreamReader("Assets/test.json");
		string json = reader.ReadToEnd();
		//Elliot el = JsonUtility.FromJson<Elliot>(json);
		//Debug.Log(el.width);
		//Debug.Log(JsonUtility.FromJson<Elliot>(json));*/
	}

	void Update()
	{
		frames++;
		if (frames % 10 == 0) {
			GetData ();
		}

	}

	void GetData()
	{
		//sending the request to url
		WWW www = new WWW(Url);
		StartCoroutine(GetEnumerator(www));
	}

	IEnumerator GetEnumerator(WWW www)
	{
		//Wait for request to complete
		yield return www;
		//imageLocation = "none";
		choices.Clear ();
		choices_index.Clear ();

		// Parse JSON into object
		string json = "{\"interventions\":" + www.text + "}";
		ElliotInterventions el = JsonUtility.FromJson<ElliotInterventions>(json);
		if (el.interventions.Count > 0) {
			id = el.interventions [0].id;
			//tileLocation = new Vector3 (0.9144f, 0.3048f, 0f);
			tileLocation = new Vector3 (el.interventions [0].form.fields [0].customData.table_x, el.interventions [0].form.fields [0].customData.table_y, 0f);
			if (el.interventions [0].form.fields [0].multipleChoiceData.media.images.Count > 0) {
				imageLocation = el.interventions [0].form.fields [0].multipleChoiceData.media.images [0].url;
			} else {
				imageLocation = "none";
			}
			for (int i = 0; i < el.interventions [0].form.fields [0].multipleChoiceData.options.Count; i++) {
				choices.Add (el.interventions [0].form.fields [0].multipleChoiceData.options [i]);
				choices_index.Add (i);
			}
		}

	}

}
