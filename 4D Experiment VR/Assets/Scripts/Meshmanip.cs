using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshmanip : MonoBehaviour
{

    public float wStartValue = 0;
    public bool wStartSet = true;
    public Vector4[] fourDmanip;
    public Vector3[] threeDmanip;
    public float rotRate = 0.1f;

    private Mesh mesh;
    private bool toggle;

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        fourDmanip = new Vector4[mesh.vertexCount];
        threeDmanip = new Vector3[mesh.vertexCount];



        for (int i = 0; i < mesh.vertexCount; i++)
        {
            fourDmanip[i] = mesh.vertices[i];
            if (wStartSet == true)
                fourDmanip[i].w = wStartValue;
            else
            {
                if (fourDmanip[i].z < 0.4 && fourDmanip[i].z > -0.4)
                    fourDmanip[i].w = 0;
                else
                    fourDmanip[i].w = 0.5f;
            }
            threeDmanip[i] = 2 * fourDmanip[i] / (ControllerInput.lightDist - fourDmanip[i].w);
        }


        GetComponent<MeshFilter>().mesh.vertices = threeDmanip;
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
        StartCoroutine(RotateFourD());


    }

    // Update is called once per frame
    void Update()
    {

        /*if (Raycast.hitDetect.collider == GetComponent<Collider>() && toggle == false)
        {
            StartCoroutine(RotateFourD());
            toggle = true;
        }
        else if (Raycast.hitDetect.collider != GetComponent<Collider>() && toggle == true)
        {
            StopCoroutine(RotateFourD());
            toggle = false;
        }*/



    }

    IEnumerator RotateFourD()
    {
        while (true)
        {



            for (int i = 0; i < mesh.vertexCount; i++)
            {


                if (Raycast.hitDetect.collider == GetComponent<Collider>() && toggle == false)
                {
                    //3D Rotations
                    if (Input.GetKey(KeyCode.Z) || OVRInput.Get(OVRInput.Button.DpadUp))
                        fourDmanip[i] = MeshRotFourYZ(fourDmanip[i], rotRate);
                    else if (Input.GetKey(KeyCode.X) || OVRInput.Get(OVRInput.Button.DpadDown))
                        fourDmanip[i] = MeshRotFourYZ(fourDmanip[i], -rotRate);

                    if (Input.GetKey(KeyCode.C) || OVRInput.Get(OVRInput.Button.DpadLeft))
                        fourDmanip[i] = MeshRotFourXZ(fourDmanip[i], rotRate);
                    else if (Input.GetKey(KeyCode.V) || OVRInput.Get(OVRInput.Button.DpadRight))
                        fourDmanip[i] = MeshRotFourXZ(fourDmanip[i], -rotRate);

                    if (Input.GetKey(KeyCode.F) || OVRInput.Get(OVRInput.Button.Four, OVRInput.Controller.Gamepad))
                        fourDmanip[i] = MeshRotFourXY(fourDmanip[i], rotRate);
                    else if (Input.GetKey(KeyCode.G) || OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Gamepad))
                        fourDmanip[i] = MeshRotFourXY(fourDmanip[i], -rotRate);

                    //4D Rotations
                    if (Input.GetKey(KeyCode.N))
                        fourDmanip[i] = MeshRotFourZW(fourDmanip[i], rotRate);
                    else if (Input.GetKey(KeyCode.M))
                        fourDmanip[i] = MeshRotFourZW(fourDmanip[i], -rotRate);

                    if (ControllerInput.joystickPos.x != 0.0f)
                        fourDmanip[i] = MeshRotFourXW(fourDmanip[i], rotRate * ControllerInput.joystickPos.x);

                    if (ControllerInput.joystickPos.y != 0.0f)
                        fourDmanip[i] = MeshRotFourYW(fourDmanip[i], rotRate * ControllerInput.joystickPos.y);

                    if (Input.GetKey(KeyCode.H))
                        fourDmanip[i] = MeshRotFourYW(fourDmanip[i], rotRate);
                    else if (Input.GetKey(KeyCode.J))
                        fourDmanip[i] = MeshRotFourYW(fourDmanip[i], -rotRate);

                    if (Input.GetKey(KeyCode.Y) || OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.Gamepad))
                        fourDmanip[i] = MeshRotFourZW(fourDmanip[i], rotRate);
                    else if (Input.GetKey(KeyCode.U) || OVRInput.Get(OVRInput.Button.Three, OVRInput.Controller.Gamepad))
                        fourDmanip[i] = MeshRotFourZW(fourDmanip[i], -rotRate);

                }

                threeDmanip[i] = 2 * fourDmanip[i] / (ControllerInput.lightDist - fourDmanip[i].w);
            }


            GetComponent<MeshFilter>().mesh.vertices = threeDmanip;
            GetComponent<MeshFilter>().mesh.RecalculateNormals();
            if (GetComponent<MeshCollider>() != null)
                GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;

            yield return new WaitForSeconds(.03f);
        }
    }

    //rotate about plane specified
    public Vector4 MeshRotFourXY(Vector4 i, float angle)
    {
        i.Set((i.x * Mathf.Cos(angle)) - (i.y * Mathf.Sin(angle)), (i.x * Mathf.Sin(angle)) + (i.y * Mathf.Cos(angle)), i.z, i.w);
        return i;
    }

    public Vector4 MeshRotFourXZ(Vector4 i, float angle)
    {
        i.Set((i.z * Mathf.Sin(angle)) + (i.x * Mathf.Cos(angle)), i.y, (i.z * Mathf.Cos(angle)) - (i.x * Mathf.Sin(angle)), i.w);
        return i;
    }

    public Vector4 MeshRotFourYZ(Vector4 i, float angle)
    {
        i.Set(i.x, (i.y * Mathf.Cos(angle)) - (i.z * Mathf.Sin(angle)), (i.y * Mathf.Sin(angle)) + (i.z * Mathf.Cos(angle)), i.w);
        return i;
    }

    public Vector4 MeshRotFourXW(Vector4 i, float angle)
    {
        i.Set((i.w * Mathf.Sin(angle)) + (i.x * Mathf.Cos(angle)), i.y, i.z, (i.w * Mathf.Cos(angle)) - (i.x * Mathf.Sin(angle)));
        return i;
    }

    public Vector4 MeshRotFourYW(Vector4 i, float angle)
    {
        i.Set(i.x, (i.w * Mathf.Sin(angle)) + (i.y * Mathf.Cos(angle)), i.z, (i.w * Mathf.Cos(angle)) - (i.y * Mathf.Sin(angle)));
        return i;
    }

    public Vector4 MeshRotFourZW(Vector4 i, float angle)
    {
        i.Set(i.x, i.y, (i.z * Mathf.Cos(angle)) - (i.w * Mathf.Sin(angle)), (i.z * Mathf.Sin(angle)) + (i.w * Mathf.Cos(angle)));
        return i;
    }




}
