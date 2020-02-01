using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public interface IPlayerLife
    {
        void AddHP();

        void DoDamage();

        float GetLive();
    }

    public interface IItem
    {

    }


    public class Player : MonoBehaviour, IPlayerLife
    {
        #region EditorSettings
        
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}