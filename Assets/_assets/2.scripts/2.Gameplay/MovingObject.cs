using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : Target {

    [SerializeField]
    private System.Action movement;
    

	// Use this for initialization
	new void Start () {
        base.Start();
        m_StartTime = Time.time;
        movement = SinusoidaleMovement;
        transform.localScale = new Vector3(0, 0, 7);
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        movement();
        ScaleOverLifetime();
    }

    private void SinusoidaleMovement()
    {
        transform.Translate(new Vector3(0.2f, Mathf.Sin(transform.position.x / 2) / 4, 0));
    }

    private void OtherMovement()
    {

    }

    private void ScaleOverLifetime()
    {
        float sizeOverLifetime = Mathf.Clamp((-Mathf.Pow(m_Time - Lifetime/2, 2) + 1) * 8, 0, 7);
        Vector3 newScale = new Vector3(sizeOverLifetime, sizeOverLifetime, 7);
        transform.localScale = newScale;
    }
    
}
