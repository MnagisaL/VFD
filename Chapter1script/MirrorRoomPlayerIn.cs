using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorRoomPlayerIn : MonoBehaviour
{
    [SerializeField]
    private Chapter1_Gamesystem chapter1_Gamesystem;

    [SerializeField]
    private GameObject keySecondTrans;  //2つ目の鍵を出す場所
    [SerializeField]
    private GameObject keySecond;  //2つ目の鍵のオブジェクト
    [SerializeField]
    private GameObject doorSpotSecond;  //鍵が開けれるスポット

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip se;
    private const string AUDIO_TAGSNAME = "Sound";

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
    }

    private bool isMakedKeyOnce = false;  //赤西が部屋に入り、プレイヤーが適切な位置に入ったとき鍵を作成

    private void OnTriggerStay(Collider other)  //プレイヤーが入ったら鍵生成、スポット生成
    {
        if (other.gameObject.CompareTag("Player") && chapter1_Gamesystem.GetisMirrorPlayerStay() && !isMakedKeyOnce)
        {
            audioSource.PlayOneShot(se);
            Instantiate(
                keySecond,
                keySecondTrans.gameObject.transform.position,
                Quaternion.identity
                );
            doorSpotSecond.SetActive(true);
            isMakedKeyOnce = true;
        }
    }

}
