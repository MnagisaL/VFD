using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Check_Taiikukan: MonoBehaviour
{
    [SerializeField]
    private string Tagsname;

    private AudioSource audioSource;
    [FormerlySerializedAs("Se")]
    [SerializeField]
    private AudioClip se;
    private const string Audio_TagsName = "Sound";
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(Audio_TagsName).gameObject.GetComponent<AudioSource>();
    }

    private bool isObjectStay = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tagsname)&&!isObjectStay)
        {
            audioSource.PlayOneShot(se);
            isObjectStay = true;
        }
    }

    public bool GetisObjectStay()
    {
        return this.isObjectStay;
    }

}
