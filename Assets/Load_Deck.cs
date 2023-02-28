using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Load_Deck : MonoBehaviour
{
    [SerializeField]
    private Deck deck;
    [SerializeField]
    private Premade_Decks CurrentDeck;
    [SerializeField]
    private GameObject[] CardSlots;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 21; i++)
        {
            CardSlots[i] = gameObject.transform.GetChild(0).transform.GetChild(i).gameObject;
        }

        CurrentDeck = deck.CurrentDeck;
        LoadDeckBuilder(CurrentDeck, CardSlots);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDeckBuilder(Premade_Decks deck, GameObject[] cards)
    {
        for(int i = 0; i < CurrentDeck.cards.Count; i++)
        {
            CardSlots[i].transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().spell = CurrentDeck.cards[i];
            CardSlots[i].transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().SpellInfo = CurrentDeck.cards[i].GetComponent<Spell_Info>();
            CardSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = CurrentDeck.cards[i].GetComponent<Spell_Info>().CardSprite;
        };

        CardSlots[20].transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().spell = CurrentDeck.BasicSpell;
        CardSlots[20].transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().SpellInfo = CurrentDeck.BasicSpell.GetComponent<Spell_Info>();
        CardSlots[20].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = CurrentDeck.BasicSpell.GetComponent<Spell_Info>().CardSprite;
    }
}