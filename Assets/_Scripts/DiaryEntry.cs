using UnityEngine;
using System.Collections;

namespace HighHouse
{
    /// <summary>
    /// An entry for the diary
    /// </summary>
    [CreateAssetMenu]
    public class DiaryEntry : ScriptableObject
    {
        [SerializeField]
        protected Sprite entryPage;
        
        public Sprite EntryPage => entryPage;
        public string AssetName => name;
     }
}