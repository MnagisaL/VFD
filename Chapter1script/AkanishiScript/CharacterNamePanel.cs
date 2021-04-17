using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*
 * 名前を当てさせるpanel
 */

public class CharacterNamePanel : SelectPanel
{
    private AudioSource audioSource;
    private const string AUDIO_TAGSNAME = "Sound";

    private List<string[]> answerStringList = new List<string[]>();

    [SerializeField]
    private string[] firstColumn;
    [SerializeField]
    private string[] secondColumn;
    [SerializeField]
    private string[] thirdColumn;
    [SerializeField]
    private string[] fourthColumn;
    [SerializeField]
    private string[] fifthColumn;

    private int columnNum;  //一つの列の長さ

    private bool isChecknameAnswerPanel = false;

    [SerializeField]
    private Text[] answerText;//回答のテキスト

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
        base.SetAudioSouce(audioSource);
        InitializeanswerStringList();
        columnNum = firstColumn.Length;
        isCheckColumnlengthEqual(secondColumn);
        isCheckColumnlengthEqual(thirdColumn);
        isCheckColumnlengthEqual(fourthColumn);
        isCheckColumnlengthEqual(fifthColumn);
    }

    void Update()
    {
        base.PutStick();
        TextAnswer();
    }

    private void InitializeanswerStringList()
    {
        answerStringList.Add(firstColumn);
        answerStringList.Add(secondColumn);
        answerStringList.Add(thirdColumn);
        answerStringList.Add(fourthColumn);
        answerStringList.Add(fifthColumn);
    }

    private void isCheckColumnlengthEqual(string[] column)  //列の長さが最初と同じか
    {
        if (column.Length != columnNum)
            Debug.LogError("配列が同じ長さではありません");
        else return;
    }

    private int textLine = 0;

    [SerializeField]
    private int[] trueNum;

    private void TextAnswer()  //あってる選択肢が選ばれたら続く
    {
        for (int i = 0; i < base.image.Length; i++)
        {
            answerText[i].text = answerStringList[i][textLine];//answerName[i, textLine];
        }
        for (int num = 0; num < columnNum; num++)
        {
            TextPlus(textLine, num, trueNum[num]);
        }
    }

    private void TextPlus(int textline, int sequence, int numans) //条件が成立したらtextline++ (n=0,1,2,3・・・)
    {
        if (textline == sequence && base.SelectNum == numans)  //文字があっていたら次の文字
            StartCoroutine(ButtonTrue());
        if (textline == sequence && base.SelectNum != numans)  //文字があっていなかったらひとつ前のパネルにもどる
            StartCoroutine(DisAnswer());
    }

    IEnumerator ButtonTrue()  //合っていたら次の文字、終わったら通知する
    {
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            audioSource.PlayOneShot(se, 0.2f);
            if (this.textLine == columnNum - 1 && base.SelectNum == trueNum[columnNum - 1])
                isChecknameAnswerPanel = true;  //終わり
            yield return new WaitForSeconds(0.1f);
            this.textLine++;
        }
    }

    IEnumerator DisAnswer()  //回答を間違えた時
    {
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            this.textLine = 0;
            this.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public bool GetisChecknameAnswerPanel()
    {
        return isChecknameAnswerPanel;
    }

}
