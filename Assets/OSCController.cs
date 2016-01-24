using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSCController : MonoBehaviour
{
	private long _lastOscTimeStamp = -1;
	public GameObject parent;
	public GameObject dropObject;


	// Use this for initialization
	void Start ()
	{
		// this line triggers the magic
		OSCHandler.Instance.Init ();

		int testInteger = 54321;
		OSCHandler.Instance.SendMessageToClient ("UnityOSC", "/osc/from/unity", testInteger);
	}
	
	// Update is called once per frame
	void Update ()
	{
		OSCHandler.Instance.UpdateLogs ();

		foreach (KeyValuePair<string, ServerLog> item in OSCHandler.Instance.Servers) {
			for (int i = 0; i < item.Value.packets.Count; i++) {
				if (_lastOscTimeStamp < item.Value.packets [i].TimeStamp) {
					_lastOscTimeStamp = item.Value.packets [i].TimeStamp;

					string address = item.Value.packets [i].Address;
					int message1 = (int)item.Value.packets [i].Data [0];
					string message2 = (string)item.Value.packets [i].Data [1];

					Debug.Log (address + ":(" + message1 + ", " + message2 + ")");
					MakeRandom (30);
				}
			}
		}
	}

	public void MakeRandom (int makeCount)
	{
		GameObject go;
		for (int i = 0; i < makeCount; i++) {
			go = this.dropObject;
			MakeTouchObject (go, new Vector3 (Random.Range (-10.0f, 10.0f), Random.Range (-5.0f, 10.0f), Random.Range (0.0f, 10.0f)), 1);
		}
	}

	public GameObject MakeTouchObject (GameObject gameObject, Vector3 pos, int renderQueue)
	{
		GameObject cube = (GameObject)Instantiate (gameObject);
		cube.name = gameObject.name;

		Transform[] allChildren = cube.GetComponentsInChildren<Transform> ();
		foreach (Transform child in allChildren) {
			child.GetComponent<Renderer>().material.renderQueue += renderQueue;
		}

		Color color = new Color (Random.value, Random.value, Random.value, 1.0f);
		cube.GetComponent<Renderer>().material.color = color;

		cube.transform.position = pos;
		cube.transform.parent = this.parent.transform;

		return cube;
	}
}
