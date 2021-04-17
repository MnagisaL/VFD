using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLastRoom : MonoBehaviour
{
    private bool isCheckLastRoomHit = false;

    private void OnTriggerEnter(Collider other)  //部屋に入ってこのトリガーに当たったら通知
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isCheckLastRoomHit) isCheckLastRoomHit = true;
        }
    }

    public bool GetisCheckLastRoomHit()
    {
        return isCheckLastRoomHit;
    }

}
