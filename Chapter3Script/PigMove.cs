using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * 触ったら触られたオブジェクトが動く
 */

public class PigMove : MonoBehaviour
{
    [SerializeField]
    private GameObject pigObject;

    [SerializeField]
    private Vector3[] transVect3Pig;  //豚の動き
    [SerializeField]
    private Vector3[] rotateVect3Pig;  //豚の回転

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip se;
    private const string AUDIO_TAGSNAME = "Sound";

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
    }

    private int moveCount = 0;

    private void OnTriggerEnter(Collider other)  //手が中に入ったら豚を動かす,触ったら一定期間触れないようにする。
    {
        if (other.gameObject.CompareTag("Lhand") || other.gameObject.CompareTag("Rhand"))
            StartCoroutine(WaitPigMove(1.0f));
    }

    [SerializeField]
    private GameObject newPigObject;

    IEnumerator WaitPigMove(float delay)  //触れた後一定の期間触れないようにする
    {
        moveCount++;
        if (moveCount > transVect3Pig.Length - 1)
        {
            Destroy(this.gameObject);
            Instantiate(
                newPigObject,
                pigObject.transform.position,
                Quaternion.Euler(0, 180, 0)
                );
            Destroy(pigObject);
        }
        MovePig(moveCount);
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(delay);
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    private void MovePig(int countMoveNum)  //豚の動き
    {
        if (moveCount <= transVect3Pig.Length - 1)
        {
            audioSource.PlayOneShot(se, 0.2f);
            pigObject.transform.DOLocalRotate(rotateVect3Pig[countMoveNum], 0.2f).SetRelative();
            pigObject.transform.DOLocalMove(transVect3Pig[countMoveNum], 1.0f).SetRelative();
        }
    }

}
