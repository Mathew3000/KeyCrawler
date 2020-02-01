using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{

    public class Spike : MonoBehaviour
    {
        public Sprite spIsIn;
        public Sprite spIsOut;
        public AudioClip acSwitch;
        
        bool bIsOut;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Cycle());
        }
        

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Player>() && bIsOut)
            {
                other.GetComponent<Player>().DoDamage(1f);
            }
        }

        IEnumerator Cycle()
        {
            yield return new WaitForSeconds(2);
            AudioSource.PlayClipAtPoint(acSwitch, this.transform.position);
            if (bIsOut)
            {
                this.GetComponent<SpriteRenderer>().sprite = spIsIn;
                bIsOut = false;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = spIsOut;
                bIsOut = true;
            }
            StartCoroutine(Cycle());
            yield return null;
        }
    }
}
