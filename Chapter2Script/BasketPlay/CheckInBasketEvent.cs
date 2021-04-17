using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInBasketEvent : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController playerController;

    void Update()
    {
        PlayerStopandNext();
    }

    private void PlayerStopandNext()  //ボタンを押したら次の画面表示
    {
        if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
        {
            playerController.Acceleration = 0;
            this.transform.parent.transform.GetChild(1).gameObject.SetActive(true);  //panelの一つ下の階層を取得 そして表示
            this.gameObject.SetActive(false);
        }
    }

}