using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    [SerializeField]
    public List<Petal> Petals;
    
    public int m_PetalsUpCount
    {
        get
        {
            return CountPetalsUp();
        }
    }

    public bool CompareWithFlower(Flower toCompare)
    {
        for(int i = 0; i < Petals.Count; i++)
        {
            if(toCompare.Petals[i].IsUp != Petals[i].IsUp)
            {
                return false;
            }
        }
        return true;
    }

    private int CountPetalsUp()
    {
        int petalsUpCount = 0;
        foreach(Petal petal in Petals)
        {
            if(petal.IsUp)
            {
                petalsUpCount++;
            }
        }

        return petalsUpCount;
    }

}
