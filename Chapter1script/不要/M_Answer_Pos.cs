using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Answer_Pos : MonoBehaviour
{
    [SerializeField]
    private string Tagsname;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip Se;
    private const string Audio_TagsName = "Sound";
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(Audio_TagsName).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        se();
    }
    private bool isObjectStay = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tagsname))
        {
            isObjectStay = true;
        }
    }
    private bool Se_Once;
    private void se()
    {
        if (!Se_Once && isObjectStay)
        {
            audioSource.PlayOneShot(Se);
            Se_Once = true;
        }
    }
    public bool GetisObjStay()
    {
        return this.isObjectStay;
    }
}
