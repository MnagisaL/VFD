using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smokepre : MonoBehaviour
{

    void OnParticleCollision(GameObject obj)
    {
        if (obj.gameObject.CompareTag("fire")) {
            obj.SetActive(false);
        }
    }
}
    
