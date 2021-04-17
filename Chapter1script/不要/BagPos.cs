using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isBagStay);
    }
    private bool isBagStay = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bag")) {
            isBagStay = true;
        }
    }
    public bool getisBagStay()
    {
        return this.isBagStay;
    }
}
