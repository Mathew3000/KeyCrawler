using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeyCrawler {
    public class PlayerSpawn : MonoBehaviour
    {
        private void Start()
        {
            GameObject.Find("ManagerContainer").GetComponent<GameLogicManager>().MoveCamera(new Vector3(-0.01f, 10.48f, -2.75f));
        }
    }
}