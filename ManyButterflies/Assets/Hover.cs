using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if(Time.timeSinceLevelLoad < .5)
        transform.Translate(Vector3.up * Time.deltaTime);
	}
}
