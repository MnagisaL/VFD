using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
/*
 * オブジェクトがコライダーに当たったら返す
 */
public class CheckHitObject : MonoBehaviour
{
    [SerializeField]
    private string tagsName;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip se;
    private const string AUDIO_TAGSNAME = "Sound";

    private bool isObjectStay = false;  //オブジェクトに当たったらtrue

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(tagsName) && !isObjectStay)
        {
            audioSource.PlayOneShot(se);
            isObjectStay = true;
        }
    }

    public bool GetisObjctStay()
    {
        return this.isObjectStay;
    }

}


