using UnityEngine;
using System.Collections;

namespace HighHouse
{
    /// <summary>
    /// representation of an item.
    /// </summary>
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        [SerializeField]
        protected string itemName;
        [SerializeField]
        protected string description;
        [SerializeField]
        protected Sprite image;

        public string ItemName => itemName;
        public string Description => description;
        public Sprite Image => image;
        public string AssetName => name;
    }
}