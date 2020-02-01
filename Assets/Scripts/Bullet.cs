using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class Bullet : MonoBehaviour
    {
        #region EditorSettings
        public float maxLifetime = 5.0f;
        #endregion

        #region PrivateVariables
        private float timeLeft = 1.0f;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            timeLeft = maxLifetime;
        }

        // Update is called once per frame
        void Update()
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft <= 0)
            {
                DestroyBullet();
            }
        }

        #region PublicMember
        public void DestroyBullet()
        {
            Destroy(gameObject);
        }
        #endregion
    }
}