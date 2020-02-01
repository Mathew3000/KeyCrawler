using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    enum itemKind
    {
        key,
        health,
        upgrade
    }

    public interface IItem<Item>
    {
         string Kind { get; set; }
    }

    public class Item : MonoBehaviour, IItem<Item>
    {
        private itemKind kind;

        internal itemKind Kind { get => kind; set => kind = value; }

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