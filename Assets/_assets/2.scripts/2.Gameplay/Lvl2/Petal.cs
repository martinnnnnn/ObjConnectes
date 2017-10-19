using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petal : MonoBehaviour {

    public bool IsUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Leave()
    {
        gameObject.SetActive(false);
        IsUp = false;
    }

    public void Grow()
    {
        gameObject.SetActive(true);
        IsUp = true;
    }
}
