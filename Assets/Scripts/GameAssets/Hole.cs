using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeyCrawler
{
    public class Hole : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                //other.GetComponent<Player>().FALLINHOLE;
            }
        }
    }
}