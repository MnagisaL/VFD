using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool isSheepStay = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Sheep"))
        {
            isSheepStay = true;
        }
    }
    public bool getisSheepStay() {
        return this.isSheepStay;
    }
}
