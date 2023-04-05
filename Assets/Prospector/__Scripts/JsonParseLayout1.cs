using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Poker
{

    /// <summary>
    /// Stores information about the Layout of the Prospector mine.
    /// </summary>
    [System.Serializable]
    public class JsonLayout
    {

        public Vector2 multiplier;
        public List<JsonLayoutSlot> slots;
        public JsonLayoutPile drawPile, discardPile;
    }

    /// <summary>
    /// Stores information for each slot in the layout.
    /// Implements Unity’s ISerializationCallbackReceiver Interface.
    /// </summary>
    [System.Serializable]
    public class JsonLayoutSlot : ISerializationCallbackReceiver
    {
        // a
        public int id;
        public int x;
        public int y;
        public bool faceUp;
        public string layer;
        public string hiddenByString;                                         // b

        [System.NonSerialized]                                                      // b
        public List<int> hiddenBy;                                               // b

        /// <summary>
        /// Pulls data from hiddenByString and places it into the hiddenBy List
        /// </summary>
        public void OnAfterDeserialize()
        {                                          // c
            hiddenBy = new List<int>();
            if (hiddenByString.Length == 0) return;

            string[] bits = hiddenByString.Split(',');
            for (int i = 0; i < bits.Length; i++)
            {
                hiddenBy.Add(int.Parse(bits[i]));
            }
        }

        /// <summary>
        /// Required by ISerializationCallbackReceiver, but empty in this class
        /// </summary>
        public void OnBeforeSerialize() { }     // Note the empty braces here        // d
    }

    /// <summary>
    /// Stores information for the draw and discard piles
    /// </summary>
    [System.Serializable]
    public class JsonLayoutPile
    {
        public int x, y;
        public string layer;
        public float xStagger; // xStagger fans cards to the side for the draw pile
    }

    public class JsonParseLayout1 : MonoBehaviour
    {
        public static JsonParseLayout1 S { get; private set; }


        [Header("Inscribed")]
        public TextAsset jsonLayout2File;

        [Header("Dynamic")]
        public JsonLayout layout;

        void Awake()
        {
            layout = JsonUtility.FromJson<JsonLayout>(jsonLayout2File.text);
            S = this;                                                               // e
        }
    }
}