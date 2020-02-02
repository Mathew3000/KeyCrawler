using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class EWandera : MonoBehaviour
    {
        float fHP;

        Vector3 v3MoveDirection;
        public float fMoveTime;
        public float fSpeed;

        void Start()
        {
            StartCoroutine(AI());
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                other.GetComponent<Player>().DoDamage(1f);
            }

            if (other.GetComponent<Bullet>())
            {
                other.GetComponent<Bullet>().DestroyBullet();
                fHP = -1f;
                if(fHP <= 0)
                {
                    Destroy(this);
                }
                StopCoroutine(AI());
                StartCoroutine(AI_Break());
            }
        }

        private void Update()
        {
            this.GetComponent<CharacterController>().Move(v3MoveDirection * fSpeed * Time.deltaTime);
        }


        IEnumerator AI()
        {
            fMoveTime = Random.Range(0.8f, 1.2f);

            float fRandDirection = Random.Range(0, 360);
            v3MoveDirection = new Vector3(Mathf.Cos(fRandDirection), 0, Mathf.Sin(fRandDirection));

            yield return new WaitForSeconds(fMoveTime);

            StartCoroutine(AI());
            yield return null;
        }

        IEnumerator AI_Break()
        {
            fMoveTime = Random.Range(0.8f, 1f);
            
            v3MoveDirection = v3MoveDirection * -1;

            yield return new WaitForSeconds(fMoveTime);

            StartCoroutine(AI());
            yield return null;
        }
    }
}
