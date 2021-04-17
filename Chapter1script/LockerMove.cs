using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerMove : MonoBehaviour
{
    private bool isLockerOpenOnce = false; //ロッカーが一度動いたか判定

    [SerializeField]
    private Vector3 vector3LockerRotate = new Vector3(0, -90, 0);

    private void OnTriggerEnter(Collider other)  //ロッカーに手が触れたらロッカーが開く
    {
        if (isLockerOpenOnce) return;
        if (other.gameObject.CompareTag("Lhand") || other.gameObject.CompareTag("Rhand"))
        {
            GameObject[] getLocker = GameObject.FindGameObjectsWithTag("Locker");

            foreach (GameObject gameObj in getLocker)
            {
                gameObj.transform.DOLocalRotate(vector3LockerRotate, 1.0f).SetRelative();
            }
            isLockerOpenOnce = true;
        }
    }

}
