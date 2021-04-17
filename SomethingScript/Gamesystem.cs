using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamesystem : MonoBehaviour
{

/*
 * シーンのゲームシステムに継承する。
 * あらかじめよく使う動きはここに書く
 */

    [SerializeField]
    protected OVRScreenFade fade;//画面の暗さ

    [SerializeField]
    protected OVRPlayerController playerController;  //プレイヤーの動き

    [SerializeField]
    protected GameObject[] talkObject;//会話パート

    [SerializeField]
    protected List<GameObject> transObjectList = new List<GameObject>();//gameObjectを生成trans

    protected AudioSource audioSource;

    [SerializeField]
    protected AudioClip[] se;
    private const string AUDIO_TAGSNAME = "Sound";

    private const string TAGSNAME_CANVAS = "Talk_Canvas";
    private const string TAGSNAME_NOTFIRSTOBJECT = "notFirstpos_Talk";

    [SerializeField]
    protected string nextLoadScene;  //次の遷移するシーン

    [SerializeField]
    protected GameObject[] character;
    protected void InitializeEnvironment()  //初期化　最初に映すものなどを決める
    {
        fade.FadeOn(1, 0.6f);
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
        InitializeEventOnceList();
        InitializeTalk();
        DisActive(TAGSNAME_CANVAS);
        DisActive(TAGSNAME_NOTFIRSTOBJECT);//canvasを非表示にしてからオブジェクトを非表示
    }

    protected List<bool> eventOnceList = new List<bool>();
    private int eventOnceNum;

    protected void SetEventOnceNum(int eventOnceNum)
    {
        this.eventOnceNum = eventOnceNum;
    }

    private void InitializeEventOnceList()  //1回だけ行いたいものを扱うboolリストの初期化
    {
        for (int i = 0; i < eventOnceNum; i++)
        {
            eventOnceList.Add(false);
        }
    }

    protected virtual void InitializeTalk()  //会話後に変化を与えるObjectの取得
    {

    }

    private void DisActive(string tagsName)  //getcomponetしてからタグのついたオブジェクトを全て取得し非表示にする
    {
        GameObject[] disActiveObject = GameObject.FindGameObjectsWithTag(tagsName);
        foreach (GameObject gameObj in disActiveObject)
        {
            gameObj.SetActive(false);
        }
    }

    protected void FadeandDestroyCanvas(int talkObjectNum, float beforeFade = 0, float afterFade = 1)  //フェードした後にcanvasを消す
    {
        fade.FadeOn(beforeFade, afterFade);
        Destroy(talkObject[talkObjectNum].transform.GetChild(0).gameObject);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }

    protected void TalkObjectDisplayShift(GameObject[] talkOb, int falseNum, int trueNum)  //会話の何か非表示にして何かを表示させる
    {
        talkOb[falseNum].SetActive(false);
        talkOb[trueNum].SetActive(true);
    }

    protected void CharacterBodyRotate(GameObject character) //体の向きをy軸固定でカメラ向く
    {
        Vector3 CharacterRotate = Camera.main.transform.position;
        CharacterRotate.y = character.transform.position.y;
        character.transform.LookAt(CharacterRotate);
    }

}
