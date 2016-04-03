﻿using UnityEngine;
using System.Collections;
using DFHack;

public class CameraMovement : MonoBehaviour
{
    GameMap gameMap;
    public Transform cameraPos;
    public bool following = true;
    public float followSpeed = 0.5f;
    public float followSnapDistance = 10;
    public float speed = 10;
    public float minDistance = 5;
    public float fasterMultiplier = 10;

    void Awake()
    {
        gameMap = FindObjectOfType<GameMap>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float moveZ = Input.GetAxis("CamUpDown");
        float moveX = Input.GetAxis("CamLeftRight");
        float moveY = Input.GetAxis("CamFrontBack");

        if (following && gameMap != null)
        {
            Vector3 goal = GameMap.DFtoUnityTileCenter(new DFCoord(gameMap.PosXTile, gameMap.PosYTile, gameMap.PosZ - 1));
            Vector3 diff = goal - transform.position;
            if (diff.sqrMagnitude > followSnapDistance * followSnapDistance)
            {
                transform.position = goal;
            }
            else
            {
                float interp = Time.deltaTime * diff.magnitude * followSpeed;
                if (interp > 1)
                {
                    interp = 1;
                }
                transform.Translate(diff * interp, Space.World);
            }
        }

        if (moveZ != 0.0f || moveY != 0.0f || moveX != 0.0f)
        {
            following = false;
            float cameraDistance = cameraPos.localPosition.z * -1.0f;
            if (cameraDistance < minDistance)
                cameraDistance = minDistance;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                cameraDistance *= fasterMultiplier;
            Vector3 movement = new Vector3(moveX, moveZ, moveY);
            transform.Translate(movement * Time.deltaTime * speed * cameraDistance, Space.Self);
            if (gameMap != null)
                gameMap.UpdateCenter(transform.position);
        }
    }
}
