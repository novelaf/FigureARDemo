using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using System.IO;
using System.Net.Sockets;

public class EchoTest : MonoBehaviour {
	// Use this for initialization
	IEnumerator Start () {
		WebSocket w = new WebSocket(new Uri("ws://echo.websocket.org"));
		yield return StartCoroutine(w.Connect());
		w.SendString("Hi there");
		int i=0;
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{
				Debug.Log ("Received: "+reply);
				w.SendString("Hi there"+i++);
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

//	internal bool socketReady = false;
//	TcpClient mySocket;
//	NetworkStream theStream;
//	StreamWriter theWriter;
//	StreamReader theReader;
//	string Host = "ws://echo.websocket.org";
//	int Port = 80;
//
//	// Use this for initialization
//	void Start () {
//		setupSocket ();
//		writeSocket ("bk here");
//		readSocket ();
//
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		//Debug.Log (socketReady);
//
//	}
//
//	public void setupSocket() {
//		try {
//			mySocket = new TcpClient(Host, Port);
//			theStream = mySocket.GetStream();
//			theWriter = new StreamWriter(theStream);
//			theReader = new StreamReader(theStream);
//			socketReady = true;
//			Debug.Log ("socket is ready.");
//		}catch (Exception e) {
//			Debug.Log("Socket error: " + e);
//		}
//	}
//
//	public void writeSocket(string theLine) {
//		Debug.Log ("writeSocket");
//		if (!socketReady) {
//			Debug.Log ("socket not ready to write to");
//			return;
//		}
//		String foo = theLine + "\r\n";
//		theWriter.Write(foo);
//		theWriter.Flush();
//	}
//
//	public String readSocket() {
//		if (!socketReady) {
//			Debug.Log ("socket not ready to read from");
//		}
//		if (theStream.DataAvailable) {
//			Debug.Log ("reading from socket.");
//			Debug.Log (theReader.ReadLine ());
//		} else {
//			Debug.Log ("no data to read");
//		}
//		return "";
//	}
//
//	public void closeSocket() {
//		if (!socketReady)
//			return;
//		theWriter.Close();
//		theReader.Close();
//		mySocket.Close();
//		socketReady = false;
//	}
}
