using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		foreach (Transform t in col.gameObject.transform) {
			Destroy (t.gameObject.GetComponent<Renderer>().material);
			Destroy (t.gameObject);
		}

		Destroy (col.gameObject.GetComponent<Renderer>().material);
		Destroy (col.gameObject);
	}
}
