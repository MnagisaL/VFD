using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/*
* trigerの中に入ったらバイブレーション
* さらにAボタンでオブジェクト生成
*/

public class VibrationController : MonoBehaviour
{
    [SerializeField]
    private GameObject purposeObject;

    [SerializeField]
    private Transform vibrationTransform;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip se;
    private const string AUDIO_TAGSNAME = "Sound";

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(AUDIO_TAGSNAME).gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        MakeObject();
    }

    private bool isCheckAbutton = false;

    private void OnTriggerStay(Collider other)  //入ったら振動開始
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
            if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
                isCheckAbutton = true;
        }
    }

    private void OnTriggerExit(Collider other)  //出たら振動中止
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }

    private bool isMakeObjectOnce = false;

    private void MakeObject()  //Aボタンが押されたらバイブを止めてオブジェクト生成
    {
        if (isMakeObjectOnce) return;
        if (isCheckAbutton)
        {
            audioSource.PlayOneShot(se, 0.3f);
            Instantiate(
                purposeObject,
                vibrationTransform.transform.position,
                Quaternion.identity
                );
            isMakeObjectOnce = true;
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            Destroy(this.gameObject);
        }
    }

}
