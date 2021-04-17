using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*
 読み込まれるテキストはListの要素1から
最後にプレイヤーがどうするべきか書く
 */

public class CharacterText : MonoBehaviour
{
    [SerializeField]
    private Text nameText = default;
   
    [SerializeField]
    private Text textTalk = default;

    private AudioSource audioSource;

    [FormerlySerializedAs("Push_Button")]
    [SerializeField]
    private AudioClip pushButton;

    private const string AUDIO_TAGSNAME = "Sound";

    [SerializeField]
    [Multiline(2)]//インスペクター上では改行コードが扱えないのでMultiline 
    List<string> talkTextList = new List<string>();

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        DispTalk();
        ChangeColor();
    }

    private bool isCheckAbuttonPush = false;

    private string[] SpilitText(int textLine)//文字を","で分ける
    {
        string[] characterTextString = talkTextList[textLine].Split(',');//カンマで分ける
        return characterTextString;
    }

    private int textLine = 0;

    private int CountAbuttonNum()//Aボタンが何回押されたか
    {
        if ((OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch)))
        {
            textLine++;
            textTalk.text = "";
            isCheckAbuttonPush = true;
        }
        return textLine;
    }

    private bool isCheckAllTextRead = false;

    [SerializeField][Range(0, 0.1f)]
    private float textSpeed = 0.03f;

    private void DispTalk()//テキスト表示
    {
        if (!DOTween.IsTweening(textTalk))
        {
            int textNum = CountAbuttonNum();

            if (isCheckAbuttonPush)
            {
                audioSource.PlayOneShot(pushButton, 0.2f);
                StartCoroutine(Vibrate(duration:0.1f, controller: OVRInput.Controller.RTouch));
                string Cname = "";//名前を入力
                string Ctext = "";//テキスト
                Cname = SpilitText(textNum)[0];
                Ctext = SpilitText(textNum)[1];
                nameText.text = Cname;
                textTalk.DOText(Ctext, Ctext.Length * textSpeed);
            }
            if (talkTextList.Count - 1 == textLine)
            {
                this.isCheckAllTextRead = true;
                textLine = 0;
            }
            isCheckAbuttonPush = false;
        }
    }

    private IEnumerator Vibrate(float duration = 0.1f, float frequency = 0.2f, float amplitude = 0.2f, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        //コントローラーを振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }

    private Image image;
    private Color PanelColor = new Color(0, 0.8f, 0.3f, 0.8f);
    private bool isChangePaneled;

    private void ChangeColor()//テキストを読み終えたら色を変える
    {
        if (isCheckAllTextRead && textLine >= 1 && !isChangePaneled)
        {
            image = this.GetComponent<Image>();
            image.color = PanelColor;
            isChangePaneled = true;
        }
    }

    public bool GetisCheckAllRead()
    {
        return isCheckAllTextRead;
    }

}
