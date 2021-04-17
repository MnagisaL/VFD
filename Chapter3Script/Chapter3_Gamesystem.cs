using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Chapter3_Gamesystem : Gamesystem
{
    private CharacterText chapter3Talk1;  //赤西
    private CharacterText chapter3Talk2;  //先生
    private CharacterText chapter3Talk3;  //赤西
    private CharacterText chapter3Talk4;  //赤西
    private CharacterText chapter3Talk5;  //先生

    void Start()
    {
        SetEventOnceNum(6);
        InitializeEnvironment();
    }

    void Update()
    {
        Event1();     //1回目の赤西の会話が終わった後
        Event2();     //先生を見つけ、その会話が終わった後
        Event3();     //2回目の赤西に会話した後
        Event4();     //カレーに必要な素材が全部そろった後
        Event5();     //4回目の赤西との会話をした後
        Event6();     //最後の先生と会話した後
    }

    protected override void InitializeTalk()  //会話後に変化を与えるObjectの取得
    {
        chapter3Talk1 = talkObject[0].GetComponentInChildren<CharacterText>();//talk1赤西1
        chapter3Talk2 = talkObject[2].GetComponentInChildren<CharacterText>();//talk2先生
        chapter3Talk3 = talkObject[3].GetComponentInChildren<CharacterText>();//talk2赤西
        chapter3Talk4 = talkObject[8].GetComponentInChildren<CharacterText>();//talk3赤西
        chapter3Talk5 = talkObject[11].GetComponentInChildren<CharacterText>();//talk3先生
    }

    private void Event1()  //1回目の赤西の会話が終わった後
    {
        if (eventOnceList[0]) return;
        if (chapter3Talk1.GetisCheckAllRead())
        {
            playerController.Acceleration = 0.15f; //動く
            fade.FadeOn(0.6f, 0);
            eventOnceList[0] = true;
        }
    }

    [SerializeField]
    private GameObject[] kareMaterial;
    [SerializeField]
    private GameObject pigGameObject;

    private void Event2()  //先生を見つけ、その会話が終わった後
    {
        if (eventOnceList[1]) return;
        if (chapter3Talk2.GetisCheckAllRead())
        {
            eventOnceList[1] = true;
            TalkObjectDisplayShift(talkObject, 0, 3);
            TalkObjectDisplayShift(talkObject, 1, 4);
            TalkObjectDisplayShift(talkObject, 2, 5);
            CharacterBodyRotate(character[0]);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            foreach (GameObject gameObj in kareMaterial)  //次に必要なものをtrue
            {
                gameObj.SetActive(true);
            }
            pigGameObject.SetActive(true);
            talkObject[7].gameObject.SetActive(true);
        }
    }

    private void Event3()  //2回目の赤西に会話した後
    {
        if (eventOnceList[2]) return;
        if (chapter3Talk3.GetisCheckAllRead())
            StartCoroutine(Event3_FadeandActive());
    }

    [SerializeField]
    private GameObject[] cupandPot;

    IEnumerator Event3_FadeandActive()  //カップを設置
    {
        eventOnceList[2] = true;
        FadeandDestroyCanvas(3);
        yield return new WaitForSeconds(1.5f);
        foreach (GameObject gameObj in cupandPot)  //次に必要なものをtrue
        {
            gameObj.SetActive(true);
        }
        TalkObjectDisplayShift(talkObject, 3, 6);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        fade.FadeOn(1, 0);
    }

    [SerializeField]
    private CheckHitAllObject checkHitAllObject;

    private void Event4()  //カレーに必要な素材が全部そろった後
    {
        if (eventOnceList[3]) return;
        if (checkHitAllObject.GetCheckAllHitObject())
        {
            eventOnceList[3] = true;
            TalkObjectDisplayShift(talkObject, 6, 8);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }

    private void Event5()  //4回目の赤西との会話をした後
    {
        if (eventOnceList[4]) return;
        if (chapter3Talk4.GetisCheckAllRead())
            StartCoroutine(Event5_FadeandDisactive());
    }

    [SerializeField]
    private GameObject cupinKareObject;

    IEnumerator Event5_FadeandDisactive()  //カレーを作っている音の表現と、素材をすべて非アクティブに
    {
        eventOnceList[4] = true;
        FadeandDestroyCanvas(8);
        Invoke("SoundPlayCut", 2.0f);
        Invoke("SoundPlayIgnition", 4.0f);
        Invoke("SoundPlayBoil", 10.0f);
        yield return new WaitForSeconds(24.5f);
        foreach (GameObject gameObj in cupandPot)  //次に必要なものをfalse
        {
            gameObj.SetActive(false);
        }
        foreach (GameObject gameObj in kareMaterial)  //次に必要なものをfalse
        {
            gameObj.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("pig").SetActive(false);
        cupinKareObject.SetActive(true);
        TalkObjectDisplayShift(talkObject, 8, 9);
        TalkObjectDisplayShift(talkObject, 4, 10);
        TalkObjectDisplayShift(talkObject, 5, 11);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        fade.FadeOn(1, 0);
    }

    //順番で音を出す。
    private void SoundPlayCut()
    {
        audioSource.PlayOneShot(se[0]);
    }

    private void SoundPlayIgnition()
    {
        audioSource.PlayOneShot(se[1]);
    }

    private void SoundPlayBoil()
    {
        audioSource.PlayOneShot(se[2]);
    }

    private void Event6()  //最後の先生と会話した後
    {
        if (eventOnceList[5]) return;
        if (chapter3Talk5.GetisCheckAllRead())
        {
            StartCoroutine(Event6_FadeandNewscene());
        }
    }

    IEnumerator Event6_FadeandNewscene()
    {
        eventOnceList[5] = true;
        FadeandDestroyCanvas(11);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(nextLoadScene);
    }

}
