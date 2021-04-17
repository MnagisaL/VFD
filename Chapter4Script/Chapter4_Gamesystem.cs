using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Chapter4_Gamesystem : Gamesystem
{
    private CharacterText chapter4Talk1;
    private CharacterText chapter4Talk2;
    private CharacterText chapter4Talk3;
    private CharacterText chapter4Talk4;

    void Start()
    {
        SetEventOnceNum(7);
        InitializeEnvironment();
    }

    void Update()
    {
        Event1();                //最初の会話終了後
        Event2();                //すべてのオブジェクトが置き終わった後
        AkanishiLetterPut();     //赤西に手紙を渡した後
        KiseLetterPut();         //黄瀬に手紙を渡した後
        Event3();                //赤西と黄瀬に話しかけた後
        Event4();                //先生に話し終わった後鍵を貰う
        Event5();                //違う部屋に行く
    }

    protected override void InitializeTalk()  //会話後に変化を与えるObjectの取得
    {
        chapter4Talk1 = talkObject[0].GetComponentInChildren<CharacterText>();
        chapter4Talk2 = talkObject[4].GetComponentInChildren<CharacterText>();
        chapter4Talk3 = talkObject[5].GetComponentInChildren<CharacterText>();
        chapter4Talk4 = talkObject[6].GetComponentInChildren<CharacterText>();
    }

    private void Event1()  //最初の会話終了後
    {
        if (eventOnceList[0]) return;
        if (chapter4Talk1.GetisCheckAllRead())
        {
            playerController.Acceleration = 0.15f;
            FadeandDestroyCanvas(0, 0.6f, 0);
            Destroy(talkObject[0].transform.GetChild(0).gameObject);
            eventOnceList[0] = true;
        }
    }

    [SerializeField]
    private CheckHitObject[] check_HitObjects;

    [SerializeField]
    private GameObject plasma;
    [SerializeField]
    private GameObject[] letter;

    private void Event2()  //すべてのオブジェクトが置き終わった後
    {
        if (eventOnceList[1]) return;
        if (check_HitObjects[0].GetisObjctStay() && check_HitObjects[1].GetisObjctStay() && check_HitObjects[2].GetisObjctStay())
        {
            StartCoroutine(LetterMake());
        }
    }

    IEnumerator LetterMake()  //雷の演出を出した後に手紙を2つ生成
    {
        eventOnceList[1] = true;
        check_HitObjectLetters[0].gameObject.SetActive(true);
        check_HitObjectLetters[1].gameObject.SetActive(true);
        audioSource.PlayOneShot(se[0], 0.8f);
        Instantiate(
                        plasma,
                        transObjectList[0].gameObject.transform.position,
                        Quaternion.identity
                        );
        yield return new WaitForSeconds(2.0f);
        Instantiate(
                       letter[0],
                       transObjectList[0].gameObject.transform.position + new Vector3(-0.7f, 0, 0),  //少し位置をずらす
                       Quaternion.identity
                       );
        Instantiate(
                       letter[1],
                       transObjectList[0].gameObject.transform.position + new Vector3(0.7f, 0, 0),
                       Quaternion.identity
                       );
    }

    [SerializeField]
    private CheckHitObject[] check_HitObjectLetters;

    private void AkanishiLetterPut()  //赤西に手紙を渡した後
    {
        if (eventOnceList[2]) return;
        if (check_HitObjectLetters[0].GetisObjctStay())
        {
            TalkObjectDisplayShift(talkObject, 1, 4);
            eventOnceList[2] = true;
        }
    }

    private void KiseLetterPut()  //黄瀬に手紙を渡した後
    {
        if (eventOnceList[3]) return;
        if (check_HitObjectLetters[1].GetisObjctStay())
        {
            TalkObjectDisplayShift(talkObject, 2, 5);
            eventOnceList[3] = true;
        }
    }

    private void Event3()  //赤西と黄瀬に話しかけた後
    {
        if (eventOnceList[4]) return;
        if (chapter4Talk2.GetisCheckAllRead() && chapter4Talk3.GetisCheckAllRead())
        {
            TalkObjectDisplayShift(talkObject, 3, 6);
            eventOnceList[4] = true;
        }
    }

    [SerializeField]
    private GameObject lastKey;
    [SerializeField]
    private GameObject lastPoint;

    private void Event4()  //先生に話し終わった後鍵を貰う
    {
        if (eventOnceList[5]) return;
        if (chapter4Talk4.GetisCheckAllRead())  //鍵生成
        {
            audioSource.PlayOneShot(se[1], 0.8f);  //鍵を落とした音
            Instantiate(
                  lastKey,
                  transObjectList[1].gameObject.transform.position,
                  Quaternion.identity
                  );
            lastPoint.gameObject.SetActive(true);  //最後のスポットを開ける
            eventOnceList[5] = true;
        }
    }

    [SerializeField]
    private ToLastRoom toLastRoom;

    private void Event5()  //違う部屋に行く
    {
        if (eventOnceList[6]) return;
        if (toLastRoom.GetisCheckLastRoomHit())
        {
            StartCoroutine(Event5_FadeandNewScene());
        }
    }

    IEnumerator Event5_FadeandNewScene()
    {
        eventOnceList[6] = true;
        fade.FadeOn(0, 1);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(nextLoadScene);
    }

}
