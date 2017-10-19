using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorfingLevelManager : MonoBehaviour
{
    public float LevelDuration;
    public Victory VictoryScreen;

    [SerializeField]
    private Player m_Player1;
    [SerializeField]
    private Player m_Player2;

    public MorfingObject morfingObj;
    public Text UIScorePlayer1;
    public Text UIScorePlayer2;

    private const float delay = 2f;
    public bool canInput;

    public GameObject Player1Victory1;
    public GameObject Player1Victory2;
    public GameObject Player2Victory1;
    public GameObject Player2Victory2;


    void Start()
    {
        StartCoroutine(EndGame());
        UpdateUIScore();
        canInput = true;
    }

    
    void Update()
    {

        if (m_Player1.IsReacting() && canInput)
        {
            if (morfingObj.CheckShape())
            {
                canInput = false;
                StartCoroutine(InputDelay());
                Player1Victory1.SetActive(true);
                Player1Victory2.SetActive(true);
                StartCoroutine(ParticulesDelay());
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
        if (m_Player2.IsReacting() && canInput)
        {
            if (morfingObj.CheckShape())
            {
                canInput = false;
                StartCoroutine(InputDelay());
                m_Player2.score++;
                Player2Victory1.SetActive(true);
                Player2Victory2.SetActive(true);
                StartCoroutine(ParticulesDelay());
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

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(delay);
        canInput = true;
    }

    IEnumerator ParticulesDelay()
    {
        yield return new WaitForSeconds(delay);
        Player1Victory1.SetActive(false);
        Player1Victory2.SetActive(false);
        Player2Victory1.SetActive(false);
        Player2Victory2.SetActive(false);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(LevelDuration);
        canInput = false;
        if (m_Player1.score > m_Player2.score)
        {
            VictoryScreen.DoVictory(1);
        }
        else if (m_Player1.score < m_Player2.score)
        {
            VictoryScreen.DoVictory(2);
        }
        else
        {
            VictoryScreen.DoVictory(-1);
        }
    }
}
