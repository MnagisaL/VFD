using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *キャラクターの体と顔を移動させる
 */

public class CharacterLook : MonoBehaviour
{
    private CanvasActive canvasActive;

    [SerializeField]
    private string colliderTagsName;  //コライダーのあるオブジェクトのタグの名前

    [SerializeField]
    private Transform characterNeck;
    private Transform startPosition;  //最初の位置
    private Transform target;  //プレイヤーの顔の位置

    private float step = 0;

    [SerializeField]
    private float rotateSpeed = 0.003f;  //首の曲がる速度

    Quaternion startNeckQuaternion;  //首の回転位置
    Quaternion startBodyQuaternion;  //体の初期位置

    private void Start()
    {
        startPosition = characterNeck.transform;
        startNeckQuaternion = characterNeck.rotation;
        startBodyQuaternion = this.transform.rotation;
        if (!target) target = Camera.main.transform;  //メインカメラの取得
        if (GameObject.FindGameObjectWithTag(colliderTagsName) != null)
        {
            //キャラクターと同じ親を持ったCanvasActive取得
            canvasActive = this.transform.root.gameObject.GetComponentInChildren<CanvasActive>();  
        }
    }

    private void LateUpdate()
    {
        CharacterNeckLook();
    }

    private void CharacterNeckLook()  //顔の角度、体の角度を変化させる
    {
        if (canvasActive.GetisCheckInThisCollider())  //コライダーの中に入っている時顔がこちらを向く
        {

            step += rotateSpeed * Time.deltaTime;
            characterNeck.rotation = Quaternion.Slerp(startPosition.rotation,
                                     Quaternion.LookRotation((target.transform.position - startPosition.position).normalized),
                                     step);
            if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
            {
                BodyRotate();
            }
        }
        if (!canvasActive.GetisCheckInThisCollider())  //コライダーを出た時初期位置
        {
            step += rotateSpeed * Time.deltaTime;
            characterNeck.rotation = Quaternion.Slerp(characterNeck.rotation, startNeckQuaternion, step);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, startBodyQuaternion, step);
        }
    }

    private void BodyRotate()  //体の向きをy軸固定で向く
    {
        Vector3 CharacterRotate = Camera.main.transform.position;
        CharacterRotate.y = transform.position.y;
        transform.LookAt(CharacterRotate);
    }

}