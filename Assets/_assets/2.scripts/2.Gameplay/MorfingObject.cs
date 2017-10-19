using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MorfingObject : MonoBehaviour
{

    public float AverageTimeBetweenShapes;
    public float ShapeLifeTime;

    private float timeLeftBeforeShift;
    private bool isShaped;
    private Animator animator;

    public Image IndicationImage;
    public Sprite[] IndicationSprites;

    //private ReferenceObject reference;

    [HideInInspector]
    public bool canClick;
    string[] shapeNames;
    List<int> shapesDone;
    string currentShape;
    string rightShape;

	void Start ()
    {
        shapeNames = new string[]
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
        };
        timeLeftBeforeShift = AverageTimeBetweenShapes;
        shapesDone = new List<int>();
        animator = GetComponent<Animator>();
        currentShape = shapeNames[Random.Range(0, shapeNames.Length)];
        ChangeRightShape();
        //reference.ChangeShape(shapeNames[Random.Range(0,shapeNames.Length)]);
    }

    

    void Update ()
    {

        timeLeftBeforeShift -= Time.deltaTime;
        if (timeLeftBeforeShift <= 0)
        {
            if (isShaped)
            {
                animator.Play(currentShape+"rev");
                timeLeftBeforeShift = AverageTimeBetweenShapes + Random.Range(0.0f, AverageTimeBetweenShapes / 2) * Random.Range(-1, 1);
                isShaped = false;
            }
            else
            {
                currentShape = shapeNames[ChooseNextShape()];
                animator.Play(currentShape);

                timeLeftBeforeShift = ShapeLifeTime;
                isShaped = true;
            }
        }
	}

    private int ChooseNextShape()
    {
        int triggerIndex = Random.Range(0, shapeNames.Length);

        if (shapesDone.Count == shapeNames.Length)
        {
            shapesDone.Clear();
            Debug.Log(shapesDone.Count);
        }
        while (shapesDone.Contains(triggerIndex))
        {
            triggerIndex = Random.Range(0, shapeNames.Length);
        }
        shapesDone.Add(triggerIndex);
        return triggerIndex;
    }

    public bool CheckShape()
    {
        //bool check = reference.CurrentShape == currentShape;
        bool check = rightShape == currentShape;
        if (check)
        {
            string newShape = shapeNames[Random.Range(0, shapeNames.Length)];
            while (rightShape == newShape)
            {
                newShape = shapeNames[Random.Range(0, shapeNames.Length)];
            }
            //reference.ChangeShape(newShape);
            ChangeRightShape();
            shapesDone.Clear();
        }
        return check;
    }


    public void ChangeRightShape()
    {
        rightShape = shapeNames[Random.Range(0, shapeNames.Length)];
        IndicationImage.sprite = IndicationSprites[System.Int32.Parse(rightShape) - 1];
    }

}
