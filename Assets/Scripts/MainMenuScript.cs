using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject ground1;
    public GameObject ground2;

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
    }
}
