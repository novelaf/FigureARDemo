using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pulse3dText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.DOScale (new Vector3 (.001f, .001f, .001f), 1).SetRelative ().SetLoops (-1, LoopType.Yoyo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
