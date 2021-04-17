using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool switch_on;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lhand") || other.gameObject.CompareTag("Rhand"))
        {
                    this.transform.DOLocalRotate(new Vector3(-106, 0, 0), 1.0f).SetRelative();
            switch_on = true;
        }
    }
    public bool get_switch_On() {
        return switch_on;
    }
}
