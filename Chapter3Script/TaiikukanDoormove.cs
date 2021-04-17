using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 体育館にあるパネルのついたドアが正解したらドアをスライド
 */

public class TaiikukanDoormove : MonoBehaviour
{
    [SerializeField]
    private SelectNumberPanelFor selectNumberPanelFor;

    [SerializeField]
    private float doorMoveLengthX = 0.979f;

    [SerializeField]
    private float doorMoveTime = 1.5f;

    private bool isDoorMovedOnce = false;

    [SerializeField]
    protected OVRPlayerController playerController;

    void Update()
    {
        DoorMove();
    }

    private void DoorMove()
    {
        if (selectNumberPanelFor.GetisClearForPanel() && !isDoorMovedOnce)
        {
            ClearForPanelAnswer();
            this.transform.DOLocalMoveX(doorMoveLengthX, doorMoveTime).SetRelative();
            isDoorMovedOnce = true;
        }
    }

    private void ClearForPanelAnswer()  //4つの文字があっていた時の動作
    {
        playerController.Acceleration = 0.15f;
        if (selectNumberPanelFor.transform.root.transform.GetChild(1).gameObject != null)
        {
            selectNumberPanelFor.transform.root.transform.GetChild(1).gameObject.SetActive(true);
        }
        selectNumberPanelFor.transform.root.transform.GetChild(2).gameObject.SetActive(false);//青いポインタを消す
        selectNumberPanelFor.gameObject.SetActive(false);
    }

}
