using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class Inactive : MonoBehaviour {

    private bool gameStart;
    // Use this for initialization
    void Start () {

        gameStart = false;
    }
	
	// Update is called once per frame
	void Update () {
	
        if(gameStart)
        {
            GameStart();
        }
        else
        {
            GameStop();
        }
	}

    void GameStart()
    {
        gameObject.SetActive(true);
    }

    void GameStop()
    {
        gameObject.SetActive(false);
    }


}
