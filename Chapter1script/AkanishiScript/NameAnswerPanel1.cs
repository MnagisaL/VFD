using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameAnswerPanel1 : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController playerController;

    void Update()
    {
        StopandNext();
    }

    private void StopandNext()  //プレイヤーの動きを止め次のパネル表示
    {
        if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
        {
            playerController.Acceleration = 0;
            this.transform.parent.transform.GetChild(1).gameObject.SetActive(true);//panelの一つ下の階層を取得 そして表示
            this.gameObject.SetActive(false);
        }
    }

}
