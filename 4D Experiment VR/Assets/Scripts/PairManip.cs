using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairManip : MonoBehaviour
{

    public GameObject pairedObject;
    public Vector4[] fourDpair;
    public Vector3[] threeDpair;
    public bool isFourDimensional=false;
    private Vector3 vecStart;

    // Use this for initialization
    void Start()
    {
        vecStart = GetComponent<Transform>().position;
        threeDpair = new Vector3[pairedObject.GetComponent<MeshFilter>().mesh.vertexCount];
        fourDpair = new Vector4[pairedObject.GetComponent<MeshFilter>().mesh.vertexCount];

        StartCoroutine(PairUp());
    }

    // Update is called once per frame
    void Update()
    {



    }
    IEnumerator PairUp()
    {
        while (true)
        {
            for (int i = 0; i < pairedObject.GetComponent<Meshmanip>().fourDmanip.Length; i++)
            {
                fourDpair[i].Set(pairedObject.GetComponent<Meshmanip>().fourDmanip[i].w, pairedObject.GetComponent<Meshmanip>().fourDmanip[i].y, pairedObject.GetComponent<Meshmanip>().fourDmanip[i].z, pairedObject.GetComponent<Meshmanip>().fourDmanip[i].x);
                if (isFourDimensional)
                    threeDpair[i] = 2 * fourDpair[i] / (ControllerInput.lightDist - fourDpair[i].x);
                else
                    threeDpair[i] = fourDpair[i];
            }

            GetComponent<MeshFilter>().mesh.vertices = threeDpair;

            GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, vecStart.y,1.0f+vecStart.z-(Mathf.Pow(ControllerInput.lightDist,2.0f)));

            yield return new WaitForSeconds(.03f);
        }

    }
}