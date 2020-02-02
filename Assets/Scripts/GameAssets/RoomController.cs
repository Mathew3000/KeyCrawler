using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class RoomController : MonoBehaviour
    {
        bool bIsClear;
        public GameObject goRoom;
        GameObject goAktivRoom;

        // Start is called before the first frame update
        void Start()
        {
            bIsClear = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Camera>())
            {
                goAktivRoom = Instantiate(goRoom);
                
                foreach (GameObject goSpawner in FindObjectsOfType<GameObject>())
                {
                    if (goSpawner.GetComponent<EnemySpawner>())
                    {
                        goSpawner.GetComponent<EnemySpawner>().bSpawn = !bIsClear;
                    }
                }
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Camera>())
            {
                Destroy(goAktivRoom);
                bIsClear = true;
            }
        }



    }
}