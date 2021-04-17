using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectNumber_Collider : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController playerController;

    [SerializeField]
    private GameObject canvas;

    private void OnTriggerStay(Collider other)  //動きを止め次のオブジェクトを映す　
    {
        if (other.gameObject.CompareTag("Player") && (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch)))
        {
            playerController.Acceleration = 0;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canvas.SetActive(false);
    }

}
