using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using System.IO;
using System.Net.Sockets;

public class WebSocketRobot : MonoBehaviour {

	public GameObject tool;
	public GameObject robot;
	public Vector3 tile_picked_position;
	public float delay;

	private Keyframe[] tX;
	private Keyframe[] tY;
	private Keyframe[] tZ;
	private Keyframe[] qX;
	private Keyframe[] qY;
	private Keyframe[] qZ;
	private Keyframe[] qW;


	private Animation anim;


	private AnimationCurve curve_tX;
	private AnimationCurve curve_tY;
	private AnimationCurve curve_tZ;

	private AnimationCurve curve_qX;
	private AnimationCurve curve_qY;
	private AnimationCurve curve_qZ;
	private AnimationCurve curve_qW;


	private Keyframe[] A1;
	private Keyframe[] A2;
	private Keyframe[] A3;
	private Keyframe[] A4;
	private Keyframe[] A5;
	private Keyframe[] A6;

	private Animation anim_A1;
	private Animation anim_A2;
	private Animation anim_A3;
	private Animation anim_A4;
	private Animation anim_A5;
	private Animation anim_A6;

	private AnimationCurve curve_A1;
	private AnimationCurve curve_A2;
	private AnimationCurve curve_A3;
	private AnimationCurve curve_A4;
	private AnimationCurve curve_A5;
	private AnimationCurve curve_A6;

	private Vector3 robot_position;
	private Vector3 eulerConversion;

