using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorfingLevelManager : MonoBehaviour
{

    [SerializeField]
    private Player m_Player1;
    [SerializeField]
    private Player m_Player2;

    public MorfingObject morfingObj;
    public Text UIScorePlayer1;
    public Text UIScorePlayer2;
    public Text UIVictory;
    public Text UIDeafeat;

    void Start()
    {
        UpdateUIScore();
    }

    void Update()
    {

        if (m_Player1.IsReacting())
        {
            if (morfingObj.CheckShape())
            {
                m_Player1.score++;
            }
            else
            {
                m_Player1.score--;
                if (m_Player1.score < 0)
                {
                    m_Player1.score = 0;
                }
            }
        }
        if (m_Player2.IsReacting())
        {
            if (morfingObj.CheckShape())
            {
                m_Player2.score++;
            }
            else
            {
                m_Player2.score--;
                if (m_Player2.score < 0)
                {
                    m_Player2.score = 0;
                }
            }
        }

        UpdateUIScore();
    }

    void UpdateUIScore()
    {
        UIScorePlayer1.text = m_Player1.score + " points";
        UIScorePlayer2.text = m_Player2.score + " points";
    }

    void ShowVictory()
    {

    }

    void ShowDefeat()
    {

    }


}
