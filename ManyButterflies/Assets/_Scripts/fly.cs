using UnityEngine;
using System.Collections;

public class fly : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        var timer = 0.0;

        timer += Time.realtimeSinceStartup;

        if (timer < 5.0)
        {
           
            transform.Translate(Vector3.forward * Time.deltaTime * 4, Space.World);
            transform.Translate(Vector3.left * Time.deltaTime * 4, Space.World);
            transform.Translate(Vector3.up * Time.deltaTime * 4, Space.World);
        }

        transform.Translate(Random.insideUnitSphere);
        transform.Translate(Vector3.back * Time.deltaTime * 5, Space.World);
    }
}
