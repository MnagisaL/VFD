using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquatThrough : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController playerController;

    private MeshCollider meshCollider;

    void Start()
    {
        meshCollider = this.GetComponent<MeshCollider>();
    }

    void Update()
    {
        WallThrough();
    }

    private void WallThrough()  //プレイヤーがしゃがんでいたらメッシュコライダーを消す
    {
        if (!playerController.Getsquate()) meshCollider.enabled = false;
        if (playerController.Getsquate()) meshCollider.enabled = true;
    }

}
