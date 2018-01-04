using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAnimation : MonoBehaviour {
	private Animation anim;
	private AnimationCurve curve;
	private Keyframe[] ks;

	void Start() {
		AnimationClip clip = new AnimationClip();
		this.gameObject.AddComponent<Animation> ();
		anim = GetComponent<Animation>();
		ks = new Keyframe[50];
		int i = 0;
		while (i < ks.Length) {
			ks[i] = new Keyframe(i, i * i);
			i++;
		}
		curve = new AnimationCurve(ks);
		int key_length = curve.length;
		for (int k = 0; k < key_length; k++) {
			curve.SmoothTangents (k, 0);
		}
		clip.legacy = true;
		clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
		anim.AddClip(clip, "test");
		anim.playAutomatically = false;
		anim.Play("test");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
