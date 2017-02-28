using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

    public static Ray lookDetect;
    public static RaycastHit hitDetect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray lookDetect = new Ray(GetComponent<Transform>().position, GetComponent<Transform>().forward);

        Physics.Raycast(lookDetect, out hitDetect);

        Debug.DrawRay(lookDetect.origin, lookDetect.direction * 10, Color.blue);
    }
}
