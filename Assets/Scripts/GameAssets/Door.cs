using System.Collections;
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

        const float SideMove = 5;
        const float TopMove = 3;

        Vector3 CamMove;
        Vector3 PlayerMove;

        public DoorSide side;

        private void Start()
        {
            CloseDoor();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                switch (side)
                {
                    case DoorSide.Left:
                        CamMove = new Vector3();
                        PlayerMove = new Vector3();
                        break;
                    case DoorSide.Right:
                        CamMove = new Vector3();
                        PlayerMove = new Vector3();
                        break;
                    case DoorSide.Top:
                        CamMove = new Vector3();
                        PlayerMove = new Vector3();
                        break;
                    case DoorSide.Bottom:
                        CamMove = new Vector3();
                        PlayerMove = new Vector3();
                        break;
                }

                GameObject.Find("Player").transform.position = GameObject.Find("Player").transform.position + PlayerMove;
                GameObject.Find("Camera").transform.position = GameObject.Find("Camera").transform.position + CamMove;

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

        //TODO Logic for Opening Dors
        //TODO Add Enemy Count to GameLogic Maybe Event


    }
}
