//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connected_Spell : MonoBehaviour
{
    public GameObject spell;
    public Spell_Info SpellInfo;

    private void Start()    //linker script for functionality
    {
        if(spell != null)
        {
            SpellInfo = spell.GetComponent<Spell_Info>();
        };
        
    }
}
