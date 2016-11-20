using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManagerBev : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wand")
        SceneManager.LoadScene("blueButterfly", LoadSceneMode.Additive);
    }

}
