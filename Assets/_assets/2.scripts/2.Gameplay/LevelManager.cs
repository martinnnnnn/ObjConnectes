using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private float m_Pace = 1.0f;
    [SerializeField]
    private float m_PaceStepPerLevel = 0.1f;
    [SerializeField]
    private Player m_Player1;
    [SerializeField]
    private Player m_Player2;
    [SerializeField]
    private Spawner m_Spawner;    
	
	void Update () {
        
		if(m_Player1.IsReacting())
        {
            if(m_Spawner.IsTargetVisible())
            {
                m_Player1.score++;
                m_Spawner.ResetSpawn();
                GoToNextLevel();
            }
            else
            {
                m_Player1.score--;
                GoToPreviousLevel();
            }
        }
        if(m_Player2.IsReacting())
        {
            if (m_Spawner.IsTargetVisible())
            {
                m_Player2.score++;
                m_Spawner.ResetSpawn();
                GoToNextLevel();
            }
            else
            {
                m_Player2.score--;
                GoToPreviousLevel();
            }
        }
	}

    public void TargetDisappearedBeforeAnyReaction()
    {
        GoToPreviousLevel();
    }

    #region Level Progression
    private void GoToPreviousLevel()
    {
        DownPace();
    }

    private void GoToNextLevel()
    {
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
