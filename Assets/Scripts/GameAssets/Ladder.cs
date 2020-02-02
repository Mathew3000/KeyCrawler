using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeyCrawler { 
public class Ladder : MonoBehaviour
{
        public string strNextLvl; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                SceneManager.LoadScene(strNextLvl);
            }
        }
    }
}
