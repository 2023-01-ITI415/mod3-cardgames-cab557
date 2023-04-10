using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Poker;
namespace Poker
{
    [System.Serializable]
    public class JsonPip
    {
        public string type = "pip";
        public Vector3 loc;
        public bool flip = false;
        public float scale = 1;
    }

    [System.Serializable]
    public class JsonCard
    {
        public int rank;
        public string face;
        public List<JsonPip> pips = new List<JsonPip>();
    }
    [System.Serializable]
    public class JsonDeck
    {
        public List<JsonPip> decorators = new List<JsonPip>();
        public List<JsonCard> cards = new List<JsonCard>();
    }

    public class JsonParseDeck1 : MonoBehaviour
    {
        private static JsonParseDeck1 S { get; set; } // Another automatic property

        [Header("Inscribed")]
        public TextAsset jsonDeck2File;// The JSONDeck 

        [Header("Dynamic")]
        public JsonDeck deck;

        void Awake()
        {
            if (S != null)
            {
                Debug.LogError("JsonParseDeck.S can’t be set a 2nd time!");
                return;
            }
            S = this;

            deck = JsonUtility.FromJson<JsonDeck>(jsonDeck2File.text);
        }

        /// <summary>
        /// Returns the decorator layout information for all cards.
        /// </summary>
        static public List<JsonPip> DECORATORS
        {
            get { return S.deck.decorators; }
        }

        /// <summary>
        /// Returns the JsonCard matching the rank passed in.
        /// Note: The rank should be 1 (Ace) - 13 (King).
        /// </summary>
        /// <param name='rank'>Must be an int in range 1-13</param>
        /// <returns>JsonCard information</returns>
        static public JsonCard GET_CARD_DEF(int rank)
        {
            if ((rank < 1) || (rank > S.deck.cards.Count))
            {
                Debug.LogWarning("Illegal rank argument: " + rank);
                return null;
            }
            return S.deck.cards[rank - 1];
        }

    }
}