using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfGravitate : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {




        if (ArrayCreate.arrayGravAct == true)
        {
            Vector3 myCOM = GetComponent<Rigidbody>().worldCenterOfMass;
            float myMass = GetComponent<Rigidbody>().mass;
            GetComponent<Rigidbody>().AddForce(((ArrayCreate.gravityConstant * myMass * myMass) / (ArrayCreate.arraySizeN * ArrayCreate.arraySizeN * Mathf.Pow(Vector3.Magnitude(ArrayCreate.arrayCOM - myCOM),2))) * (ArrayCreate.arrayCOM - myCOM));
        }
    }
}
