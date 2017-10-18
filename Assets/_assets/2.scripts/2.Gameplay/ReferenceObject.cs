using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceObject : MonoBehaviour
{
    public MorfingObject MorfingObj;

    [HideInInspector]
    public string CurrentShape;

    public Text Indication;
    private Image IndicationSprite;

    private void Awake()
    {
        MorfingObj.SetReference(this);
        IndicationSprite = Indication.GetComponentInChildren<Image>();

    }
    void Start ()
    {
    }


    public void ChangeShape(string shape)
    {
        CurrentShape = shape;
        // change shape to find
        switch (shape)
        {
            case "red":
                Indication.text = "Appuyer quand le cercle passe au rouge";
                IndicationSprite.color = Color.red;
                break;
            case "green":
                Indication.text = "Appuyer quand le cercle passe au vert";
                IndicationSprite.color = Color.green;
                break;
            case "blue":
                Indication.text = "Appuyer quand le cercle passe au bleu";
                IndicationSprite.color = Color.blue;
                break;
            case "yellow":
                Indication.text = "Appuyer quand le cercle passe au jaune";
                IndicationSprite.color = Color.yellow;
                break;
        }

    }

	
}
