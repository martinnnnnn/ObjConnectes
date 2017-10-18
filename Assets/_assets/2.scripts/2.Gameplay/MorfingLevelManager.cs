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
    public Text UIScore;
    public Text UIVictory;
    public Text UIDeafeat;

    void Start()
    {
        UIScore.text =
            "Joueur 1 a " + m_Player1.score + " points\n"
            + "Joueur 2 a " + m_Player2.score + " points\n";
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
                //if (m_Player1.score < 0)
                //{
                //    m_Player1.score = 0;
                //}
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
                //if (m_Player2.score < 0)
                //{
                //    m_Player2.score = 0;
                //}
            }
        }

        UpdateUIScore();
    }

    void UpdateUIScore()
    {
        UIScore.text =
            "Joueur1 : " + m_Player1.score + " points\n"
            + "Joueur2 : " + m_Player2.score + " points\n";
            
    }

    void ShowVictory()
    {

    }

    void ShowDefeat()
    {

    }


}
