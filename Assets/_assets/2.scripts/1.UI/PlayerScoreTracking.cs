using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreTracking : MonoBehaviour {

    public Player Player;
    private Text m_Text;

    // Use this for initialization
    void Start()
    {
        m_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = Player.score + " points";
    }
}
