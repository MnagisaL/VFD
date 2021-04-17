using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Check_AllTaiikukan : MonoBehaviour
{
    [SerializeField]
    private CheckHitObject[] checkHitObject;

    private bool CheckAllObjectStay()  //たいいく館の文字が揃う判定　通知させる
    {
        bool isCheckObjectStay = false;
        if (checkHitObject[0].GetisObjctStay() && checkHitObject[1].GetisObjctStay() && checkHitObject[2].GetisObjctStay() && checkHitObject[3].GetisObjctStay())
        {
            isCheckObjectStay = true;
        }
        return isCheckObjectStay;
    }

    public bool GetCheckAllObjectStay()
    {
        return CheckAllObjectStay();
    }

}
