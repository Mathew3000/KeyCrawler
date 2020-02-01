using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class FallTrap : MonoBehaviour
    {
        public Sprite spOpen;
        public Sprite spClosed;

        bool bIsOpen;

        void Start()
        {
            bIsOpen = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                if (bIsOpen) {
                    //other.GetComponent<Player>().FALLINHOLE;
                }
                else
                {
                    StartCoroutine(Triggert());
                }

                }
            }

        IEnumerator Triggert()
        {
            yield return new WaitForSeconds(1.2f);

            bIsOpen = true;
            this.GetComponent<SpriteRenderer>().sprite = spOpen;

            StartCoroutine(SelfeClose());
            yield return null;
        }

        IEnumerator SelfeClose()
        {
            yield return new WaitForSeconds(3f);

            bIsOpen = false;
            this.GetComponent<SpriteRenderer>().sprite = spClosed;

            yield return null;
        }

    }

}