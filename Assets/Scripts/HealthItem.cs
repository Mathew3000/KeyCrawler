using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public class HealthItem : Item
    {
        public int HealthValue;

        private void Start()
        {
            Kind = itemKind.health;
        }
    }
}

