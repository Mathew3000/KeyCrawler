using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public enum itemKind
    {
        key,
        health,
        upgrade,
        weapon
    }

    public interface IItem<Item>
    {
        itemKind getKind();
        AudioClip getSound();
        object getValue();
    }

    public class Item : MonoBehaviour, IItem<Item>
    {
        #region Properties
        internal itemKind Kind { get; set; }
        internal AudioClip Sound;
        internal object Value;
        #endregion

        #region Getter
        public itemKind getKind()
        {
            return Kind;
        }

        public AudioClip getSound()
        {
            return Sound;
        }

        public object getValue()
        {
            return Value;
        }
        #endregion

        public void Despawn()
        {
            Destroy(gameObject);
        }
    }
}