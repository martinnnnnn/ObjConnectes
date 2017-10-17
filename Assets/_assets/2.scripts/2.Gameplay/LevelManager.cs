using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //[SerializeField]
    private float m_Pace = 1.0f;
    [SerializeField]
    private Player m_Player;
    [SerializeField]
    private Spawner m_Spawner;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
		if(Input.GetKey("e") && m_Spawner.IsTargetVisible())
        {
            m_Spawner.ResetSpawn();
            GoToNextLevel();
        }
        if(m_Spawner.TargetDisappeared)
        {
            GoToPreviousLevel();
            m_Spawner.TargetDisappeared = false;
        }

	}

    private void GoToPreviousLevel()
    {
        m_Player.score--;
        DownPace();
    }

    private void GoToNextLevel()
    {
        m_Player.score++;
        UpPace();
    }

    private void UpPace()
    {
        m_Pace += 0.1f;
        m_Pace = (float) System.Math.Round(m_Pace, 2);
        UpdatePace();
    }

    private void DownPace()
    {
        m_Pace -= 0.1f;
        m_Pace = (float)System.Math.Round(m_Pace, 2);
        UpdatePace();
    }

    private void UpdatePace()
    {
        Time.timeScale = m_Pace;
    }
}
