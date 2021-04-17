using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KatanaBoxChange : MonoBehaviour
{
    [SerializeField]
    private Material boxColor;

    [SerializeField]
    private AudioClip[] se;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)  //刀がヒットした時ランダムで音が鳴る
    {
        if (collision.gameObject.CompareTag("katana"))
        {
            this.gameObject.GetComponent<Renderer>().material = boxColor;
            audioSource.PlayOneShot(se[Random.Range(0,se.Length)]);
        }
    }

}
