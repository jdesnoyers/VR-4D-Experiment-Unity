using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VR = UnityEngine.VR;

public class ControllerInput : MonoBehaviour
{

    private OVRPlayerController playerController;
    public static Vector2 joystickPos;
    public static float lightDist = 2.0f;

    void Start()
    {

    }

    void Awake()
    {
        //get controller
        OVRPlayerController[] playerControllers;
        playerControllers = gameObject.GetComponentsInChildren<OVRPlayerController>();

        if (playerControllers.Length == 0)
        {
            Debug.LogWarning("OVRMainMenu: No OVRPlayerController attached.");
        }
        else if (playerControllers.Length > 1)
        {
            Debug.LogWarning("OVRMainMenu: More then 1 OVRPlayerController attached.");
        }
        else
        {
            playerController = playerControllers[0];
        }

    }
    // Update is called once per frame
    void Update()
    {

        //restart level
        if (OVRInput.Get(OVRInput.RawButton.Back) || Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        joystickPos = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        //Adjust location of fourth dimensional light
        if (lightDist < 4.0f && (OVRInput.Get(OVRInput.Button.PrimaryShoulder, OVRInput.Controller.Gamepad) || Input.GetKey(KeyCode.Q)))
            lightDist += 0.01f;
        else if (lightDist > 1.01f && (OVRInput.Get(OVRInput.Button.SecondaryShoulder, OVRInput.Controller.Gamepad) || Input.GetKey(KeyCode.E)))
            lightDist -= 0.01f;

        //Jump
        //if (Input.GetKeyDown(KeyCode.) || Input.GetButtonDown("Jump")) playerController.Jump();


    }
}
