using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerLevelManager : MonoBehaviour {
    public float LevelDuration;
    public bool canInput;
    private const float delay = 2f;

    public PatternFlower FlowerToMatch;

    [HideInInspector]
    public bool IsPatternOnScreen;
    [HideInInspector]
    public List<int> PetalsDownIndexes;
    [HideInInspector]
    public List<Petal> PetalsDown;

    public Player Player1;
    public Player Player2;
    [SerializeField]
    private ParticleSystem m_Player1FailFx;
    [SerializeField]
    private ParticleSystem m_Player2FailFx;
    [SerializeField]
    private ParticleSystem m_Player1ok1;
    [SerializeField]
    private ParticleSystem m_Player1ok2;
    [SerializeField]
    private ParticleSystem m_Player2ok1;
    [SerializeField]
    private ParticleSystem m_Player2ok2;

    public bool Reset;
    public bool ShouldResetLevel;

    public List<int> m_MatchingPetalsNumbers;
    public Transform FlowerMatchingTransform;

	// Use this for initialization
	void Awake () {
        StartCoroutine(EndGame());
        FlowerToMatch.RollPetalLoss();
        RefreshPetalsDownLists();
        RefreshMatchingPetalsNumbers();
    }
	
    private void RefreshPetalsDownLists()
    {
        PetalsDownIndexes = new List<int>();
        PetalsDown = new List<Petal>();
        for (int i = 0; i < FlowerToMatch.Petals.Count; i++)
        {
            if (!FlowerToMatch.Petals[i].IsUp)
            {
                PetalsDownIndexes.Add(i);
                PetalsDown.Add(FlowerToMatch.Petals[i]);
            }
        }
    }

    public void RefreshMatchingPetalsNumbers()
    {
        m_MatchingPetalsNumbers = new List<int>();
        m_MatchingPetalsNumbers.Add(2);
        m_MatchingPetalsNumbers.Add(3);
        m_MatchingPetalsNumbers.Add(4);
        m_MatchingPetalsNumbers.Add(5);
        m_MatchingPetalsNumbers.Add(6);
        m_MatchingPetalsNumbers.Add(7);
        m_MatchingPetalsNumbers.Shuffle();
    }

    public int GetMatchingPetalsNumber()
    {
        int toReturn = m_MatchingPetalsNumbers[0];
        m_MatchingPetalsNumbers.RemoveAt(0);

        return toReturn;
    }

	// Update is called once per frame
	void Update () {
        ManagePlayersScore();
        if(ShouldResetLevel)
        {
            ShouldResetLevel = false;
        }
        if(Reset)
        {
            RefreshMatchingPetalsNumbers();
            FlowerToMatch.GrowPetalsBack();
            FlowerToMatch.RollPetalLoss();
            
            RefreshPetalsDownLists();
            Reset = false;
            ShouldResetLevel = true;
            IsPatternOnScreen = false;
        }
        
	}

    private void ManagePlayersScore()
    {
        Player playerReacting = PlayerReacting();
        if (playerReacting)
        {
            if (IsPatternOnScreen)
            {
                playerReacting.score++;
                if (playerReacting == Player1)
                {
                    m_Player1ok1.transform.position = FlowerMatchingTransform.position;
                    m_Player1ok1.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player1ok2.transform.position = FlowerMatchingTransform.position;
                    m_Player1ok2.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player1ok1.Play();
                    m_Player1ok2.Play();
                }
                else
                {
                    m_Player2ok1.transform.position = FlowerMatchingTransform.position;
                    m_Player2ok1.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player2ok2.transform.position = FlowerMatchingTransform.position;
                    m_Player2ok2.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player2ok1.Play();
                    m_Player2ok2.Play();
                }
                Reset = true;
            }
            else
            {
                if(playerReacting == Player1)
                {
                    m_Player1FailFx.Play();
                }
                else
                {
                    m_Player2FailFx.Play();
                }
            }
        }
    }

    private Player PlayerReacting()
    {
        if(Player1.IsReacting())
        {
            return Player1;
        }
        else if(Player2.IsReacting())
        {
            return Player2;
        }
        return null;
    }

    private void ResetLevel()
    {

    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(LevelDuration);
        canInput = false;
        if (Player1.score > Player2.score)
        {
            VictoryScreen.DoVictory(1);
        }
        else if (Player1.score < Player2.score)
        {
            VictoryScreen.DoVictory(2);
        }
        else
        {
            VictoryScreen.DoVictory(-1);
        }
    }

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(delay);
        canInput = true;
    }
}
