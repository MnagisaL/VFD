using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class NameAnswerPanel2 : SelectPanel
{
    [SerializeField]
    private OVRPlayerController playerController;

    [SerializeField]
    private Text text;

    [SerializeField]
    private string talk;

    private AudioSource audioSource;
    private const string AUDIO_TAGSNAME = "Sound";

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
        SetAudioSouce(audioSource);
    }

    void Update()
    {
        TextUp();
        PutStick();
        DisActiveCanvas();
    }

    private bool isDotweenOnce = false;

    private void TextUp()  //テキストを流す
    {
        if (isDotweenOnce) return;
        text.DOText(talk, talk.Length * 0.1f);
        isDotweenOnce = true;
    }   
    private void DisActiveCanvas()
    {
        if (SelectNum == 0)  //Noの処理
        {
            if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
            {
                audioSource.PlayOneShot(se);
                this.transform.parent.transform.GetChild(2).gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        if (SelectNum == image.Length - 1)  //Yesの処理
        {
            if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
            {
                audioSource.PlayOneShot(se);
                playerController.Acceleration = 0.15f;
                this.transform.parent.transform.GetChild(0).gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

}
