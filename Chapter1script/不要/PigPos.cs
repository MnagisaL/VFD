using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isPigStay);
    }

    private bool isPigStay = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pig"))
        {
            isPigStay = true;
        }
    }
    public bool getisPigStay()
    {
        return this.isPigStay;
    }
}
