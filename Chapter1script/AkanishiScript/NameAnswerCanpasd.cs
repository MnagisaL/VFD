using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameAnswerCanpasd : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    private bool isCheckHitPlayer;

    void Update()
    {
        CanvsaDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) isCheckHitPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) isCheckHitPlayer = false;
    }

    private void CanvsaDisplay()
    {
        if (isCheckHitPlayer)
        {
            if (canvas != null)
                canvas.SetActive(true);
        }
        if (!isCheckHitPlayer)
        {
            if (canvas != null)
                canvas.SetActive(false);
        }
    }

}
