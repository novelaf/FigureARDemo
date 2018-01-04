using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleGraph : MonoBehaviour {
	public float count_c1;
	public float count_c2;
	public float count_c3;
	public float count_c4;
	public float count_c5;

	private float last_c1;
	private float last_c2;
	private float last_c3;
	private float last_c4;

	public float placed_c1;
	public float placed_c2;
	public float placed_c3;
	public float placed_c4;

	// Use this for initialization
	void Start () {
		last_c1 = count_c1;
		last_c2 = count_c2;
		last_c3 = count_c3;
		last_c4 = count_c4;

		placed_c1 = 0f;
		placed_c2 = 0f;
		placed_c3 = 0f;
		placed_c4 = 0f;

		
	}
	
	// Update is called once per frame
	void Update () {

		float max_tiles = 5f;

		GameObject c1 = this.transform.GetChild (0).gameObject;
		GameObject c2 = this.transform.GetChild (1).gameObject;
		GameObject c3 = this.transform.GetChild (2).gameObject;
		GameObject c4 = this.transform.GetChild (3).gameObject;
		GameObject c5 = this.transform.GetChild (4).gameObject;

		Vector3 temp1 = new Vector3 (1, count_c1 / max_tiles, 1);
		Vector3 temp2 = new Vector3 (1, count_c2 / max_tiles, 1); 
		Vector3 temp3 = new Vector3 (1, count_c3 / max_tiles, 1); 
		Vector3 temp4 = new Vector3 (1, count_c4 / max_tiles, 1); 
		Vector3 temp5 = new Vector3 (1, count_c5 / max_tiles, 1); 


		//c1.transform.localScale = temp1;
		//c2.transform.localScale = temp2;
		//c3.transform.localScale = temp3;
		//c4.transform.localScale = temp4;
		//c5.transform.localScale = temp5;

		float duration = 0.75f;
		c1.transform.DOScale (temp1, duration); 
		c2.transform.DOScale (temp2, duration); 
		c3.transform.DOScale (temp3, duration); 
		c4.transform.DOScale (temp4, duration); 
		c5.transform.DOScale (temp5, duration); 


		if (last_c1 != count_c1)
		{
			if (count_c1 == last_c1 - 1f) {
				placed_c1 = placed_c1 + 1f;
				last_c1 = count_c1;
			}else {
				last_c1 = count_c1;
			}
		}

		if (last_c2 != count_c2)
		{
			if (count_c2 == last_c2 - 1f) {
				placed_c2 = placed_c2 + 1f;
				last_c2 = count_c2;
			}else {
				last_c2 = count_c2;
			}
		}

		if (last_c3 != count_c3)
		{
			if (count_c3 == last_c3 - 1f) {
				placed_c3 = placed_c3 + 1f;
				last_c3 = count_c3;
			}else {
				last_c3 = count_c3;
			}
		}

		if (last_c4 != count_c4)
		{
			if (count_c4 == last_c4 - 1f) {
				placed_c4 = placed_c4 + 1f;
				last_c4 = count_c4;
			}else {
				last_c1 = count_c1;
			}
		}
		
	}
}
