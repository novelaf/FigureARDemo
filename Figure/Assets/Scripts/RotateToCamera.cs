using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour {
	private Camera mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forwardDirection_relative = mainCamera.transform.position - this.transform.position;
		Quaternion lookDir = Quaternion.LookRotation (forwardDirection_relative);
		Vector3 newRotation = new Vector3 (this.transform.rotation.x, lookDir.eulerAngles.y, this.transform.rotation.z);
		this.transform.rotation = Quaternion.Euler (newRotation);
	}
}
