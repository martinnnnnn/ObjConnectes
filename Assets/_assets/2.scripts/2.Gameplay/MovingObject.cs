using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    private float m_StartTime;
    private float m_Time
    {
        get
        {
            return Time.time - m_StartTime;
        }
    }

	// Use this for initialization
	void Start () {
        m_StartTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(Time.deltaTime*10, Mathf.Sin(transform.position.x/2)/4, 0));
    }
}
