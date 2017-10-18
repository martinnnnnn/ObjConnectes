using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int score;
    [SerializeField]
    private KeyCode m_ValidationKey;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsReacting()
    {
        return Input.GetKeyDown(m_ValidationKey);
    }
}
