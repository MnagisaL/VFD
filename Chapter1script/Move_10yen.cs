using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Move_10yen : MonoBehaviour
{
    [SerializeField]
    private RebaMoved rebaMoved;

    [SerializeField]
    private Vector3[] vector3_10yen;  //オブジェクトの移動量
    private float moveTime_10yen = 1.5f;  //オブジェクトの移動時間

    private bool isMovedYenOnce = false;

    void Update()
    {
        Seq10yen();
    }

    private void Seq10yen()//順番どおり10円を動かす。
    {
        if (isMovedYenOnce) return;
        if (rebaMoved.GetisRebaMoved())
        {
            Sequence seq_10 = DOTween.Sequence();
            seq_10.SetLoops(-1);
            seq_10.Append(this.transform.DOLocalMove(vector3_10yen[0], moveTime_10yen).SetRelative());
            seq_10.AppendInterval(0.2f);
            seq_10.Append(this.transform.DOLocalMove(vector3_10yen[1], moveTime_10yen).SetRelative());
            seq_10.AppendInterval(0.3f);
            seq_10.Append(this.transform.DOLocalMove(vector3_10yen[2], moveTime_10yen).SetRelative());
            seq_10.AppendInterval(0.3f);
            seq_10.Append(this.transform.DOLocalMove(vector3_10yen[3], moveTime_10yen).SetRelative());
            seq_10.AppendInterval(0.3f);
            seq_10.Append(this.transform.DOLocalMove(vector3_10yen[4], moveTime_10yen).SetRelative());
            seq_10.AppendInterval(0.3f);
            seq_10.Append(this.transform.DOLocalMove(vector3_10yen[5], moveTime_10yen).SetRelative());
            seq_10.AppendInterval(0.3f);
            seq_10.Append(this.transform.DOLocalMove(vector3_10yen[vector3_10yen.Length - 1], moveTime_10yen).SetRelative());
            seq_10.AppendInterval(0.7f);
            seq_10.Play();
            isMovedYenOnce = true;
        }
    }

}
