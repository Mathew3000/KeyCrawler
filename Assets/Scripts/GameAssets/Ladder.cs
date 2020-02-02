using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler { 
public class Ladder : MonoBehaviour
{

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                GameObject.Find("ManagerContainer").GetComponent<GameLogicManager>().LoadNextLevel();
            }
        }
    }
}
