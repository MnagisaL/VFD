using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Chapter1_Gamesystem : Gamesystem
{
    private CharacterText chapter1Talk1;
    private CharacterText chapter1Talk2;
    private CharacterText chapter1Talk3;
    private CharacterText chapter1Talk4;

    void Start()
    {
        SetEventOnceNum(5);
        InitializeEnvironment();
    }

    void Update()
    {
        Event1();     //最初の会話終了後
        Event2();     //女との初めての会話終了後
        Event3();     //鏡合わせにする部屋をクリア後
        Event4();     //「たいいく館」の文字に合わせるゲームクリア後
        Event5();     //隠されたレバーを押した後
        Event6();     //赤西の名前が判明後
        Event7();     //Chapter1最後の会話終了後
    }

    protected override void InitializeTalk()  //会話後に変化を与えるObjectの取得
    {
        chapter1Talk1 = talkObject[0].GetComponentInChildren<CharacterText>();//talk1
        chapter1Talk2 = talkObject[1].GetComponentInChildren<CharacterText>();//talk2
        chapter1Talk3 = talkObject[2].GetComponentInChildren<CharacterText>();//talk3
        chapter1Talk4 = talkObject[6].GetComponentInChildren<CharacterText>();//talk3
    }

    private void Event1()  //最初の会話終了後
    {
        if (eventOnceList[0]) return;
        if (chapter1Talk1.GetisCheckAllRead())
        {
            playerController.Acceleration = 0.15f;
            FadeandDestroyCanvas(0, 0.6f, 0);
            eventOnceList[0] = true;
        }
    }

    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameObject door;//ドア

    private void Event2()  //女との初めての会話終了後 鍵を出す
    {
        if (eventOnceList[1]) return;
        if (chapter1Talk2.GetisCheckAllRead())
        {
            door.SetActive(true);
            Instantiate(
                key,
                transObjectList[0].gameObject.transform.position,
                Quaternion.identity
                );
            audioSource.PlayOneShot(se[0]);
            eventOnceList[1] = true;
        }
    }

    [SerializeField]
    private Check_AllMirrorRoom check_AllMirrorRoom;

    private void Event3()  //鏡合わせにする部屋をクリア後
    {
        if (eventOnceList[2]) return;
        if (check_AllMirrorRoom.GetisObjectStay()) TalkObjectDisplayShift(talkObject, 1, 2);
        if (chapter1Talk3.GetisCheckAllRead()) StartCoroutine(Event3_FadeandShift());
    }

    private bool mirrorPlayerStay = false;
    IEnumerator Event3_FadeandShift()
    {
        eventOnceList[2] = true;
        FadeandDestroyCanvas(2);
        yield return new WaitForSeconds(1.5f);
        TalkObjectDisplayShift(talkObject, 2, 3);
        fade.FadeOn(1, 0);
        mirrorPlayerStay = true;
    }

    public bool GetisMirrorPlayerStay()
    {
        return mirrorPlayerStay;
    }

    [SerializeField]
    private Check_AllTaiikukan check_AllTaiikukan;
    [SerializeField]
    private GameObject taiikukanBlock;  //体育館をブロックするもの

    private void Event4()  //「たいいく館」の文字に合わせるゲームクリア後
    {
        if (eventOnceList[3]) return;
        if (check_AllTaiikukan.GetCheckAllObjectStay())
        {
            eventOnceList[3] = true;
            Invoke("BellSePlayOneShot", 1.0f);
            taiikukanBlock.SetActive(false);
            TalkObjectDisplayShift(talkObject, 3, 4);
        }
    }

    private void BellSePlayOneShot()  //ベルの音を遅れて出す
    {
        audioSource.PlayOneShot(se[1]);
    }

    [SerializeField]
    private RebaMoved rebaMove;

    private void Event5()//隠されたレバーを押した後
    {
        if (rebaMove.GetisRebaMoved()) TalkObjectDisplayShift(talkObject, 4, 5);
    }

    [SerializeField]
    private CharacterNamePanel characterNamePanel;

    private void Event6()  //赤西の名前が判明後
    {
        if (characterNamePanel.GetisChecknameAnswerPanel())
        {
            TalkObjectDisplayShift(talkObject, 5, 6);
            CharacterBodyRotate(character[0]);
        }
    }

    private void Event7()  //Chapter1最後の会話終了後
    {
        if (eventOnceList[4]) return;
        if (chapter1Talk4.GetisCheckAllRead())
            StartCoroutine(TT7Event_FadeandNewScene());
    }

    IEnumerator TT7Event_FadeandNewScene()
    {
        eventOnceList[4] = true;
        FadeandDestroyCanvas(6);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(nextLoadScene);
    }

}
