using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Kvant;

public class OpenBook : MonoBehaviour {

    [SerializeField]
    private BookControl bookCrontrol;
    [SerializeField]
    private bool bookOpen = false;
    [SerializeField]
    private GameObject leafEmitter;
    [SerializeField]
    private GameObject mainButterfly;
    [SerializeField]
    private GameObject group;
    [SerializeField]
    private GameObject group2;
    [SerializeField]



    void Start()
    {
        bookCrontrol = this.GetComponent<BookControl>();
        group.gameObject.SetActive(false);
        group2.gameObject.SetActive(false);
        mainButterfly.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TurnBookCover();
        }
        if (bookOpen)
        {
            mainButterfly.gameObject.SetActive(true);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("obj enter" + other.gameObject.tag);
        if (other.gameObject.tag == "Wand" && !bookOpen)
        {
            TurnBookCover();


        }
    }
  
    public IEnumerator SparayLeaves()
    {
        Debug.Log("start spray");
        Spray sprayScript = leafEmitter.gameObject.GetComponent<Spray>();
        yield return new WaitForSeconds(1.2f);
        leafEmitter.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);
        sprayScript.throttle = 0;
        yield return new WaitForSeconds(4f);
        leafEmitter.SetActive(false);
    }

    public void TurnBookCover()
    {
        bookCrontrol.Open_Book();
        bookOpen = true;
        StartCoroutine(SparayLeaves());
        group.gameObject.SetActive(true);
        group2.gameObject.SetActive(true);
       

    }
}
