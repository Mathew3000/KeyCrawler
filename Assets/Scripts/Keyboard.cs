using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KeyCrawler
{
    public interface IKeyboard
    {
        void AddKey(KeyFunction key);
        void looseAllKeys();
    }

    public class Keyboard: MonoBehaviour, IKeyboard
    {
        #region Getter
        private List<GameObject> allKeys = new List<GameObject>();

        private List<GameObject> getAllKeys()
        {
            foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
            {
                allKeys.Add(t.gameObject);
            }
            return allKeys;
        }
        #endregion

        #region EditorSettings
        [Header("Key Image Sources")]
        public GameObject keyA;
        public GameObject keyW;
        public GameObject keyS;
        public GameObject keyD;

        public GameObject keyQ;
        public GameObject keyE;

        public GameObject keyEnter;
        public GameObject keySpace;
        public GameObject keyEsc;
        #endregion

        public void AddKey(KeyFunction key)
        {
            switch(key)
            {
                case KeyFunction.a:
                    keyA.SetActive(true);
                    break;
                case KeyFunction.w:
                    keyW.SetActive(true);
                    break;
                case KeyFunction.s:
                    keyS.SetActive(true);
                    break;
                case KeyFunction.d:
                    keyD.SetActive(true);
                    break;

                case KeyFunction.q:
                    keyQ.SetActive(true);
                    break;
                case KeyFunction.e:
                    keyE.SetActive(true);
                    break;

                case KeyFunction.enter:
                    keyEnter.SetActive(true);
                    break;
                case KeyFunction.esc:
                    keyEsc.SetActive(true);
                    break;
                case KeyFunction.space:
                    keySpace.SetActive(true);
                    break;
            }
        }

        public void looseAllKeys ()
        {

        }
    }
}