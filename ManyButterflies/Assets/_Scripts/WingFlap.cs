using UnityEngine;
using System.Collections;

public class WingFlap : MonoBehaviour {
	public GameObject body;
	private Butterfly butterflyBody;
	private float freq;
	private float amp;
	private float flipMultiplier;

	public bool flip;
	// Use this for initialization
	void Start () {
		butterflyBody = body.GetComponent<Butterfly> ();
		freq = butterflyBody.flapFreq;
		amp = butterflyBody.flapAmplitude;
		flipMultiplier = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		freq = butterflyBody.flapFreq;
		amp = butterflyBody.flapAmplitude;
		if (flip) {
			flipMultiplier = -1f;
		}
		transform.rotation =  Quaternion.Euler (new Vector3(0,0,Mathf.Sin(Time.fixedTime*freq)*amp*flipMultiplier));
	}
}
