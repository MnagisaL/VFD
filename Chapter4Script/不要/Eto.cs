using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eto : MonoBehaviour
{
    [SerializeField]
    private string Eto_name;
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
        
    }
    private bool etos;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Eto_name)&&!etos) {
            etos = true;
            audioSource.PlayOneShot(Se);
        }
    }
    public bool Get_etos() {
        return etos;
    }
}
