using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public bool IsMoon = false;

    public GameManager Manager;
    
    // Update is called once per frame
    void Update()
    {
        if (IsMoon)
        {
            if (Manager.IsDay)
            {
                SetNewRotation(180, 360);
            }
            else
            {
                SetNewRotation(0, 180);
            }
        }
        else
        {
            if (Manager.IsDay)
            {
                GetComponentInChildren<Light>().enabled = true;
                SetNewRotation(0, 180);
            }
            else
            {
                GetComponentInChildren<Light>().enabled = false;
                SetNewRotation(180, 360);
            }
        }


        
    }

    private void SetNewRotation(int min, int max)
    {
        var currRot = transform.rotation.eulerAngles;

        var newX = Mathf.Lerp(min, max, Manager.TimeLeft / Manager.CurrentDuration);

        var newW = Quaternion.Euler(newX, 0, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, newW, 1);

        // transform.SetPositionAndRotation(transform.position, newW);
    }
}
