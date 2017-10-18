using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private float m_Pace = 1.0f;
    [SerializeField]
    private float m_PaceStepPerLevel = 0.1f;
    [SerializeField]
    private Player m_Player;
    [SerializeField]
    private Spawner m_Spawner;    
	
	void Update () {
        
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if(m_Spawner.IsTargetVisible())
            {
                m_Spawner.ResetSpawn();
                GoToNextLevel();
            }
            else
            {
                GoToPreviousLevel();
            }
        }

	}

    public void NotifyNaturalDeathFromTarget()
    {
        GoToPreviousLevel();
    }

    #region Level Progression
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
        m_Pace += m_PaceStepPerLevel;
        m_Pace = (float) System.Math.Round(m_Pace, 2);
        UpdatePace();
    }

    private void DownPace()
    {
        m_Pace -= m_PaceStepPerLevel;
        m_Pace = (float)System.Math.Round(m_Pace, 2);
        UpdatePace();
    }

    private void UpdatePace()
    {
        Time.timeScale = m_Pace;
    }
    #endregion
}
