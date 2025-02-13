//Worked on by Dan Huynhvo
//CS426

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour // This class handles any game events to decrease needed references in the code base
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> OnEnemyDeath;
    public event Action<int, GameObject> OnShopBuy;
    public event Action<int> NearDroppedCard;
    public event Action<int, bool> MoneyIncrement;
    public event Action DeathIncrement;

    public void DropCard_E(int ID)
    {
        if(OnEnemyDeath != null)
        {
            OnEnemyDeath(ID);
        }
    }
    public void DropCard_S(int ID, GameObject player)
    {
        if (OnShopBuy != null)
        {
            OnShopBuy(ID, player);
        }
    }
    public void PickUpCard_E(int ID)
    {
        if(NearDroppedCard != null)
        {
            NearDroppedCard(ID);
        }
    }
    public void IncrementEnemyDeath()
    {
        if (DeathIncrement != null)
        {
            DeathIncrement();
        }
    }
    public void MoneyGained(int value, bool stevent)
    {
        if (MoneyIncrement != null)
        {
            MoneyIncrement(value, stevent);
        }
    }
}
