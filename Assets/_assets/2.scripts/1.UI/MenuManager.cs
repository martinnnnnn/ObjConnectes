using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject InactiveBannerJ1;
    public GameObject InactiveBannerJ2;
    public GameObject ActiveBannerJ1;
    public GameObject ActiveBannerJ2;
    public GameObject ActiveLeftController;
    public GameObject ActiveRightController;
    public GameObject InactiveLeftController;
    public GameObject InactiveRightController;

    public Player Player1;
    public Player Player2;

    private bool m_Player1Reacted;
    private bool m_Player2Reacted;
	
	// Update is called once per frame
	void Update () {
		if(Player1.IsReacting())
        {
            LeftHand.SetActive(false);
            InactiveBannerJ1.SetActive(false);
            ActiveBannerJ1.SetActive(true);
            InactiveLeftController.SetActive(false);
            ActiveLeftController.SetActive(true);
            m_Player1Reacted = true;
        }

        if(Player2.IsReacting())
        {
            RightHand.SetActive(false);
            InactiveBannerJ2.SetActive(false);
            ActiveBannerJ2.SetActive(true);
            InactiveRightController.SetActive(false);
            ActiveRightController.SetActive(true);
            m_Player2Reacted = true;
        }

        if(m_Player1Reacted && m_Player2Reacted)
        {
            SceneManager.LoadScene("lvl2");
        }
    }
    

}
