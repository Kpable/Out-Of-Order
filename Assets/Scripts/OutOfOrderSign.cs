using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;
using Cinemachine;
public class OutOfOrderSign : MonoBehaviour
{
    public GameObject keyboardGameObject, controllerGameObject;
    public CinemachineVirtualCamera vCam;
    public Transform PlayerObject;
    private int playerId = 0;
    private Player player; // The Rewired Player
    private Rigidbody2D body;



    // Start is called before the first frame update
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        body = GetComponent<Rigidbody2D>();
        ReInput.ControllerConnectedEvent += HandleControllerConnect;
        ReInput.ControllerDisconnectedEvent += HandleControllerDisconnect;
        keyboardGameObject.SetActive(ReInput.controllers.Joysticks.Count == 0);
        controllerGameObject.SetActive(ReInput.controllers.Joysticks.Count != 0);

        vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        if (vCam == null) Debug.LogError("No Cam found");

        PlayerObject = GameObject.Find("Player").transform;
    }

    private void HandleControllerConnect(ControllerStatusChangedEventArgs obj)
    {
        Debug.Log("ControllerConnect: " + obj.controllerType);

        keyboardGameObject.SetActive(obj.controllerType != ControllerType.Joystick);
        controllerGameObject.SetActive(obj.controllerType == ControllerType.Joystick);
        
    }

    private void HandleControllerDisconnect(ControllerStatusChangedEventArgs obj)
    {
        Debug.Log("ControllerConnect: " + obj.controllerType);

        keyboardGameObject.SetActive(obj.controllerType == ControllerType.Joystick);
        controllerGameObject.SetActive(obj.controllerType != ControllerType.Joystick);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButtonDown("Start"))
        {
            body.simulated = true;
        }

        if (transform.position.y == PlayerObject.position.y ||
            transform.position.y < PlayerObject.position.y) 
        {
            Invoke("HideSign", 3f);
            vCam.Follow = PlayerObject;
        }
    }

    private void HideSign()
    {
        body.simulated = false;
        gameObject.SetActive(false);
    }
}
