using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public enum KeyFunction
    {
        w,
        a,
        s,
        d,
        space,
        enter
    }

    public class KeyItem : Item
    {
        public KeyFunction KeyFunction;

        private void Start()
        {
            Kind = itemKind.key;
            Value = KeyFunction;
        }
    }
}
