using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MorfingObject : MonoBehaviour
{

    public float AverageTimeBetweenShapes;
    public float ShapeLifeTime;

    private float timeLeftBeforeShift;
    private bool isShaped;
    private Animator animator;

    private ReferenceObject reference;

    [HideInInspector]
    public bool canClick;
    string[] shapeNames;
    List<int> shapesDone;
    string currentShape;


	void Start ()
    {
        shapeNames = new string[]
        {
            "red",
            "blue",
            "green",
            "yellow"
        };
        currentShape = shapeNames[Random.Range(0, shapeNames.Length)];
        timeLeftBeforeShift = AverageTimeBetweenShapes;
        shapesDone = new List<int>();
        animator = GetComponent<Animator>();
        reference.ChangeShape(shapeNames[Random.Range(0,shapeNames.Length)]);
    }

    

    void Update ()
    {

        timeLeftBeforeShift -= Time.deltaTime;
        if (timeLeftBeforeShift <= 0)
        {
            if (isShaped)
            {
                animator.SetTrigger("idle");

                timeLeftBeforeShift = AverageTimeBetweenShapes + Random.Range(0.0f, AverageTimeBetweenShapes / 2) * Random.Range(-1, 1);
                isShaped = false;
            }
            else
            {
                currentShape = shapeNames[ChooseNextShape()];
                animator.SetTrigger(currentShape);

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
        bool check = reference.CurrentShape == currentShape;
        if (check)
        {
            string newShape = shapeNames[Random.Range(0, shapeNames.Length)];
            while (reference.CurrentShape == newShape)
            {
                newShape = shapeNames[Random.Range(0, shapeNames.Length)];
            }
            reference.ChangeShape(newShape);
            shapesDone.Clear();
        }
        return check;
    }

    public void SetReference(ReferenceObject obj)
    {
        reference = obj;
    }


}
