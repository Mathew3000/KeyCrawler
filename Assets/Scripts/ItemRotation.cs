using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class ItemRotation : MonoBehaviour
    {
        #region EditorSettings
        [Header("Rotations")]
        public float rotationSpeed = 1.0f;
        public float rotationLifetime = 2.0f;
        [Header("Score")]
        public int score = 20;
        #endregion

        #region PrivateVariables
        private Quaternion targetRotation;
        private float lifetimeLeft = 0;
        #endregion

        void Start()
        {
            targetRotation = Random.rotation;
            lifetimeLeft = rotationLifetime;
        }

        void Update()
        {
            lifetimeLeft -= Time.deltaTime;
            if (lifetimeLeft <= 0)
            {
                targetRotation = Random.rotation;
                lifetimeLeft = rotationLifetime;
            }
            // random rotate
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        #region EventHandler
        private void OnTriggerEnter(Collider other)
        {
            //other.GetComponent<Player>()?.AddScore(score);
            //Destroy(gameObject);
        }
        #endregion
    }
}
