using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SafetyVolumeCheck : MonoBehaviour {
	public GameObject target;
	public GameObject curtain;
	private BoxCollider cube;
	private bool insideLast;
	private bool isInside;
	// Use this for initialization
	void Start () {
		cube = this.gameObject.GetComponent<BoxCollider> ();
		isInside = false;
		insideLast = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			isInside = cube.bounds.Contains (target.transform.position);
			if (isInside == true && insideLast == false) {
				curtain.transform.DOScale (new Vector3 (1f, .5f, 1f), 1f);
			} else if (isInside == false && insideLast == true) {
				curtain.transform.DOScale (new Vector3 (1f, 0f, 1f), 1f);
			}
			insideLast = isInside;
		}
	}
}
