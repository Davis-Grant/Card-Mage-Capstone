// Author: Grant Davis using Dan Huynhvo Enemy_Mechanics.cs as resource for this script.
// CS 426: Senior Project: Card Mage
// Ghost_Mechanics.cs
// Specific Mechanics for the ghost enemy, since the ghost doesn't function the same as other enemies.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Mechanics : MonoBehaviour
{
    SpriteRenderer sprite;
    //-----------Unused code----------------------------//
    //[SerializeField] Sprite DeadSprite;
    //--------Previous mechanic for dead enemy --------//
    Enemy_Info info; 
    public bool NotDead; //Check if player is alive.
    public int thisID;
    //Money Drop Chance
    [SerializeField] private float CopperMoneyChance;
    [SerializeField] private float SilverMoneyChance;
    [SerializeField] private float GoldMoneyChance;
    [SerializeField] private float SteveMoneyChance; ///Steve is a currency lol//
    [SerializeField] private GameObject[] MoneyRefArray;
    [SerializeField] private bool hasDroppedMoney = false;
    [SerializeField] private bool hasDroppedCard = false;
    [SerializeField] private Premade_Decks CardPool;
    [SerializeField] public float fadeDuration = 2f;
    //Commented out for removal. Unused for this enemy -Grant Davis.........//
    //[SerializeField] //Navigation used in Update() to turn to false on death
    //private UnityEngine.AI.NavMeshAgent navMeshAgent;
    //.......................................................................//

    // Start is called before the first frame update
    void Start()
    {   
        thisID = gameObject.GetInstanceID();
        GameEvents.current.OnEnemyDeath += DropCardOnDeath;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<Enemy_Info>();
        NotDead = true;
        MoneyRefArray = new GameObject[4];
        MoneyRefArray[0] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefCopper");
        MoneyRefArray[1] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefSilver");
        MoneyRefArray[2] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefGold");
        MoneyRefArray[3] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefSteve");
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, 1f, transform.parent.transform.position.z);
        // Get the NavMeshAgent component from the parent game object -Grant Davis- 
        //navMeshAgent = transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (info.health <= 0 && NotDead)
        {   
            //OnDeath Events for Dead Enemies---------------
            //FadeOut and Die
            FadeOut();
            //----------------------------------------------
            //Disable Collision
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
            //Drop Card
            if (!hasDroppedCard)
            {
                GameEvents.current.DropCard_E(thisID);
                DropMoneyOnDeath();
                hasDroppedCard = true;
            }
            //Drop Money
            if (!hasDroppedMoney)
            {
                DropMoneyOnDeath();
                hasDroppedMoney = true;
            }
        }
    }

    public void DropCardOnDeath(int ID)
    {
        float randChance = Random.Range(100f, 100f);
        if(randChance < info.DropChance && NotDead && thisID == ID)
        {
            Vector3 m_NewForce = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            int randCard = Random.Range(0, CardPool.cards.Count);
            
            Vector3 newPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            
            GameObject Card = Instantiate(CardPool.cards[randCard], newPosition, Quaternion.Euler(90, 0, 0));
            Card.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
        }
    }

    public void DropMoneyOnDeath()
    {
        float randChanceC;
        float randChanceS;
        float randChanceG;
        float randChanceSteve;
        for (int i = 0; i < 10; i++)
        {
            randChanceC = Random.Range(0, 100);
            randChanceS = Random.Range(0, 100);
            randChanceG = Random.Range(0, 100);
            randChanceSteve = Random.Range(0, 100);
            
            Vector3 newPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            
            if (randChanceC < CopperMoneyChance)
            {
                Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                GameObject C = Instantiate(MoneyRefArray[0], newPosition, Quaternion.Euler(90, 0, 0));
                C.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            }

            if (randChanceS < SilverMoneyChance)
            {
                Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                GameObject S = Instantiate(MoneyRefArray[1], newPosition, Quaternion.Euler(90, 0, 0));
                S.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            }

            if (randChanceG < GoldMoneyChance)
            {
                Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                GameObject G = Instantiate(MoneyRefArray[2], newPosition, Quaternion.Euler(90, 0, 0));
                G.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            }

            //if (randChanceSteve < SteveMoneyChance)
            //{
            //    Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
            //    GameObject Steve = Instantiate(MoneyRefArray[3], newPosition, Quaternion.Euler(90, 0, 0));
            //    Steve.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            //}
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.OnEnemyDeath -= DropCardOnDeath;
    }

    //FadeOut calls Coroutine -Grant Davis
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    //Change color of enemy and fade out effect, enemy object is destroy here -Grant Davis
    private IEnumerator FadeOutCoroutine()
    {
        Renderer enemyRenderer = GetComponent<Renderer>();
        Material enemyMaterial = enemyRenderer.material;
        Color initialColor = enemyMaterial.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Target color with 0 alpha (transparent)

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            enemyMaterial.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }
        // Optional: Destroy the enemy GameObject after the fade-out
        Destroy(gameObject);
    }
}