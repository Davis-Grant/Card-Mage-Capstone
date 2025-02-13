//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card_Data", menuName = "ScriptableObjects/Premade_Decks", order = 1)]
public class Premade_Decks : ScriptableObject   //data object for decks and cards
{

    [SerializeField]
    public string DeckName;
    [SerializeField]
    public List<GameObject> cards;
    public List<string> CardNames;
    public GameObject BasicSpell;
}
