using UnityEngine;
using UnityEngine.Serialization;

/*
 * 常にメインカメラに向けたいものに使う
 */

public class LookAtCanvas : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera = null;

    private void Update()
    {
        transform.LookAt(mainCamera);
    }

}
