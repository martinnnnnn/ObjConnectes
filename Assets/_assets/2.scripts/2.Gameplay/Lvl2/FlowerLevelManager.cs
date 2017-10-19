using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerLevelManager : MonoBehaviour {

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

	// Use this for initialization
	void Awake () {
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
                    m_Player1ok1.Play();
                    m_Player1ok1.Play();
                }
                else
                {
                    m_Player2FailFx.Play();
                }
                Reset = true;
            }
            else
            {
                playerReacting.score--;
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
}
