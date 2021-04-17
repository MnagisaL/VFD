using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * コントローラーのスティックを動かして選択させるクラス
 * に継承させるものの上下がついたバージョン
 */

public class SelectPanelAddUPDown : SelectPanel
{
    private bool isCheckUp = false;

    [SerializeField]
    protected Image backImage;

    protected bool IsCheckUp {
        get => isCheckUp;
    }

    protected override void PutStick()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickUp))  //スティックを上
        {
            isCheckUp = true;
            GetAudioSource.PlayOneShot(base.se, 0.2f);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickDown))  //スティックを下
        {
            isCheckUp = false;
            GetAudioSource.PlayOneShot(base.se, 0.2f);
        }
        if (isCheckUp == true && OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
        {
            base.SelectNum++;
            GetAudioSource.PlayOneShot(base.se, 0.2f);
            RangeNum();
        }
        if (isCheckUp == true && OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            base.SelectNum--;
            GetAudioSource.PlayOneShot(base.se, 0.2f);
            RangeNum();
        }
        ChangeColor();
    }

    protected override void RangeNum()
    {
        if (0 > base.SelectNum)
        {
            base.SelectNum = base.image.Length - 1;
        }
        if (base.SelectNum > base.image.Length - 1)
        {
            base.SelectNum = 0;
        }
    }

    protected override void ChangeColor()
    {
        if (isCheckUp)
        {
            backImage.sprite = base.colorSprite[0];  //上なら下が黒
            foreach (Image img in base.image)
            {
                img.sprite = base.colorSprite[0];
            }
            base.image[base.SelectNum].sprite = base.colorSprite[1];
        }
        if (!isCheckUp)  //下なら、数字をすべて黒くしBackを赤
        {
            foreach (Image img in base.image)
            {
                img.sprite = base.colorSprite[0];
            }
            backImage.sprite = base.colorSprite[1];  //上なら下が黒
        }
    }

}
