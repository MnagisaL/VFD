using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectNumberPanel : SelectPanelAddUPDown
{
    [SerializeField]
    protected Text[] answerText;  //回答のテキスト

    protected AudioSource audioSource;

    [SerializeField]
    protected OVRPlayerController playerController;

    private const int MAXNUM = 9;  //選択できる数字の最大値
    private const int MINNUM = 0;  //選択できる数字の最小値

    protected int[] answerNum;
    private bool isInitializedOnce = false;

    protected void AnswerNumInitialize()  //Answerimageに合わせてnumを初期化
    {
        if (isInitializedOnce) return;
            answerNum = new int[image.Length];
            for (int i = 0; i < answerNum.Length; i++)
            {
                answerNum[i] = 0;
            }
            isInitializedOnce = true;
    }

    protected void TextReflection()  //条件が成立したらtextline++ (n=0,1,2,3・・・)
    {
        StartCoroutine(ButtonAPush());
        for (int i = 0; i < image.Length; i++)
        {
            answerText[i].text = answerNum[i].ToString();
        }
    }

    IEnumerator ButtonAPush()  //Aボタンを押したときに数字を変化させる
    {
        if (IsCheckUp && OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            audioSource.PlayOneShot(se, 0.2f);
            yield return new WaitForSeconds(0.1f);
            answerNum[SelectNum]++;
            if (answerNum[SelectNum] > MAXNUM)
                answerNum[SelectNum] = MINNUM;
            if (answerNum[SelectNum] < MINNUM)
                answerNum[SelectNum] = MAXNUM;
        }
        if (!IsCheckUp && OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            audioSource.PlayOneShot(se, 0.2f);
            yield return new WaitForSeconds(0.1f);
            playerController.Acceleration = 0.15f;
            this.transform.parent.gameObject.SetActive(false);
        }
    }

}
