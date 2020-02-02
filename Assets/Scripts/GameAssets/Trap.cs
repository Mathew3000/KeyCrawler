using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class Trap : MonoBehaviour
    {
        public Sprite spOff;
        bool bIsOn;

        private void Start()
        {
            bIsOn = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() && bIsOn)
            {
                //TODO add Enemy too
                other.GetComponent<Player>().DoDamage(1f);
                bIsOn = false;
                this.GetComponent<SpriteRenderer>().sprite = spOff;
            }
            
        }
    }
}