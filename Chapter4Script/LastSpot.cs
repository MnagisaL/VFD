using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LastSpot : MonoBehaviour
{
    private OVRGrabbable lastKeyGrab;

    [SerializeField]
    private GameObject door;
    bool isKeyGetOnce = false;

    [SerializeField]
    private float doorMoveLengthX = -0.972f;

    void Update()
    {
        KeyGet("last_key");
        LastDoor();
    }

    private void KeyGet(string Key_name)  //持っている鍵のOVRGrabbable取得
    {
        if (isKeyGetOnce) return;
        if (GameObject.FindGameObjectWithTag(Key_name) != null)
        {
            GameObject keyOb = GameObject.FindGameObjectWithTag(Key_name);
            lastKeyGrab = keyOb.GetComponent<OVRGrabbable>();
            isKeyGetOnce = false;
        }
    }

    private bool isCheckOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && lastKeyGrab.BoolisgrabObj("last_key"))
            isCheckOpen = true;
    }

    private bool isDoorMoveOnce = false;

    private void LastDoor()
    {
        if (isDoorMoveOnce) return;
        if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch) && isCheckOpen)
            door.transform.DOLocalMoveX(doorMoveLengthX, 1.0f).SetRelative();
    }

}
