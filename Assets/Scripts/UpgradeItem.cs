﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public enum UpgradeKind
    {
        levelUp
    }

    public class UpgradeItem : Item
    {
        public UpgradeKind upgradeKind;

        private void Start()
        {
            Kind = itemKind.upgrade;
        }

    }
}