	IEnumerator Start () {

		// Connect to Ros (websocket) server
		WebSocket w = new WebSocket(new Uri("ws://10.1.10.14:9012/"));
		yield return StartCoroutine(w.Connect());

		// Listen for ROS data on websocket
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{

				//Debug.Log ("Received: "+reply);
				TrajectoryClass trajectory = JsonUtility.FromJson<TrajectoryClass>(reply);
				if (trajectory.points.Count != 0) {
					
					AnimationClip clip = new AnimationClip ();
					anim = tool.gameObject.GetComponent<Animation> ();
					tX = new Keyframe[trajectory.points.Count];
					tY = new Keyframe[trajectory.points.Count];
					tZ = new Keyframe[trajectory.points.Count];
					qX = new Keyframe[trajectory.points.Count];
					qY = new Keyframe[trajectory.points.Count];
					qZ = new Keyframe[trajectory.points.Count];
					qW = new Keyframe[trajectory.points.Count];

					GameObject A1go = GameObject.Find ("agilus_A1_GEO");
					AnimationClip clip_A1 = new AnimationClip ();
					anim_A1 = A1go.gameObject.GetComponent<Animation> ();

					GameObject A2go = GameObject.Find ("agilus_A2_GEO");
					AnimationClip clip_A2 = new AnimationClip ();
					anim_A2 = A2go.gameObject.GetComponent<Animation> ();

					GameObject A3go = GameObject.Find ("agilus_A3_GEO");
					AnimationClip clip_A3 = new AnimationClip ();
					anim_A3 = A3go.gameObject.GetComponent<Animation> ();

					GameObject A4go = GameObject.Find ("agilus_A4_GEO");
					AnimationClip clip_A4 = new AnimationClip ();
					anim_A4 = A4go.gameObject.GetComponent<Animation> ();

					GameObject A5go = GameObject.Find ("agilus_A5_GEO");
					AnimationClip clip_A5 = new AnimationClip ();
					anim_A5 = A5go.gameObject.GetComponent<Animation> ();

					GameObject A6go = GameObject.Find ("agilus_A6_GEO");
					AnimationClip clip_A6 = new AnimationClip ();
					anim_A6 = A6go.gameObject.GetComponent<Animation> ();


					A1 = new Keyframe[trajectory.points.Count];
					A2 = new Keyframe[trajectory.points.Count];
					A3 = new Keyframe[trajectory.points.Count];
					A4 = new Keyframe[trajectory.points.Count];
					A5 = new Keyframe[trajectory.points.Count];
					A6 = new Keyframe[trajectory.points.Count];

					if (trajectory.table_t_robot.position.x.ToString () != "") {

						robot_position = new Vector3 (trajectory.table_t_robot.position.x, trajectory.table_t_robot.position.y, trajectory.table_t_robot.position.z); 
						robot.transform.localPosition = robot_position;
						eulerConversion = new Vector3 (0, 0, 90f);
						robot.transform.localEulerAngles = eulerConversion;
					}



					for (int i = 0; i < trajectory.points.Count; i++) {
						double double_time = trajectory.points [i].time_from_start.secs + (trajectory.points [i].time_from_start.nsecs * Math.Pow (10, -9)); 
						float time = (float)double_time;
						tX [i] = new Keyframe (time, trajectory.points [i].pose.position.x);
						tY [i] = new Keyframe (time, trajectory.points [i].pose.position.y);
						tZ [i] = new Keyframe (time, trajectory.points [i].pose.position.z);
						qX [i] = new Keyframe (time, trajectory.points [i].pose.orientation.x);
						qY [i] = new Keyframe (time, trajectory.points [i].pose.orientation.y);
						qZ [i] = new Keyframe (time, trajectory.points [i].pose.orientation.z);
						qW [i] = new Keyframe (time, trajectory.points [i].pose.orientation.w);

						float degA1 = trajectory.points [i].positions [0] * Mathf.Rad2Deg;
						float degA2 = -trajectory.points [i].positions [1] * Mathf.Rad2Deg;
						float degA3 = -trajectory.points [i].positions [2] * Mathf.Rad2Deg;
						float degA4 = -trajectory.points [i].positions [3] * Mathf.Rad2Deg;
						float degA5 = -trajectory.points [i].positions [4] * Mathf.Rad2Deg;
						float degA6 = -trajectory.points [i].positions [5] * Mathf.Rad2Deg;

						A1 [i] = new Keyframe (time, degA1);
						A2 [i] = new Keyframe (time, degA2);
						A3 [i] = new Keyframe (time, degA3);
						A4 [i] = new Keyframe (time, degA4);
						A5 [i] = new Keyframe (time, degA5);
						A6 [i] = new Keyframe (time, degA6);


					}

					curve_tX = new AnimationCurve (tX);
					curve_tY = new AnimationCurve (tY);
					curve_tZ = new AnimationCurve (tZ);
					curve_qX = new AnimationCurve (qX);
					curve_qY = new AnimationCurve (qY);
					curve_qZ = new AnimationCurve (qZ);
					curve_qW = new AnimationCurve (qW);

					curve_A1 = new AnimationCurve (A1);
					curve_A2 = new AnimationCurve (A2);
					curve_A3 = new AnimationCurve (A3);
					curve_A4 = new AnimationCurve (A4);
					curve_A5 = new AnimationCurve (A5);
					curve_A6 = new AnimationCurve (A6);

					int key_length = curve_tX.length;
					for (int k = 0; k < key_length; k++) {
						curve_tX.SmoothTangents (k, 0);
						curve_tY.SmoothTangents (k, 0);
						curve_tZ.SmoothTangents (k, 0);
						curve_qX.SmoothTangents (k, 0);
						curve_qY.SmoothTangents (k, 0);
						curve_qZ.SmoothTangents (k, 0);
						curve_qW.SmoothTangents (k, 0);

						curve_A1.SmoothTangents (k, 0);
						curve_A2.SmoothTangents (k, 0);
						curve_A3.SmoothTangents (k, 0);
						curve_A4.SmoothTangents (k, 0);
						curve_A5.SmoothTangents (k, 0);
						curve_A6.SmoothTangents (k, 0);
					}


					clip.legacy = true;
					clip.SetCurve ("", typeof(Transform), "localPosition.x", curve_tX);
					clip.SetCurve ("", typeof(Transform), "localPosition.y", curve_tY);
					clip.SetCurve ("", typeof(Transform), "localPosition.z", curve_tZ);
					clip.SetCurve ("", typeof(Transform), "rotation.x", curve_qX);
					clip.SetCurve ("", typeof(Transform), "rotation.y", curve_qY);
					clip.SetCurve ("", typeof(Transform), "rotation.z", curve_qZ);
					clip.SetCurve ("", typeof(Transform), "rotation.w", curve_qW);

					clip_A1.legacy = true;
					clip_A1.SetCurve ("", typeof(Transform), "localEulerAngles.z", curve_A1);

					clip_A2.legacy = true;
					clip_A2.SetCurve ("", typeof(Transform), "localEulerAngles.y", curve_A2);

					clip_A3.legacy = true;
					clip_A3.SetCurve ("", typeof(Transform), "localEulerAngles.y", curve_A3);

					clip_A4.legacy = true;
					clip_A4.SetCurve ("", typeof(Transform), "localEulerAngles.x", curve_A4);

					clip_A5.legacy = true;
					clip_A5.SetCurve ("", typeof(Transform), "localEulerAngles.y", curve_A5);

					clip_A6.legacy = true;
					clip_A6.SetCurve ("", typeof(Transform), "localEulerAngles.x", curve_A6);


					AnimationClip traj_clip = anim.GetClip ("trajectory");
					if (traj_clip != null) {
						anim.RemoveClip (traj_clip);
					}

					anim.AddClip (clip, "trajectory");
					anim.playAutomatically = false;
					//anim.Play ("trajectory");

					AnimationClip robot_clip_A1 = anim_A1.GetClip ("robot1");
					if (robot_clip_A1 != null) {
						anim_A1.RemoveClip (robot_clip_A1);
					}

					anim_A1.AddClip (clip_A1, "robot1");
					anim_A1.playAutomatically = false;
					//anim_A1.Play ("robot1");

					AnimationClip robot_clip_A2 = anim_A2.GetClip ("robot2");
					if (robot_clip_A2 != null) {
						anim_A2.RemoveClip (robot_clip_A2);
					}

					anim_A2.AddClip (clip_A2, "robot2");
					anim_A2.playAutomatically = false;
					//anim_A2.Play ("robot2");

					AnimationClip robot_clip_A3 = anim_A3.GetClip ("robot3");
					if (robot_clip_A3 != null) {
						anim_A3.RemoveClip (robot_clip_A3);
					}

					anim_A3.AddClip (clip_A3, "robot3");
					anim_A3.playAutomatically = false;
					//anim_A3.Play ("robot3");

					AnimationClip robot_clip_A4 = anim_A4.GetClip ("robot4");
					if (robot_clip_A4 != null) {
						anim_A4.RemoveClip (robot_clip_A4);
					}

					anim_A4.AddClip (clip_A4, "robot4");
					anim_A4.playAutomatically = false;
					//anim_A4.Play ("robot4");

					AnimationClip robot_clip_A5 = anim_A5.GetClip ("robot5");
					if (robot_clip_A5 != null) {
						anim_A5.RemoveClip (robot_clip_A5);
					}

					anim_A5.AddClip (clip_A5, "robot5");
					anim_A5.playAutomatically = false;
					//anim_A5.Play ("robot5");

					AnimationClip robot_clip_A6 = anim_A6.GetClip ("robot6");
					if (robot_clip_A6 != null) {
						anim_A6.RemoveClip (robot_clip_A6);
					}

					anim_A6.AddClip (clip_A6, "robot6");
					anim_A6.playAutomatically = false;
					//anim_A6.Play ("robot6");
					StartCoroutine (PlayAnim(anim, anim_A1, anim_A2, anim_A3, anim_A4, anim_A5, anim_A6, delay));


					/// parent tile that is going to be picked with robot tool

					GameObject tile_picked = GameObject.FindGameObjectWithTag ("confidence_asset_picked");
					if (tile_picked != null) {
						Vector3 tile_picked_position = new Vector3 (tile_picked.transform.position.x, tile_picked.transform.position.y, tile_picked.transform.position.z); 
					}
					GameObject tile_clone = GameObject.FindGameObjectWithTag ("confidence_asset_picked_clone");
					if (tile_clone == null) {
						if (trajectory.object_attached == true) {
							if (tile_picked != null) {
								GameObject tile_picked_moving = GameObject.Instantiate (tile_picked);
								if (tile_picked_moving != null) {
									tile_picked_moving.tag = "confidence_asset_picked_clone";
									Vector3 tool_position = tool.transform.position;
									tile_picked_moving.transform.position = tool_position;
									tile_picked_moving.transform.SetParent (tool.transform);
								}
							}

							// hide picked tile, in this case move out of sight
							if (tile_picked != null) {
								Vector3 bury_picked = new Vector3 (tile_picked.transform.position.x, tile_picked.transform.position.y - 100f, tile_picked.transform.position.z);
								tile_picked.transform.position = bury_picked;
								tile_picked_position = bury_picked;
							}
						}
					}
					if (trajectory.object_attached == false) {
						var assets_picked_clone = GameObject.FindGameObjectsWithTag("confidence_asset_picked_clone");
						foreach(GameObject item_picked in assets_picked_clone)
						{
							Destroy(item_picked);
						}
					}
				}



			}
			if (w.error != null)
			{
				Debug.LogError ("Error: "+w.error);
				break;
			}
			yield return 0;
		}
		w.Close();
	}

	IEnumerator PlayAnim(Animation trajectory, Animation a1, Animation a2, Animation a3, Animation a4, Animation a5, Animation a6, float time)
	{
		float rangetime = GameObject.Find ("Slider").GetComponent <Slider>().value;
		//float rangetime = (sliderValue * .1f) * 5;
		//float rangetime = 0.15f;

		yield return new WaitForSeconds(rangetime);
		trajectory.Play ("trajectory");
		a1.Play ("robot1");
		a2.Play ("robot2");
		a3.Play ("robot3");
		a4.Play ("robot4");
		a5.Play ("robot5");
		a6.Play ("robot6");
		yield break;
	}

}

