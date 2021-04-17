using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNumberPanelSix : SelectNumberPanel
{
    [SerializeField]
    private int[] trueAnswer;

    private const string AUDIO_TAGSNAME = "Sound";

    private bool isCorrectedSixPanel=false;

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

    private void Correct()  //6文字合っているかの判定
    {
        {
            if (answerNum[0] == trueAnswer[0] && answerNum[1] == trueAnswer[1] &&
                answerNum[2] == trueAnswer[2] && answerNum[3] == trueAnswer[3] &&
                 answerNum[4] == trueAnswer[4] && answerNum[5] == trueAnswer[5])
            {
                isCorrectedSixPanel = true;
            }
        }
    }

    public bool GetisClearSixPanel()
    {
        return isCorrectedSixPanel;
    }

}
