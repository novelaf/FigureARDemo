using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTexture : MonoBehaviour {
	private string url;
	private string last_url;
	private GetJsonData json;
	private bool is_loading;

	// Use this for initialization
	void Start () {
		//url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
		//url = "https://s3-us-west-1.amazonaws.com/media.elliot.figure.works/1510683892201";
		GameObject goElliot = GameObject.Find ("Elliot");
		json = goElliot.GetComponent<GetJsonData> ();
		url = json.imageLocation;
		last_url = url;
		//StartCoroutine (AddTexture());

		//StartCoroutine (AddTexture (url));
	}
	
	// Update is called once per frame
	void Update () {
		url = json.imageLocation;
		if (url != last_url) {
			last_url = json.imageLocation;
			StartCoroutine (AddTexture ());
		}
	}


	IEnumerator AddTexture(){
		GameObject goElliot = GameObject.Find ("Elliot");
		json = goElliot.GetComponent<GetJsonData> ();

		WWW www = new WWW(json.imageLocation);

		// Wait for download to complete
		yield return www;

		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = www.texture;

		if (json.imageLocation == "none") {
			Color blank = new Color (1f, 1f, 1f, 0.25f);
			renderer.material.color = blank;
		} else {
			Color opaque = new Color (1f, 1f, 1f, 0.75f);
			renderer.material.color = opaque;
		}

		//is_loading = false;
		//yield break;
	}
}
