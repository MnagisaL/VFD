using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RebaMoved : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip se;
    private const string AUDIO_TAGSNAME = "Sound";

    [SerializeField]
    private Vector3 vector3RebaRotate = new Vector3(-106, 0, 0);  //レバーの回転

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
    }

    private bool isRebaMoved = false;

    private void OnTriggerEnter(Collider other)  //右手か左手が当たるとレバーが回る
    {
        if (other.gameObject.CompareTag("Lhand") || other.gameObject.CompareTag("Rhand") && !isRebaMoved)
        {
            audioSource.PlayOneShot(se);
            this.transform.DOLocalRotate(vector3RebaRotate, 1.0f).SetRelative();
            isRebaMoved = true;
        }
    }

    public bool GetisRebaMoved()
    {
        return isRebaMoved;
    }

}
