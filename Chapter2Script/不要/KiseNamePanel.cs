using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class KiseNamePanel : SelectPanel
{
    [SerializeField]
    private Text[] answerText;  //回答のテキスト

    private string[,] answerName = new string[,]{{ "よ", "こ", "ま", "し", "も","" },
                                                 { "き", "な", "が", "か", "と","" },
                                                 { "ろ", "せ", "と", "は", "う","" },
                                                 { "あ", "ら", "ね", "わ", "せ","" },
                                                 { "こ", "す", "ゆ", "こ", "こ","" }};

    private AudioSource audioSource;
    private const string AUDIO_TAGSNAME = "Sound";

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
        base.SetAudioSouce(audioSource);
    }

    void Update()
    {
        base.PutStick();
        TrueAnswer();
    }
   /* private void PutStick()  //スティックを押したときの処理
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
        {
            selectNum++;
            audioSource.PlayOneShot(se, 0.2f);
            RangeNum();
            ChangeColor();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            selectNum--;
            audioSource.PlayOneShot(se, 0.2f);
            RangeNum();
            ChangeColor();
        }
    }

    private void RangeNum()
    {
        if (0 > selectNum)
        {
            selectNum = answerImage.Length - 1;
        }
        if (selectNum > answerImage.Length - 1)
        {
            selectNum = 0;
        }
    }

    private void ChangeColor()
    {
        foreach (Image img in answerImage)
        {
            img.sprite = colorSprite[0];
        }
        answerImage[selectNum].sprite = colorSprite[1];
    }
   */

    private int textLine = 0;

    private void TrueAnswer()  //正しい答えだったら先に進める
    {
        for (int i = 0; i < image.Length; i++)
        {
            answerText[i].text = answerName[i, textLine];
        }
        TextPlus(textLine, 0, 1);
        TextPlus(textLine, 1, 2);
        TextPlus(textLine, 2, 0);
        TextPlus(textLine, 3, 4);
        TextPlus(textLine, 4, 1);
    }

    private void TextPlus(int textline, int sequence, int numans)  //条件が成立したらtextline++ (n=0,1,2,3・・・)
    {
        if (textline == sequence && base.SelectNum == numans)  //あっていたら次の文字
            StartCoroutine(ButtonTrue());
        if (textline == sequence && base.SelectNum != numans)  //間違っていたらひとつ前のパネルに戻る
            StartCoroutine(DisAnswer());
    }

    private bool isCheckEnd = false;

    IEnumerator ButtonTrue()  //ボタンがすぐ入力されるので少し遅らせる
    {
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            audioSource.PlayOneShot(se, 0.2f);
            if (this.textLine == 4 && base.SelectNum == 1)  isCheckEnd = true;  //終わりの処理
            yield return new WaitForSeconds(0.1f);
            this.textLine++;
        }
    }

    IEnumerator DisAnswer()
    {
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            audioSource.PlayOneShot(se, 0.2f);
            yield return new WaitForSeconds(0.1f);
            this.textLine = 0;
            this.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public bool GetisEnd()
    {
        return isCheckEnd;
    }

}