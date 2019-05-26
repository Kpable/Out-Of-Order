using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;
using Cinemachine;

using DG.Tweening;

public class OutOfOrderSign : MonoBehaviour
{
    public GameObject keyboardGameObject, controllerGameObject;
    public CinemachineVirtualCamera vCam;
    public Transform PlayerObject;
    private int playerId = 0;
    private Player player; // The Rewired Player
    private Rigidbody2D body;
    private GameObject activeObject;
    private bool signHidden;
    private Vector3 originalPosition;
    private bool gameEnded;


    // Start is called before the first frame update
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        body = GetComponent<Rigidbody2D>();
        ReInput.ControllerConnectedEvent += HandleControllerConnect;
        ReInput.ControllerDisconnectedEvent += HandleControllerDisconnect;
        keyboardGameObject.SetActive(ReInput.controllers.Joysticks.Count == 0);
        controllerGameObject.SetActive(ReInput.controllers.Joysticks.Count != 0);
        activeObject = keyboardGameObject.activeInHierarchy ? keyboardGameObject : controllerGameObject;

        vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        if (vCam == null) Debug.LogError("No Cam found");

        PlayerObject = GameObject.Find("Player").transform;

        originalPosition = transform.position;
    }

    private void HandleControllerConnect(ControllerStatusChangedEventArgs obj)
    {
        Debug.Log("ControllerConnect: " + obj.controllerType);

        keyboardGameObject.SetActive(obj.controllerType != ControllerType.Joystick);
        controllerGameObject.SetActive(obj.controllerType == ControllerType.Joystick);

        activeObject = keyboardGameObject.activeInHierarchy ? keyboardGameObject : controllerGameObject;
    }

    private void HandleControllerDisconnect(ControllerStatusChangedEventArgs obj)
    {
        Debug.Log("ControllerConnect: " + obj.controllerType);

        keyboardGameObject.SetActive(obj.controllerType == ControllerType.Joystick);
        controllerGameObject.SetActive(obj.controllerType != ControllerType.Joystick);

        activeObject = keyboardGameObject.activeInHierarchy ? keyboardGameObject : controllerGameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButtonDown("Start") && !gameEnded)
        {
            body.simulated = true;
        }

        if ((vCam.transform.position.y == PlayerObject.position.y ||
            vCam.transform.position.y < PlayerObject.position.y) && !signHidden) 
        {
            Invoke("HideSign", 3f);
            vCam.Follow = PlayerObject;
        }

        if(player.GetButtonDown("Fail"))
        {
            Fail();
        }
    }

    private void HideSign()
    {
        body.simulated = false;
        activeObject.SetActive(false);
        signHidden = true;
        CancelInvoke("HideSign");
    }

    public void Fail()
    {
        body.simulated = false;
        body.velocity = Vector2.zero;
        transform.position = originalPosition;
        transform.DOScale(1f, 1f).From(5);
        vCam.Follow = transform;
        activeObject.SetActive(true);
        gameEnded = true;
        SceneTransitioner.Instance.RestartScene();
    }
}
