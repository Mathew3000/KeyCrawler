using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KeyCrawler
{
    public interface IKeyboard
    {
        void AddKey(KeyFunction key);
    }

    public class Keyboard : MonoBehaviour, IKeyboard
    {
        private List<KeyFunction> enabledKeys = new List<KeyFunction>();
        // WIP: will probably change later!
        public object[] KeyboardImageAssets;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddKey(KeyFunction key)
        {
        }

        public void loadNextImage()
        {
        }
    }
}