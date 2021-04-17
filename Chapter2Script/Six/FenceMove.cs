using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FenceMove : MonoBehaviour
{
    [FormerlySerializedAs("sakuLeft")]
    [SerializeField]
    private GameObject fenceLeft;
    [FormerlySerializedAs("sakuRight")]
    [SerializeField]
    private GameObject fenceRight;

    [SerializeField]//フェンスのオーディオソースを得る
    private AudioSource[] audioSource;
    [FormerlySerializedAs("Se")]
    [SerializeField]
    private AudioClip se;

    [SerializeField]
    private float fenceMovelength = -3.93f;  //フェンスが下りる長さ

    void Update()
    {
        SakuDown();
    }

    private bool isCheckFenceDown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) isCheckFenceDown = true;
    }

    [FormerlySerializedAs("sakuTrap")]
    [SerializeField]
    private GameObject[] fenceTrap;

    private void SakuDown()  //Trigerに入ったら2つの柵をおろす
    {
        if (!isCheckFenceDown) return;
        audioSource[0].PlayOneShot(se);
        audioSource[1].PlayOneShot(se);
        fenceLeft.transform.DOLocalMoveY(fenceMovelength, 3f).SetRelative();
        fenceRight.transform.DOLocalMoveY(fenceMovelength, 3f).SetRelative();
        Destroy(fenceTrap[0]);
        Destroy(fenceTrap[1]);
    }

}
