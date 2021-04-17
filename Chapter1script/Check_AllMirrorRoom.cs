using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Check_AllMirrorRoom : MonoBehaviour
{
    [SerializeField]
    private CheckHitObject[] checkHitObject;

    private bool CheckisObjectStay()  //すべて所定の位置にあったらtrue
    {
        bool isCheckHitAllObject = false;
        if (checkHitObject[0].GetisObjctStay() && checkHitObject[1].GetisObjctStay() && checkHitObject[2].GetisObjctStay())
        {
            isCheckHitAllObject = true;
        }
        return isCheckHitAllObject;
    }

    public bool GetisObjectStay()
    {
        return CheckisObjectStay();
    }

}
