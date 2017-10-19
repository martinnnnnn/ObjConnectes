﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerLevelManager : MonoBehaviour {

    public PatternFlower FlowerToMatch;

    //[HideInInspector]
    public bool IsPatternOnScreen;
    [HideInInspector]
    public List<int> PetalsDownIndexes;
    [HideInInspector]
    public List<Petal> PetalsDown;

    public Player Player1;
    public Player Player2;

	// Use this for initialization
	void Awake () {
        FlowerToMatch.RollPetalLoss();
        PetalsDownIndexes = new List<int>();
        PetalsDown = new List<Petal>();
        for(int i = 0; i < FlowerToMatch.Petals.Count; i++)
        {
            if(!FlowerToMatch.Petals[i].IsUp)
            {
                PetalsDownIndexes.Add(i);
                PetalsDown.Add(FlowerToMatch.Petals[i]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        ManagePlayersScore();
        
	}

    private void ManagePlayersScore()
    {
        Player playerReacting = PlayerReacting();
        if (playerReacting)
        {
            if (IsPatternOnScreen)
            {
                playerReacting.score++;
            }
            else
            {
                playerReacting.score--;
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
}