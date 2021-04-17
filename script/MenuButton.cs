using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*
 *メニューシーン以外のシーンで
 *メニューシーンに戻らせるUI
 */

public class MenuButton : SingletonMonoBehaviour<MenuButton>
{
    private Transform target;  //mainカメラ

    private OVRPlayerController playerController;
    private float playerAcceleration = 0;

    private OVRScreenFade fade;
    private const float FADELENGTH = 0.6f;

    [SerializeField]
    private float distanceToPlayer = 2.0f;  //プレイヤーとこのUIの距離
    [SerializeField]
    private float followSpeed = 0.1f;  //ついてくる速度

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] se;
    private const string AUDIO_TAGSNAME = "Sound";

    [SerializeField]
    private string loadScene;

    private enum SeCollection
    {
        MENUOPEN,
        SELECT,
        NO,
        YES
    }

    public void Awake()  //最初以外のインスタンスの場合これを消す
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);  //canvasは最初非表示
    }

    private void Update()
    {
        StartButtonPush();
        ReStartButtonPush();
        PutStick();
        SelectNo();
        SelectYes();
    }

    private void LateUpdate()
    {
        MenuButtonTransform();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)  //シーンをロードするたびに変数の初期化
    {
        if (!target) target = Camera.main.transform;  //各シーンのOVRPlayerControllerに追従させる
        target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        fade = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OVRScreenFade>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<OVRPlayerController>();
        if (GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME) != null)
            audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
        switch (scene.name)  //シーンによってプレイヤーの速度保存
        {
            case "VFD_Akagawa":
            case "VFD_zisin":
                playerAcceleration = 0.1f;
                break;
            case "VFD_shoukamode":
                playerAcceleration = 0.2f;
                break;
            default:
                playerAcceleration = 0.15f;
                break;
        }
    }

    private void MenuButtonTransform()  //playerの正面からの少し離れた距離に置く、少し遅れて追従させる。
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.position + transform.forward * distanceToPlayer, followSpeed);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.rotation, followSpeed);
    }

    private bool isCheckPushFirst = false;

    private void StartButtonPush()  //スタートボタンを押したときの処理
    {
        if (OVRInput.GetUp(OVRInput.RawButton.Y, OVRInput.Controller.LTouch)
            && SceneManager.GetActiveScene().name != "UIstart1" && !isCheckPushFirst && playerController.Acceleration != 0)
        {
            StartCoroutine(deltaTime());
            IEnumerator deltaTime()
            {
                var menuSe = SeCollection.MENUOPEN;
                audioSource.PlayOneShot(se[(int)menuSe]);
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                playerController.Acceleration = 0;
                fade.FadeOn(0, FADELENGTH);
                yield return new WaitForSeconds(0.1f);
                isCheckPushFirst = true;
            }
        }
    }

    [SerializeField]
    private Image[] answerImage;  //選択の画像
    [SerializeField]
    private Sprite[] colorSprite;  //スプライト 0黒1赤

    private bool isCheckYes = false;  //yesを押したときの判定

    private void PutStick()
    {
        if (isCheckPushFirst)
        {
            var selectSe = SeCollection.SELECT;
            if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
            {
                audioSource.PlayOneShot(se[(int)selectSe]);
                isCheckYes = false;
            }
            if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
            {
                audioSource.PlayOneShot(se[(int)selectSe]);
                isCheckYes = true;
            }
        }
    }

    private void ReStartButtonPush()  //もう一回menuボタンを押したとき
    {
        if ((OVRInput.GetUp(OVRInput.RawButton.Y, OVRInput.Controller.LTouch)) && isCheckPushFirst)
        {
            StartCoroutine(deltaTime());
            IEnumerator deltaTime()
            {
                ReturnToNormal();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void SelectNo()  //いいえを選択　
    {
        if (!isCheckYes && this.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            answerImage[0].sprite = colorSprite[1];
            answerImage[1].sprite = colorSprite[0];
            if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                ReturnToNormal();
            }
        }
    }

    private void ReturnToNormal()  //通常状態に戻す。
    {
        var noSe = SeCollection.NO;
        audioSource.PlayOneShot(se[(int)noSe]);
        playerController.Acceleration = playerAcceleration;
        fade.FadeOn(FADELENGTH, 0);
        isCheckPushFirst = false;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void SelectYes()  //はいを選択 modeを初期化しタイトルシーンに戻る
    {
        if (isCheckYes && this.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            answerImage[0].sprite = colorSprite[0];
            answerImage[1].sprite = colorSprite[1];
            if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                StartCoroutine(NewScene());
                IEnumerator NewScene()
                {
                    var yesSe = SeCollection.YES;
                    audioSource.PlayOneShot(se[(int)yesSe]);
                    fade.FadeOn(FADELENGTH, 1);
                    isCheckPushFirst = false;
                    yield return new WaitForSeconds(2.0f);
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    Select_Clear.Mode = "";//初期化
                    Select_Clear.isPressedNewGame = false;
                    SceneManager.LoadScene(loadScene);
                }
            }
        }
    }

}
