using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public interface IPlayerLife
    {
        void AddHP(float value);

        void DoDamage(float value);

        float GetLife();
    }

    public class Player : MonoBehaviour
    {
        #region EditorSettings
        public float baseDmg = 1.0f;
        #endregion

        #region Properties
        public float PlayerLife { get; private set; }
        #endregion

        void Start()
        {

        }

        void Update()
        {
            // Player Movement

        }

        #region InterfaceMember
        public void AddHp()
        {

        }
        #endregion
    }
}
