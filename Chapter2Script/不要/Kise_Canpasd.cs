using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kise_Canpasd : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    private bool Canvas_Disp;

    void Update()
    {
        Canvsa_Dispaly();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Canvas_Disp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Canvas_Disp = false;
        }
    }

    private void Canvsa_Dispaly()
    {
        if (Canvas_Disp)
        {
            if (canvas != null)
            {
                canvas.SetActive(true);
            }
        }
        if (!Canvas_Disp)
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }
    }

}
