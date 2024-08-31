using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ModeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If OpenVR system isn't available, then just assume the user is present
        if (OpenVR.System == null)
        {
            return;
        }

        EDeviceActivityLevel level = OpenVR.System.GetTrackedDeviceActivityLevel(0);
        if(level == EDeviceActivityLevel.k_EDeviceActivityLevel_Standby) 
        {
            Debug.Log("Heaedset off");
        }        


        
    }
}
