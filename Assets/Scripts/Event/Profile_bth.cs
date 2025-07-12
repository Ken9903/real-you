using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile_bth : MonoBehaviour
{
    public GameObject profile;
    public bool profileOn = false;
    public void onClick()
    {
        if(!profileOn)
        {
            profile.SetActive(true);
            profileOn = true;
        }
        else
        {
            profile.SetActive(false);
            profileOn = false;
        }
    }

}
