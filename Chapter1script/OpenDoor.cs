using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OpenDoor : MonoBehaviour
{
    private OVRGrabbable keyGrab;

    [SerializeField]
    private GameObject door;

    [SerializeField]
    private float doorMoveLength = -0.926f;  //ドアの移動量

    void Update()
    {
        Reference_Keydata("key");
        Reference_Keydata("key2");
        DoorMoved();
    }

    private bool isFirstDoormoved = false;  //移動させるときtrue
    private bool isSecondDoormoved = false;

    private void Reference_Keydata(string keyName)  //鍵が存在している時に持った時の鍵の情報を得る
    {  //鍵があるとき情報を得る
        if (GameObject.FindGameObjectWithTag(keyName) != null)
        {
            GameObject keyObject = GameObject.FindGameObjectWithTag(keyName);
            keyGrab = keyObject.GetComponent<OVRGrabbable>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && keyGrab.BoolisgrabObj("key"))
            isFirstDoormoved = true;
        if (other.gameObject.CompareTag("Player") && keyGrab.BoolisgrabObj("key2"))
            isSecondDoormoved = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFirstDoormoved = false;
            isSecondDoormoved = false;
        }
    }

    private void DoorMoved()  //ドアを移動する
    {
        if (isFirstDoormoved)
        {
            if ((OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch)))
            {
                door.transform.DOLocalMoveX(doorMoveLength, 1.0f).SetRelative();
                Destroy(this.gameObject);
            }
        }
        if (isSecondDoormoved)
        {
            if ((OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch)))
            {
                door.transform.DOLocalMoveX(doorMoveLength, 1.0f).SetRelative();
                Destroy(this.gameObject);
            }
        }
    }

}
