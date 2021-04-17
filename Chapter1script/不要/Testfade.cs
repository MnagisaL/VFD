using UnityEngine;

public class Testfade : MonoBehaviour
{

    [SerializeField] OVRScreenFade fade;

    void Start()
    {
        
    }

    void Update()
    {
        test();    
    }
    private void test() {
        if (Input.GetKey("z")) {
            fade.FadeOn(0, 1);//fadeout
        }
        if (Input.GetKey("x")) {
            fade.FadeOn(1,0);//fadein
        }
    }
}