using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public interface IKeyboard
    {
        void AddKey(KeyFunction key);
    }

    public class Keyboard : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
