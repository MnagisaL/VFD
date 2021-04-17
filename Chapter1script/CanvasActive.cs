using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Playerが特定のColiderに入ったらCanvasを映す
 */

public class CanvasActive : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    private bool isCheckInThisCollider = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canvas != null)
            {
                canvas.SetActive(true);
                isCheckInThisCollider = true;
            }
            else isCheckInThisCollider = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
                isCheckInThisCollider = false;
            }
            else isCheckInThisCollider = false;
        }
    }

    public bool GetisCheckInThisCollider()  //プレイヤーが入ったら通知。
    {
        return isCheckInThisCollider;
    }

}
