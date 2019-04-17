using UnityEngine;
using System.Collections;

namespace HighHouse
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite image;
    }
}