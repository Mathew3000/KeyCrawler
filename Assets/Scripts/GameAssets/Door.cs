﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class Door : MonoBehaviour
    {
        public enum DoorSide
        {
            Left,
            Right,
            Top,
            Bottom
        }
        public Collider DoorCloseBlock;

        const float fSideMove = 5;
        const float fTopMove = 3;

        GameLogicManager gameLogicManager;

        Vector3 CamMove;
        Vector3 PlayerMove;

        public DoorSide side;

        private void Start()
        {
            CloseDoor();
            gameLogicManager = GameObject.Find("ManagerContainer").GetComponent<GameLogicManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                switch (side)
                {
                    case DoorSide.Left:
                        CamMove = new Vector3(-13,0,0);
                        PlayerMove = new Vector3(-3, 0, 0);
                        break;
                    case DoorSide.Right:
                        CamMove = new Vector3(13, 0, 0);
                        PlayerMove = new Vector3(3, 0, 0);
                        break;
                    case DoorSide.Top:
                        CamMove = new Vector3(0,0,13);
                        PlayerMove = new Vector3(0,0,3);
                        break;
                    case DoorSide.Bottom:
                        CamMove = new Vector3(0,0,-13);
                        PlayerMove = new Vector3(0,0,-3);
                        break;
                }
                
                other.GetComponent<CharacterController>().enabled = false;
                other.gameObject.transform.position = other.gameObject.transform.position + PlayerMove;
                other.GetComponent<CharacterController>().enabled = true;
                GameObject.Find("Main Camera").transform.position = GameObject.Find("Main Camera").transform.position + CamMove;

            }
        }

        private void Update()
        {
            if (gameLogicManager.IsClear && !DoorCloseBlock.isTrigger)
            {
                OpenDoor();
            }
        }

        void CloseDoor()
        {
            DoorCloseBlock.isTrigger = false;
        }

        void OpenDoor()
        {
            DoorCloseBlock.isTrigger = true;
        }



    }
}
