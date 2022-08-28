using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject ground1;
    public GameObject ground2;
    public Button tapToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fall()
    {
        ground1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ground2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        tapToPlay.gameObject.SetActive(false);
    }
}
