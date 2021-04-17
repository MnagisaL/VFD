using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sindou2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Object;
    [SerializeField]
    private Transform T;

  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Make();
        this.vib();
      
    }
    private bool vibration;
    private bool Abutton;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (OVRInput.GetUp(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
            {
                this.Abutton = true;
            }
            if (!Abutton)
            {
                this.vibration = true;
            }
            if (Abutton)
            {
                this.vibration = false;
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.vibration = false;
        }
    }
    private void vib()
    {
        if (this.vibration)
        {
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
        }
        if (!this.vibration)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
    private bool One;
    private void Make()
    {
        if (!One)
        {
            if (Abutton)
            {
               
                Instantiate(
                    Object,
                    T.transform.position
                    , Quaternion.identity
                    );
                One = true;
            }
        }
    }

}
