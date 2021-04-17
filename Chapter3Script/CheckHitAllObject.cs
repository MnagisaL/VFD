using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 複数のオブジェクトの接触判定
 */

public class CheckHitAllObject : MonoBehaviour
{
    private List<GameObject> objectList = new List<GameObject>();  

    [SerializeField]
    private string[] objectName;  //接触判定をしたいタグ

    private void Start()
    {
        SetBoolObjectOnce();
    }

    bool[] hitObjectOnce;

    private void SetBoolObjectOnce()
    {
        hitObjectOnce = new bool[objectName.Length];
    }

    private void OnCollisionStay(Collision collision)  //タグの付いたオブジェクトと接触したら、リストに入れる
    {
        for (int i = 0; i < objectName.Length; i++)
        {
            CheckHitObject(i, objectName[i]);
        }
        void CheckHitObject(int num, string ObjectsName)
        {
            if (hitObjectOnce[num]) return;
            if (collision.gameObject.CompareTag(ObjectsName))
            {
                objectList.Add(collision.gameObject);
                hitObjectOnce[num] = true;
            }
        }
    }

    private bool CheckAllHitObject()//リストの中の数がタグの数と一致したらtrue
    {
        if (objectList.Count == objectName.Length) return true;
        else return false;
    }

    public bool GetCheckAllHitObject()
    {
        return CheckAllHitObject();
    }

}
