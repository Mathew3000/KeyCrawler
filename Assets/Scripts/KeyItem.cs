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
        q,
        e,
        enter,
        esc,
        space,
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
