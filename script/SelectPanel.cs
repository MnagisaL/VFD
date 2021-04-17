using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * コントローラーのスティックを動かして選択させるクラス
 * に継承させる
 */

public class SelectPanel : MonoBehaviour
{
    private int selectNum = 0;  //選択する番号

    private AudioSource audioSource;
    [SerializeField]
    protected AudioClip se;
    [SerializeField]

    protected Image[] image;  //選択すると色が変わる画像
    [SerializeField]
    protected Sprite[] colorSprite;  //枠線の色

    protected void SetAudioSouce(AudioSource audioSource)  
    {
        this.audioSource = audioSource;
    }

    protected AudioSource GetAudioSource {
        get => audioSource;
    }
    
    protected int SelectNum {
        get => selectNum;
        set => selectNum = value;
    }

    protected virtual void PutStick()  //スティックを動かすと選択される
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
        {
            audioSource.PlayOneShot(se, 0.2f);
            selectNum++;
            RangeNum();
            ChangeColor();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            audioSource.PlayOneShot(se, 0.2f);
            selectNum--;
            RangeNum();
            ChangeColor();
        }
    }

    protected virtual void RangeNum()  //選択できる範囲
    {
        if (0 > selectNum)
        {
            selectNum = image.Length - 1;
        }
        if (selectNum > image.Length - 1)
        {
            selectNum = 0;
        }
    }

    protected virtual void ChangeColor()  //選んだ場所の画像の変化
    {
        foreach (Image img in image)
        {
            img.sprite = colorSprite[0];
        }
        image[selectNum].sprite = colorSprite[1];
    }

}
