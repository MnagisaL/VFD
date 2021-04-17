using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // 追加しましょう

public class manager : MonoBehaviour//次のシーンに変数送る
{

  
    public GameObject score_object = null; // Textオブジェクト
    public static float HP;
    public static float timelimit;


    // 初期化
    void Start()
    {
        HP = Random.Range(0, 10);
        timelimit = Random.Range(0, 60);
    }
        // 更新
        void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text score_text = score_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        score_text.text = Mybutton.stage+" "+Mybutton.mode+" "+HP*timelimit;

        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("UIstart");
            
        }
    }
}