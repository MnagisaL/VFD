using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BasketSound : MonoBehaviour
{
    //バスケの時に出す音
    [SerializeField]
    private AudioClip drible;
    [SerializeField]
    private AudioClip shuto;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [SerializeField]
    private SelectBasketPlay selectBasketPlay;

    public void MoveandSound()  //動くオブジェクトに立体音響を付け、立体音響を聴いてるようにする。
    {
        Sequence sounds = DOTween.Sequence();
        sounds.Append(this.transform.DOLocalMoveX(5, 2f).SetRelative());
        sounds.AppendInterval(2.0f);
        sounds.Append(this.transform.DOLocalMoveX(-10, 7f).SetRelative());
        sounds.AppendInterval(2.0f);
        sounds.AppendInterval(2.0f);
        sounds.Append(this.transform.DOLocalMoveX(5, 3.5f).SetRelative());
        sounds.Append(this.transform.DOLocalMoveX(-5, 3.5f).SetRelative());
        sounds.AppendInterval(2.0f);
        sounds.Append(this.transform.DOLocalMoveX(10, 7f).SetRelative());
        sounds.AppendInterval(2.0f);
        sounds.AppendInterval(2.0f);
        sounds.Append(this.transform.DOLocalMoveX(-10, 7f).SetRelative());
        sounds.AppendInterval(2.0f);
        sounds.AppendInterval(2.0f);
        sounds.Append(this.transform.DOLocalMoveX(5, 3.5f).SetRelative());
        sounds.Play();
        Invoke("PlaySeShuto" , 2.0f);
        Invoke("PlaySeDrible", 4.0f);
        Invoke("PlaySeShuto" , 11.0f);
        Invoke("PlaySeShuto" , 13.0f);
        Invoke("PlaySeDrible", 15.0f);
        Invoke("PlaySeShuto" , 22.0f);
        Invoke("PlaySeDrible", 24.0f);
        Invoke("PlaySeShuto" , 31.0f);
        Invoke("PlaySeShuto" , 33.0f);
        Invoke("PlaySeDrible", 35.0f);
        Invoke("PlaySeShuto" , 42.0f);
        Invoke("PlaySeShuto" , 44.0f);
    }

    private void PlaySeDrible()
    {
        audioSource.PlayOneShot(drible);
    }

    private void PlaySeShuto()
    {
        audioSource.PlayOneShot(shuto);
    }

}
