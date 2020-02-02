using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeyCrawler{
    public class LasserEmitter : MonoBehaviour
    {
        public float fDellay;

        //TODO Create Layer Mask and asign
        static int layer1 = 8;
        static int layer2 = 9;
        static int layermask1 = 1 << layer1;
        static int layermask2 = 1 << layer2;
        int finalmask = layermask1 | layermask2; // Or, (1 << layer1) | (1 << layer2)

        bool bIsOn;

        // Start is called before the first frame update
        void Start()
        {
            bIsOn = true;
            StartCoroutine(Go());
        }

        private void Update()
        {
            if (bIsOn)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, finalmask))
                    {
                        Debug.Log(hit);
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                        if (hit.transform.GetComponent<Player>())
                        {
                            hit.transform.GetComponent<Player>().DoDamage(1f);
                        }
                        
                        //TODO Draw in Game
                    }
                }
            }

        IEnumerator Go()
        {
            yield return new WaitForSeconds(fDellay);


            StartCoroutine(Cycle());
            yield return null;
        }

        IEnumerator Cycle()
        {
            yield return new WaitForSeconds(4f);
            if (bIsOn)
            {
                bIsOn = false;
            }
            else
            {
                bIsOn = true;
            }
            StartCoroutine(Cycle());
            yield return null;
        }

    }
}