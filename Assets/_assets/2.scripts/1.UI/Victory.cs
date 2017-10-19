using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Victory : MonoBehaviour
{

    public Transform VictoryDestination;
    public Transform VictoryImageTie;
    public Transform VictoryImageP1;
    public Transform VictoryImageP2;

    public float SlideDownDuration;
    public float BumpDuration;

	void Start ()
    {
	}

	void Update ()
    {
	}

    public void DoVictory(int player)
    {
        Transform winner = VictoryImageTie;
        if (player == 1)
        {
            winner = VictoryImageP1;
        }
        if (player == 2)
        {
            winner = VictoryImageP2;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(winner.DOMove(VictoryDestination.position, SlideDownDuration));
        seq.Join(winner.DOScale(1.3f, SlideDownDuration).SetEase(Ease.InQuad));
        seq.Append(winner.DOPunchScale(new Vector3(0.8f, 0.8f, 0.8f), BumpDuration, 3, .5f));

        seq.OnComplete(() =>
        {
            StartCoroutine(ChangeLevel());
        });
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(5);
        // load next lvl
    }


}
