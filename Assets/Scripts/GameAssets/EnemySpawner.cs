using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject goEnemy;
        public bool bSpawn;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Spawn());
        }
        
        IEnumerator Spawn()
        {
            yield return null;

            if (bSpawn)
            {
                Instantiate(goEnemy, this.transform);
            }

            yield return null;
        }
    }
}