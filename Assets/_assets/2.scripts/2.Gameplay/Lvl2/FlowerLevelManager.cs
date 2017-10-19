using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlowerLevelManager : MonoBehaviour {

    public float LevelDuration;
    public bool P1CanInput = true;
    public bool P2CanInput = true;
    private const float delay = 2f;
    public Animator P1VictoryAnimator;
    public Animator P2VictoryAnimator;
    public Animator SquareVictoryAnimator;
    public bool PauseGame;

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

    public List<int> MatchingPetalsNumbers;
    public Transform FlowerMatchingTransform;

    public List<float> MinTimesBeforeLose;
    public List<float> MaxTimesBeforeLose;

	// Use this for initialization
	void Awake () {
        RefreshMinAndMaxTimes();
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

    public void RefreshMinAndMaxTimes()
    {
        MinTimesBeforeLose = new List<float>();
        MaxTimesBeforeLose = new List<float>();

        MinTimesBeforeLose.Add(0.5f);
        MinTimesBeforeLose.Add(1f);
        MinTimesBeforeLose.Add(0.25f);
        MinTimesBeforeLose.Add(2f);
        MinTimesBeforeLose.Add(1.5f);
        MinTimesBeforeLose.Add(2.25f);
        MinTimesBeforeLose.Shuffle();

        MaxTimesBeforeLose.Add(3);
        MaxTimesBeforeLose.Add(2.5f);
        MaxTimesBeforeLose.Add(3.5f);
        MaxTimesBeforeLose.Add(1);
        MaxTimesBeforeLose.Add(6);
        MaxTimesBeforeLose.Add(5);
        MaxTimesBeforeLose.Shuffle();

    }

    public float GetMinTime()
    {
        float toReturn = MinTimesBeforeLose[0];
        MinTimesBeforeLose.RemoveAt(0);

        return toReturn;
    }

    public float GetMaxTime()
    {
        float toReturn = MaxTimesBeforeLose[0];
        MaxTimesBeforeLose.RemoveAt(0);

        return toReturn;
    }

    public void RefreshMatchingPetalsNumbers()
    {
        MatchingPetalsNumbers = new List<int>();
        MatchingPetalsNumbers.Add(2);
        MatchingPetalsNumbers.Add(3);
        MatchingPetalsNumbers.Add(4);
        MatchingPetalsNumbers.Add(5);
        MatchingPetalsNumbers.Add(6);
        MatchingPetalsNumbers.Add(7);
        MatchingPetalsNumbers.Shuffle();
    }

    public int GetMatchingPetalsNumber()
    {
        int toReturn = MatchingPetalsNumbers[0];
        MatchingPetalsNumbers.RemoveAt(0);

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
            RefreshMinAndMaxTimes();
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
        //Player playerReacting = PlayerReacting();
        if(!PauseGame)
        {
            if (Player1.IsReacting() && P1CanInput)
            {
                StartCoroutine(InputDelay(Player1));
                if (IsPatternOnScreen)
                {
                    Player1.score++;
                    
                    m_Player1ok1.transform.position = FlowerMatchingTransform.position;
                    m_Player1ok1.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player1ok2.transform.position = FlowerMatchingTransform.position;
                    m_Player1ok2.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player1ok1.Play();
                    m_Player1ok2.Play();
                
                    Reset = true;
                    
                }
                else
                {
                    m_Player1FailFx.Play();
                }
            }
            else if(Player2.IsReacting() && P2CanInput)
            {
                StartCoroutine(InputDelay(Player2));
                if(IsPatternOnScreen)
                {
                    Player2.score++;
                    m_Player2ok1.transform.position = FlowerMatchingTransform.position;
                    m_Player2ok1.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player2ok2.transform.position = FlowerMatchingTransform.position;
                    m_Player2ok2.transform.rotation = FlowerMatchingTransform.rotation;
                    m_Player2ok1.Play();
                    m_Player2ok2.Play();
                    Reset = true;
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
        P1CanInput = false;
        PauseGame = true;
        if (Player1.score > Player2.score)
        {
            P1VictoryAnimator.Play("victory");           
        }
        else if (Player1.score < Player2.score)
        {
            P2VictoryAnimator.Play("victory");

        }
        else
        {
            SquareVictoryAnimator.Play("victory");

        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator InputDelay(Player playerReacting)
    {
        if(playerReacting == Player1)
        {
            P1CanInput = false;
        }
        else
        {
            P2CanInput = false;
        }
        yield return new WaitForSeconds(delay);
        if(playerReacting == Player1)
        {
            P1CanInput = true;
        }
        else
        {
            P2CanInput = true;
        }
    }
}
