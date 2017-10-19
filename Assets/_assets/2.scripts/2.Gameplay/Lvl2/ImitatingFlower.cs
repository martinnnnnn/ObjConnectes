using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitatingFlower : Flower {
    [SerializeField]
    private int NumberOfPetalsMatchingPattern;
    private int m_PetalsToDownMatchingPatternCount;
    private List<Petal> m_PetalsDownMatchingPattern;
    private List<int> m_PetalsDownMatchingPatternIndexes;
    private FlowerLevelManager m_LevelManager;
    private bool m_CanLeaveOtherPetals;

    private float m_LoseTimer;

    // Use this for initialization
    void Start () {
        m_LevelManager = GameObject.Find("LevelManager").GetComponent<FlowerLevelManager>();
        m_PetalsDownMatchingPattern = new List<Petal>();
        m_PetalsToDownMatchingPatternCount = NumberOfPetalsMatchingPattern;
        m_PetalsDownMatchingPatternIndexes = new List<int>();
        foreach (int petalIndex in m_LevelManager.PetalsDownIndexes)
        {
            m_PetalsDownMatchingPatternIndexes.Add(petalIndex);
        }
        m_PetalsDownMatchingPatternIndexes.Shuffle();

        int i = m_PetalsDownMatchingPatternIndexes.Count - 1;
        while (m_PetalsDownMatchingPatternIndexes.Count > NumberOfPetalsMatchingPattern)
        {
            m_PetalsDownMatchingPatternIndexes.RemoveAt(i);
            i--;
        }
        for (int j = 0; j < Petals.Count; j++)
        {
            if (m_PetalsToDownMatchingPatternCount > 0)
            {
                if (m_PetalsDownMatchingPatternIndexes.Contains(j))
                {
                    m_PetalsDownMatchingPattern.Add(Petals[j]);
                    m_PetalsToDownMatchingPatternCount--;
                }
            }
        }
        m_PetalsDownMatchingPattern.Shuffle();
        m_PetalsToDownMatchingPatternCount = NumberOfPetalsMatchingPattern;

        m_LoseTimer = Random.Range(0.5f, 3);
    }
	
	// Update is called once per frame
	void Update () {
        m_LoseTimer -= Time.deltaTime;
        if (m_LoseTimer < 0)
        {
            if (m_PetalsDownMatchingPattern.Count > 0)
            {
                Petal leavingPetal = m_PetalsDownMatchingPattern[0];
                leavingPetal.Leave();
                m_PetalsDownMatchingPattern.Remove(leavingPetal);
            }
            else
            {
                m_CanLeaveOtherPetals = true;
            }

            if (m_CanLeaveOtherPetals)
            {
                if (m_PetalsUpCount > m_LevelManager.FlowerToMatch.m_PetalsUpCount)
                {
                    for (int j = 0; j < Petals.Count; j++)
                    {
                        if (!m_LevelManager.PetalsDownIndexes.Contains(j) && Petals[j].IsUp)
                        {
                            Petals[j].Leave();
                            break;
                        }
                    }
                }
            }
            m_LoseTimer = Random.Range(0.5f, 3);
        }

        if (CompareWithFlower(m_LevelManager.FlowerToMatch))
        {
            m_LevelManager.IsPatternOnScreen = true;
        }

    }
}
