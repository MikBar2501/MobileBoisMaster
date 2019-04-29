using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveOnWhatever : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel(0);
        }
    }
}
