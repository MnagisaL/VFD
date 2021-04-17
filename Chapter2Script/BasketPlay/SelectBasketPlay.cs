using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Serialization;

public class SelectBasketPlay : SelectPanel
{

    [SerializeField]
    private OVRScreenFade fade;

    [SerializeField]
    private Text text;

    [SerializeField]
    private string talkBefore;  //バスケを始める前のテキスト
    [SerializeField]
    private string talkAfter;  //バスケが終わった後のテキスト

    private AudioSource audioSource;
    private const string AUDIO_TAGSNAME = "Sound";

    [SerializeField]
    private OVRPlayerController playerController;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
        SetAudioSouce(audioSource);
    }

    void Update()
    {
        TextUp();
        PutStick();
        DisactiveCanvas();
    }

    private bool isCheckDotweenOnce = false;

    private void TextUp()
    {
        if (!isCheckDotweenOnce)
        {
            text.DOText(talkBefore, talkBefore.Length * 0.1f);
            isCheckDotweenOnce = true;
        }
    }

    [SerializeField]
    private BasketSound basketSound;
    private bool isCheckBasketSoundOn = false;

    private void DisactiveCanvas()
    {
        if (SelectNum == 0)  //バスケスタート48秒後にフェードイン
        {
            if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
            {
                audioSource.PlayOneShot(se);
                isCheckBasketSoundOn = true;
                fade.FadeOn(0, 1);
                if (isCheckBasketSoundOn)
                {
                    basketSound.MoveandSound();
                    isCheckBasketSoundOn = false;
                }
                StartCoroutine(NextText());
                Invoke("BasketEnd", 48);
            }
        }
        if (SelectNum == 1)  //やめる
        {
            if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
            {
                audioSource.PlayOneShot(se);
                playerController.Acceleration = 0.15f;
                this.transform.parent.GetChild(0).gameObject.SetActive(true);
                this.transform.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator NextText()  //テキストをフェードアウトした後変える
    {
        yield return new WaitForSeconds(4.0f);
        text.text = talkAfter;
        this.gameObject.SetActive(false);
    }

    private void BasketEnd()  //バスケが終了したときcanvasを再表示
    {
        fade.FadeOn(1, 0);
        this.gameObject.SetActive(true);
    }

}
