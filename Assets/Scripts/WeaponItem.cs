using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public enum WeaponKind
    {
        fist,
        gun
    }

    public class WeaponItem : Item
    {
        public WeaponKind weaponKind;

        private void Start()
        {
            Kind = itemKind.weapon;
        }
    }
}
