using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Select_Clear : MonoBehaviour
{
    [SerializeField]
    private OVRScreenFade fade;  //画面をフェード
    //火のオブジェクト左と右
    [SerializeField]
    private GameObject[] fire_L;
    [SerializeField]
    private GameObject[] fire_R;

    public static bool isPressedNewGame = false;  //new gameを押したか

    [SerializeField]  //セレクトパネルのリスト
    private List<GameObject> PanelList = new List<GameObject>();

    [SerializeField]
    private GameObject stage;
    [SerializeField]
    private new Light light;

    //オーディオ関係
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip se;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        ClearDisp();
        ScriptScore();
        ClearTextDisplay();
    }

    public void OnClick_Menu0(string other)  //最初の画面の処理　次のmenuを開く
    {
        if (other == "Menu0")
            PanelDispControl(PanelList, 0, 1);
    }

    private bool isFireOnced;  //火が出る仕掛けのトリガー

    //Menu1の画面の処理　次のmenuを開く
    public void OnClick_Menu1(string other)
    {
        if (other == "Menu1")
        {
            PanelDispControl(PanelList, 1, 2);
            if (isFireOnced) return;
            StartCoroutine(Fire(0.2f));
            isFireOnced = true;
        }
    }

    //指定した秒数ごとに火を表示
    private IEnumerator Fire(float second)
    {
        int fireIndex = 0;
        while (fireIndex < fire_L.Length)
        {
            yield return new WaitForSeconds(second);
            fire_L[fireIndex].SetActive(true);
            fire_R[fireIndex].SetActive(true);
            audioSource.PlayOneShot(se);
            fireIndex++;
        }
    }

    public static string Mode = "";  //このモードによって次に飛ぶシーンやクリア画面の表示を変える。

    private bool doMenu2Pushed;  //スタートを押したらfalseに戻す。
    [SerializeField]
    private GameObject[] Menu2_ModeDiscriptionImage;

    public void OnClick_Menu2(string other)  //Menu2の処理
    {
        switch (other)
        {
            case "Back2":
                doMenu2Pushed = false;
                PanelDispControl(PanelList, 2, 1);
                break;
            case "Hinan":
                Menu2_Menu3_Pushed(Menu2_ModeDiscriptionImage, 0, 2, "Hinan");
                break;
            case "Zisin":
                Menu2_Menu3_Pushed(Menu2_ModeDiscriptionImage, 1, 2, "Zisin");
                break;
            case "Shouka":
                Menu2_Menu3_Pushed(Menu2_ModeDiscriptionImage, 2, 2, "Shouka");
                break;
            case "Dasshutu":
                PanelDispControl(PanelList, 2, 3);
                break;
            case "Start2":
                if (doMenu2Pushed)
                {
                    doMenu2Pushed = false;
                    StartCoroutine(FadeAndScene());
                }
                break;
        }
    }

    private bool doMenu3Pushed;  //スタートを押したらfalseに戻す。
    [SerializeField]
    private GameObject[] Menu3_ModeDiscriptionImage;

    public void OnClick_Menu3(string other)
    {
        switch (other)
        {
            case "Back3":
                doMenu3Pushed = false;
                PanelDispControl(PanelList, 3, 2);
                break;
            case "Chapter1":
                Menu2_Menu3_Pushed(Menu3_ModeDiscriptionImage, 0, 3, "Chapter1");
                break;
            case "Chapter2":
                Menu2_Menu3_Pushed(Menu3_ModeDiscriptionImage, 1, 3, "Chapter2");
                break;
            case "Chapter3":
                Menu2_Menu3_Pushed(Menu3_ModeDiscriptionImage, 2, 3, "Chapter3");
                break;
            case "Chapter4":
                Menu2_Menu3_Pushed(Menu3_ModeDiscriptionImage, 3, 3, "Chapter4");
                break;
            case "Start3":
                if (doMenu3Pushed)
                {
                    doMenu3Pushed = false;
                    StartCoroutine(FadeAndScene());
                }
                break;
        }
    }

    private void Menu2_Menu3_Pushed(GameObject[] discription, int modeNum, int menuNum, string modeName)  //ボタンを押したときの変化
    {

        foreach (GameObject gameObj in discription)
        {
            gameObj.SetActive(false);
        }
        discription[modeNum].SetActive(true);
        if (menuNum == 2)
            doMenu2Pushed = true;
        if (menuNum == 3)
            doMenu3Pushed = true;
        Mode = modeName;
    }

    public void OnClick_Clear(string other)
    {
        if (other == "Retry")
            StartCoroutine(FadeAndScene());
        if (other == "NewGame")
        {
            PanelDispControl(PanelList, 4, 1);
            ChangeColorOfGameObject(stage, Color.black);
            StartCoroutine(LightGradually());
            isPressedNewGame = false;
        }
    }

    [SerializeField]
    private Text[] clearText;

    private void ClearTextDisplay()  //モードによってクリアを変化
    {
        if (!(Mode == "Hinan") || (Mode == "Shouka"))
        {
            for (int i = 0; i < clearText.Length; i++)
            {
                clearText[i].gameObject.SetActive(false);
            }
        }
        if (Mode == "Hinan" || Mode == "Shouka")
        {
            for (int i = 0; i < clearText.Length; i++)
            {
                clearText[i].gameObject.SetActive(true);
            }
        }
    }

    private void ScriptScore()  //スコアの表示
    {
        clearText[2].text = "" + ThisScore();
        clearText[3].text = "" + HighScore();
    }

    private int ThisScore()  //スコア計算
    {
        int thisscore = 0;
        if (Mode == "Hinan")
            thisscore = ManageScore.nowGameScore;
        if (Mode == "Shouka")
            thisscore = CriateObject.score;
        return thisscore;
    }

    private int HighScore()  //ハイスコアの取得
    {
        int highscore = 0;
        if (Mode == "Hinan")
            highscore = PlayerPrefs.GetInt("HinanHighscore");
        if (Mode == "Shouka")
            highscore = PlayerPrefs.GetInt("ShoukaHighscore");
        return highscore;
    }

    private void ChangeColorOfGameObject(GameObject targetObject, Color color)　 //選んだオブジェクトの色を変える
    {
        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            foreach (Material material in targetRenderer.materials)
            {
                material.color = color;
            }
        }
        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
        }
    }

    private void ClearDisp()  //クリアした状態の場合ステージの色の変化とpanelの変化
    {
        if (isPressedNewGame)
        {
            PanelDispControl(PanelList, 0, 4);
            ChangeColorOfGameObject(stage, Color.white);
            light.color = Color.white;
        }
    }

    private const float LIGHTGRADUALLYTIME = 0.1f;  //色の変化の速さ

    private IEnumerator LightGradually()
    {
        float LightColor = 0.03f;
        while (LightColor <= 1)
        {
            light.color = new Color(1 - LightColor, 1 - LightColor, 1 - LightColor, 1);
            yield return new WaitForSeconds(LIGHTGRADUALLYTIME);
            LightColor += 0.03f;
        }
    }

    private const float FADETIME = 2.0f;

    private IEnumerator FadeAndScene()  //フェードとシーン移動
    {
        fade.FadeOn(0, 1);
        yield return new WaitForSeconds(FADETIME);
        isPressedNewGame = true;
        JumpToScene();
    }

    private void JumpToScene()  //Modeによってシーンの飛ぶ場所を変化
    {
        switch (Mode)
        {
            case "Hinan":
                SceneManager.LoadScene("VFD_Akagawa");
                break;
            case "Zisin":
                SceneManager.LoadScene("VFD_zisin");
                break;
            case "Shouka":
                SceneManager.LoadScene("VFD_shoukamode");
                break;
            case "Chapter1":
                SceneManager.LoadScene("VFD_chapter1");
                break;
            case "Chapter2":
                SceneManager.LoadScene("VFD_chapter2");
                break;
            case "Chapter3":
                SceneManager.LoadScene("VFD_chapter3");
                break;
            case "Chapter4":
                SceneManager.LoadScene("VFD_chapter4");
                break;
            default:
                Debug.LogError("選択されていません");
                break;
        }
    }

    private void PanelDispControl(List<GameObject> panelList, int falseNum, int trueNum)  //パネルのどれかを表示して何かを消す。
    {
        panelList[falseNum].SetActive(false);
        panelList[trueNum].SetActive(true);
    }

}
