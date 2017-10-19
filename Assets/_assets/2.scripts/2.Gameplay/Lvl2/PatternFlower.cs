using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternFlower : Flower {

    [SerializeField]
    private int m_NumberOfPetalsUp = 5;

    new protected void Start()
    {
        base.Start();
    }

    new protected void Update () {
        base.Update();
        
	}

    public void RollPetalLoss()
    {
        List<Petal> PetalsCopy = new List<Petal>();
        foreach(Petal petal in Petals)
        {
            PetalsCopy.Add(petal);
        }
        PetalsCopy.Shuffle();

        while(m_PetalsUpCount > m_NumberOfPetalsUp)
        {
            PetalsCopy[0].Leave();
            PetalsCopy.RemoveAt(0);
        }
    }
}
