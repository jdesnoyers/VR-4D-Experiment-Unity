using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayCreate : MonoBehaviour {

    [SerializeField] private Rigidbody arrayOriginal;
    [SerializeField] private Transform arrayParent;

    public int arraySizeX; //number of instances in X
    public int arraySizeY; //number of instances in Y
    public int arraySizeZ; //number of instances in Z
    public int arraySizeW;  //number of instances in W
    public float arrayOffset = -1.0f;
    public float projectionDistance = 2.0f;
    public float arraySpacing; //spacing between instances in all directions
    public static bool arrayGravAct = false; //boolean to activate/deactivate local gravity
    public float gravityConstantSet = 6.674E-11f;
    public static float gravityConstant = 6.674E-11f;
    public Vector4[] arrayFourDStore = null;

    [HideInInspector] public static float totalMass; //stores total mass of array
    [HideInInspector] public static int arraySizeN; //stores total size of array
    [HideInInspector] public static GameObject[] arrayObjects = null; //array of objets with tag "arrayObject"
    [HideInInspector] public static Vector3 arrayCOM; //array center of mass

    // Use this for initialization
    void Start()
    {

        arrayFourDStore = new Vector4[arraySizeX*arraySizeY*arraySizeZ* arraySizeW];

        int arrayIndex = 0;

        for (int i = 0; i < arraySizeX; i++)
        {
            for (int j = 0; j < arraySizeY; j++)
            {
                for (int k = 0; k < arraySizeZ; k++)
                {
                    for(int l = 0; l < arraySizeW; l++)
                    {
                        arrayFourDStore[arrayIndex].Set((i*arraySpacing)+arrayOffset,(j*arraySpacing) + arrayOffset,(k * arraySpacing) + arrayOffset,(l * arraySpacing) + arrayOffset);
                        Instantiate(arrayOriginal,arrayParent.position + (Vector3) arrayFourDStore[arrayIndex], arrayOriginal.transform.rotation, arrayParent);
                        arrayIndex++;
                    }


                }

            }
        }

        //define array of objects
        arrayObjects = GameObject.FindGameObjectsWithTag("arrayObject");

        //calculate total mass
        foreach (GameObject arrayObject in arrayObjects)
            totalMass += arrayObject.GetComponent<Rigidbody>().mass;

        arraySizeN = arrayObjects.Length;
        gravityConstant = gravityConstantSet;

        StartCoroutine(RotateInXY());


        /*for (int i = 0; i < arrayObjects.Length; i++)
        {
            arrayFourDStore[i] = arrayObjects[i].GetComponent<Transform>().localPosition;


        }*/

    }

    // Update is called once per frame
    void Update () {

        


        /*
        Vector3 sumCOM = Vector3.zero; //create/reset vector3

        // calculate center of mass
        foreach (GameObject arrayObject in arrayObjects)
        {
            
            sumCOM += arrayObject.GetComponent<Rigidbody>().worldCenterOfMass;
        }

        arrayCOM = sumCOM / arraySizeN; //calculate average
        */
    }

    IEnumerator RotateInXY()
    {
        while (true)
        {
            for (int i = 0; i < arrayFourDStore.Length; i++)
            {
                arrayFourDStore[i] = RotateFourXY(arrayFourDStore[i], 0.1f);
                //arrayFourDStore[i] = RotateFourXZ(arrayFourDStore[i], 0.01f);
                //arrayFourDStore[i] = RotateFourWZ(arrayFourDStore[i], 0.01f);
                arrayObjects[i].GetComponent<Transform>().localPosition = 2*arrayFourDStore[i] / (projectionDistance - arrayFourDStore[i].w);
            }

            yield return new WaitForSeconds(.03f);
        }
    }

    public Vector4 RotateFourXY(Vector4 i,float angle)
    {
        i.Set(i.x, i.y, (i.z * Mathf.Cos(angle)) - (i.w * Mathf.Sin(angle)), (i.z * Mathf.Sin(angle)) + (i.w * Mathf.Cos(angle)));
        return i;
    }

    public Vector4 RotateFourXZ(Vector4 i, float angle)
    {
        i.Set(i.x, (i.w * Mathf.Sin(angle)) + (i.y * Mathf.Cos(angle)), i.z, (i.w * Mathf.Cos(angle)) - (i.y* Mathf.Sin(angle)));
        return i;
    }

    public Vector4 RotateFourWZ(Vector4 i, float angle)
    {
        i.Set((i.z * Mathf.Sin(angle)) + (i.x * Mathf.Cos(angle)), i.y, (i.z * Mathf.Cos(angle)) - (i.x * Mathf.Sin(angle)),i.w);
        return i;
    }
}


