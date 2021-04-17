using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNumberPanelFor : SelectNumberPanel
{
    private const string AUDIO_TAGSNAME = "Sound";

    private bool isCorrectedForPanel=false;

    [SerializeField]
    private int[] trueAnswer;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
        SetAudioSouce(audioSource);
    }

    void Update()
    {
        PutStick();
        AnswerNumInitialize();
        TextReflection();
        Correct();
    }

    private void Correct()  //4数字合っているかの判定
    {
            if (answerNum[0] == trueAnswer[0] && answerNum[1] == trueAnswer[1] &&
                answerNum[2] == trueAnswer[2] && answerNum[3] == trueAnswer[3])
            {
                isCorrectedForPanel = true;
            }
    }

    public bool GetisClearForPanel()
    {
        return isCorrectedForPanel;
    }

}
