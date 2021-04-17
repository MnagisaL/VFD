using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Chapter2_Gamesystem : Gamesystem
{
    private CharacterText chapter2Talk1;
    private CharacterText chapter2Talk2;
    private CharacterText chapter2Talk3;
    private CharacterText chapter2Talk4;

    [SerializeField]
    private GameObject mojiCube;  //文字のキューブを最初非アクティブ

    void Start()
    {
        SetEventOnceNum(7);
        InitializeEnvironment();
        mojiCube.SetActive(false);
    }

    void Update()
    {
        Event1();     //最初の会話が終わった後
        Event2();     //黄瀬との初めての会話が終わった後
        Event3();     //6つの数字を当てた後
        Event4();     //2回目の黄瀬と会話が終わった後
        Event5();     //4つのパネルを当てたあと
        Event6();     //黄瀬の名前が判明した後
        Event7();     //黄瀬との最後の会話が終わった後
    }

    protected override void InitializeTalk()  //会話後に変化を与えるObjectの取得
    {
        chapter2Talk1 = talkObject[0].GetComponentInChildren<CharacterText>();  //talk1赤西1
        chapter2Talk2 = talkObject[1].GetComponentInChildren<CharacterText>();  //talk2黄瀬1
        chapter2Talk3 = talkObject[3].GetComponentInChildren<CharacterText>();  //talk4黄瀬2
        chapter2Talk4 = talkObject[8].GetComponentInChildren<CharacterText>();  //talk4黄瀬2
    }

    private void Event1()  //最初の会話が終わった後
    {
        if (eventOnceList[0]) return;
        if (chapter2Talk1.GetisCheckAllRead())
        {
            playerController.Acceleration = 0.15f;
            FadeandDestroyCanvas(0, 0.6f, 0);
            eventOnceList[0] = true;
        }
    }

    private void Event2()  //黄瀬との初めての会話が終わった後
    {
        if (eventOnceList[1]) return;
        if (chapter2Talk2.GetisCheckAllRead())
        {
            eventOnceList[1] = true;
            talkObject[2].gameObject.SetActive(true);
        }
    }

    [SerializeField]
    private SelectNumberPanelSix selectNumbersPanelSix;
    [SerializeField]
    private GameObject fenceLeft;
    [SerializeField]
    private GameObject fenceRight;
    [SerializeField]
    private AudioSource[] fence_AudioSource;
    [SerializeField]
    private AudioClip fence_Se;

    [SerializeField]
    private float fenceMoveLengthY = 3.93f;

    private void Event3()  //6つの数字を当てた後
    {  //panelをクリアしたらtalkOb1,3交換
        if (eventOnceList[2]) return;
        if (selectNumbersPanelSix.GetisClearSixPanel())
        {
            ClearSixPanelAnswer();
        }
    }

    private void ClearSixPanelAnswer()
    {
        fence_AudioSource[0].PlayOneShot(fence_Se);
        fence_AudioSource[1].PlayOneShot(fence_Se);
        fenceLeft.transform.DOLocalMoveY(fenceMoveLengthY, 3f).SetRelative();
        fenceRight.transform.DOLocalMoveY(fenceMoveLengthY, 3f).SetRelative();
        playerController.Acceleration = 0.15f;
        Invoke("DestroyFence", 3.0f);
        selectNumbersPanelSix.gameObject.transform.parent.gameObject.SetActive(false);
        TalkObjectDisplayShift(talkObject, 1, 3);
        selectNumbersPanelSix.transform.root.transform.GetChild(1).gameObject.SetActive(false);//青いポインタを消す
        eventOnceList[2] = true;
    }

    private void DestroyFence()
    {
        fenceLeft.SetActive(false);
        fenceRight.SetActive(false);
    }

    private void Event4()  //2回目の黄瀬と会話が終わった後
    {
        if (eventOnceList[3]) return;
        if (chapter2Talk3.GetisCheckAllRead())
            StartCoroutine(Event4_FadeandShift());
    }

    IEnumerator Event4_FadeandShift()
    {
        eventOnceList[3] = true;
        FadeandDestroyCanvas(3);
        yield return new WaitForSeconds(1.5f);
        TalkObjectDisplayShift(talkObject, 3, 4);
        TalkObjectDisplayShift(talkObject, 0, 5);
        talkObject[6].SetActive(true);
        fade.FadeOn(1, 0);
    }

    [SerializeField]
    private SelectNumberPanelFor selectNumberPanelFor;
    [SerializeField]
    private GameObject katanaObject;

    private void Event5()  //4つのパネルを当てたあと
    {
        if (eventOnceList[4]) return;
        if (selectNumberPanelFor.GetisClearForPanel())
        {
            ClearForPanelAnswer();
            mojiCube.SetActive(true);
            Instantiate(
            katanaObject,
            transObjectList[0].gameObject.transform.position,
            Quaternion.identity
             );
            TalkObjectDisplayShift(talkObject, 4, 7);
            eventOnceList[4] = true;
        }
    }

    private void ClearForPanelAnswer()  //4つの文字があっていた時の動作
    {
        playerController.Acceleration = 0.15f;
        if (selectNumberPanelFor.transform.root.transform.GetChild(1).gameObject != null)
        {
            selectNumberPanelFor.transform.root.transform.GetChild(1).gameObject.SetActive(true);
        }
        selectNumberPanelFor.transform.root.transform.GetChild(2).gameObject.SetActive(false);//青いポインタを消す
        selectNumberPanelFor.gameObject.SetActive(false);
    }

    [SerializeField]
    private CharacterNamePanel characterNamePanel;

    private void Event6()  //黄瀬の名前が判明した後
    {
        if (eventOnceList[5]) return;
        if (characterNamePanel.GetisChecknameAnswerPanel())
        {
            TalkObjectDisplayShift(talkObject, 7, 8);
            CharacterBodyRotate(character[0]);
            eventOnceList[5] = true;
        }
    }

    private void Event7()  //黄瀬との最後の会話が終わった後
    {
        if (eventOnceList[6]) return;
        if (chapter2Talk4.GetisCheckAllRead())
            StartCoroutine(Event7_FadeandNewscene());
    }

    IEnumerator Event7_FadeandNewscene()
    {
        eventOnceList[6] = true;
        FadeandDestroyCanvas(8);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(nextLoadScene);
    }

}
