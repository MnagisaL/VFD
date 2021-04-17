using OVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Mybutton : MonoBehaviour
{
    int Page = 0;　//移るページ

    public GameObject menu_1;//menu-1のこと
    public GameObject menu0;
    public GameObject menu1;
    public GameObject menu2;
    public GameObject Clear1;

  public static  bool startorclear=true; //false clear画面 true menu画面 別のシーンの時に橋渡し
 
    public GameObject gameobject;

    public GameObject fire_L1;
    public GameObject fire_L2;
    public GameObject fire_L3;
    public GameObject fire_L4;
    public GameObject fire_L5;
    public GameObject fire_L6;

    public GameObject fire_R1;
    public GameObject fire_R2;
    public GameObject fire_R3;
    public GameObject fire_R4;
    public GameObject fire_R5;
    public GameObject fire_R6;

   
    public AudioClip fire;//音出します
    //public AudioClip button1;
    AudioSource audioSource;

    private float startTime=0;
   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Update()
    {

        //if (startorclear == true)
        //{
        Firel();//ちょっとおかしい
        //}
        page_count();
        henka();
      //  setfloatbest(stage,mode);
        scriptscore();
        
    }
    
    public static string stage;//stage名　これは別のシーンに送られる
    public static string mode;//mode どんなモードのゲームか

    bool menu1_flag = false;//どれか押さないとnext押せない
    bool menu2_flag = false;//どれか押さないとstart押せない

    bool color_flag=false;
    float colortime = 0;
    /// ボタンをクリックした時の処理
    public void OnClick(string other)
    {
        if (other == "next")//ネクスト選択で次の画面
        {
            if (Page == 0) {
                Page++;
            }
            if (Page == 1 && menu1_flag == false)//page1の時にmenu2_flagがfalseだった場合次のページに遷移しない
            {
                Page = 1;
            }
            else if (Page == 1 && menu1_flag == true)
            {//page1の時にmenu2_flagがtrueだった場合次のページに遷移する
                Page++;
            }
        }
        if (other == "back")
        {　　//バック選択でページ戻る

            if (Page == 1 && menu1_flag == true) {//page1でのフラグを戻す
                menu1_flag = false;
            }
            if (Page == 2 && menu1_flag == true)
            {//page2でのフラグを戻す
                menu1_flag = false;
            }
            if (Page == 2 && menu2_flag == true) {//page2でのフラグを戻す
                menu2_flag = false;
            }
            Page--;
            if (Page <= 0)
                Page = 0;
        }
        //menu1での処理
        if (other == "Button_ol1")
        {
            stage = "school";//左上
            menu1_flag = true;
            //これがtrueになったら色を変える
        //色変え
        }
        else if (other == "Button_or1")
        {
            stage = "sname1";//name1に作ったステージ右上
            menu1_flag = true;
        }
        else if (other == "Button_ul1")
        {
            stage = "sname2";//name2に作ったステージ左下
            menu1_flag = true;
        }
        else if (other == "Button_ur1")
        {
            stage = "sname3";//name3に作ったステージ右下
            menu1_flag = true;
        }

        // menu2での処理
        if (other == "Button_ol2")
        {
            menu2l = true;
            mode = "kazi"; //左上
            menu2_flag = true;
            
        }
        else if (other == "Button_or2")
        {
            menu2l = true;
            mode = "teiden";//name1に作ったステージ右上
            menu2_flag = true;
        }
        else if (other == "Button_ul2")
        {
            menu2l = true;
            mode = "zisin";//name2に作ったステージ左下
            menu2_flag = true;
        }
        else if (other == "Button_ur2")
        {
            menu2l = true;
            mode = "shouka";//name3に作ったステージ右下
            menu2_flag = true;
        }

        if (other == "start"&& menu2_flag == true)
        {
            Invoke("JumpToScene", 2.0f);
        }
        if (other == "newgame") {//ニューゲーム
            color_flag = true;
            menu_1keep = false;
            startorclear = true;
        }
        if (other == "retry") {//リトライ
            Invoke("JumpToScene", 2.0f);
        }
    }
    bool menu1_keep = true;
    bool menu_1keep = true;

    bool menu2l = false;
    public bool counter_flag = false;
    public void Firel() {
        //ボタンを押したら時間が進む
        if ((OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))) {
            counter_flag = true;
        }
        if (counter_flag == true)
        {
            startTime += Time.deltaTime;
            Debug.Log("計測中： " + (startTime).ToString());
        }
        //  火出しますかー
        if (startTime >= 0.1)
        {
            fire_L1.SetActive(true);
            fire_R1.SetActive(true);
            audioSource.PlayOneShot(fire);
        }

        if (startTime >= 0.25)
        {
            fire_L2.SetActive(true);
            fire_R2.SetActive(true);
            audioSource.PlayOneShot(fire);
        }
        if (startTime >= 0.4)
        {
            fire_L3.SetActive(true);
            fire_R3.SetActive(true);
            audioSource.PlayOneShot(fire);
        }
        if (startTime >= 0.55)
        {
            fire_L4.SetActive(true);
            fire_R4.SetActive(true);
            audioSource.PlayOneShot(fire);
        }
        if (startTime >= 0.7)
        {
            fire_L5.SetActive(true);
            fire_R5.SetActive(true);
            audioSource.PlayOneShot(fire);
        }
        if (startTime >= 0.85)
        {
            fire_L6.SetActive(true);
            fire_R6.SetActive(true);
            audioSource.PlayOneShot(fire);
        }
        if (startTime >= 1)
        {
            startTime = 1;
        }
        if (fire_R6.activeSelf == false)
        {
            if (menu_1keep)
            {
                menu_1.SetActive(true);
            }
            else
            {
                menu_1.SetActive(false);
            }
        }

        if ((fire_R6.activeSelf == true))
        {
            menu_1.SetActive(false);
        }
    }
    bool setac = true;
    private void page_count() {
        //menu遷移
        if (startorclear == false)
        {//クリア画面
            light.SetActive(true);
            ChangeColorOfGameObject(target, colorset());
            menu_1.SetActive(false);
            menu0.SetActive(false);
            menu1.SetActive(false);
            menu2.SetActive(false);
            Clear1.SetActive(true);
        }
        else if (startorclear == true)
        {
            Clear1.SetActive(false);
            if (Page == 0)
            {
                menu0.SetActive(true);
                menu1.SetActive(false);
                menu2.SetActive(false);
            }
            else if (Page == 1)
            {
                menu0.SetActive(false);
                menu1.SetActive(true);
                menu2.SetActive(false);
            }
            else if (Page == 2)
            {
                menu0.SetActive(false);
                menu1.SetActive(false);
                menu2.SetActive(true);
            }
        }
    }
    public  GameObject light;
    void JumpToScene() //シーンの切り替え
    {
        startorclear = false;
        if (stage == "school" && mode == "kazi")
        {
            SceneManager.LoadScene("VFD_Akagawa");
        }
        if (stage == "school" && mode == "zisin")
        {
            SceneManager.LoadScene("VFD_zisin");
        }
        if (stage == "school" && mode == "shouka")
        {
            SceneManager.LoadScene("VFD_shoukamode");
        }
    }
    public GameObject target;
    void henka() {//色の変化
        if (color_flag) {
            ChangeColorOfGameObject(target, colorset());
        }
    }
    private Color colorset() {//ここで色を変える
        Color color;
        color = new Color(1, 1, 1, 1);//白
        if (color_flag) {
            colortime += Time.deltaTime;
            if (colortime >= 100)
            {
                colortime = 100;
            }
            color = new Color(1-colortime*0.4f, 1 - colortime * 0.4f, 1 - colortime * 0.4f, 1);
        }
        return color;
    }
    private void ChangeColorOfGameObject(GameObject targetObject, Color color)　//色を変える
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



    public GameObject scorekazu;//scoretext
    public GameObject bestscorekazu;//bestscoretext

    public GameObject bestscore_menu2;
    public static float bestscore=0;

    public void scriptscore()
    {
        Text score_text = scorekazu.GetComponent<Text>();
        Text bestscore_text = bestscorekazu.GetComponent<Text>();
       // Text best_menu2 = bestscore_menu2.GetComponent<Text>();//menu2の時に表示する目
        score_text.text = "" + thisscore();//スコア
        bestscore_text.text = "" +highscore();//highスコア
        if (menu2l)
        {
            bestscore_menu2.SetActive(true);
        }
           // best_menu2.text = "" + menu2sc();
        
    }
    public int thisscore() {//スコア??
        int thissco = 0;
        if (stage == "school" && mode == "kazi")
        {
            thissco= ManageScore.nowGameScore;
        }
        if (stage == "school" && mode == "shouka")
        {
            thissco = CriateObject.score;
        }
        return thissco;
    }
    public int highscore() {
        int highscore = 0;
        if (stage == "school" && mode == "kazi")
        {
            highscore = PlayerPrefs.GetInt("HinanHighscore"); 
        }
        if (stage == "school" && mode == "shouka")
        {
            highscore = PlayerPrefs.GetInt("ShoukaHighscore");
        }
        return highscore;
    }
    public int menu2sc() {
        
        if (stage == "school" && mode == "kazi")
        {
            return  highscore();
        }
        else {
            return 0;
        }
    }
}

