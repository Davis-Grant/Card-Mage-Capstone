// Worked on by Abida Mim and Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health = 0f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField]
    private HealthBar bar;
    [SerializeField] private GameObject mainCamera;
    [SerializeField]
    private GameObject DefeatScreen;
    [SerializeField]
    private GameObject DefeatText;
    [SerializeField] private AnimationCurve _FadeInCurve;
    [SerializeField]
    private float HitCooldown;
    [SerializeField]
    private Sprite_Flash Flash;
    private bool RecentlyHit;
    [SerializeField]
    private GameObject CurrencyUI;
    [SerializeField]
    private GameObject HandUI;
    [SerializeField]
    private GameObject TimerUI;
    [SerializeField]
    private GameObject HealthUI;
    [SerializeField]
    private GameObject BossHealth;
    [SerializeField]
    private Player_Currency SteveMoney;

    public float DefenceBuff;
    public bool IsAlive 
    {
        get { return health > 0; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Flash = gameObject.transform.GetChild(1).GetComponent<Sprite_Flash>();
        health = maxHealth;
        bar.SetMaxHealth(maxHealth);
        DefenceBuff = 1f;
    }
    
    void Awake(){
        gameObject.SetActive(true);
    }

    private void Update()
    {
        bar.SetHealth(health);
    }

    private IEnumerator ResetRecentlyHit(float HitTimer)
    {
        yield return new WaitForSeconds(HitTimer);
        RecentlyHit = false;
    }


        // when the character takes damage, screen shake is activated,
        // the character sprite changes color, a sound happens to indicate damage,
        // and health bar is changed
    public void TakeDamage(float mod)
    {
        if (!IsAlive)
        {
            return;
        };

        if(!RecentlyHit)
        {
            health -= mod / DefenceBuff; ;
            AudioManager.instance.Play("PlayerDamaged");
            mainCamera.GetComponent<shaker>().start = true;
            RecentlyHit = true;
            Flash.Flash();
            StartCoroutine(ResetRecentlyHit(HitCooldown));
        }
        if(!IsAlive)
        {
            Time.timeScale = 0f;
            DefeatScreen.SetActive(true);
            DefeatText.SetActive(true);
            AudioManager.instance.Play("GameOver");
            mainCamera.GetComponent<shaker>().enabled = false;
            StartCoroutine(ShowDefeatScreen(5));
        }
    }

    public void UpdateHealth(float mod){
        health += mod;
        
        if(health > maxHealth){
            health = maxHealth;
            
        }
        else if(health <= 0f){
            health = 0f;
        }
    }

        // code for the defeat screen, which toggles of all other ui elements
        // and pulls up a black screen slowly with indicated text
    IEnumerator ShowDefeatScreen(float fadeinDuration)
    {
        SteveMoney.SteveReset();
        CurrencyUI.SetActive(false);
        HandUI.SetActive(false);
        HealthUI.SetActive(false);
        TimerUI.SetActive(false);
        BossHealth.SetActive(false);
        //mainCamera.GetComponent<shaker>().enabled = false;
        // GetComponent<shaker>().enabled = false;
        float currentTime = 0;
        while (currentTime < fadeinDuration)
        {
            float output = 0 + ((1 - 0) / (fadeinDuration - 0)) * (currentTime - 0);
            currentTime += Time.unscaledDeltaTime;
            float t = _FadeInCurve.Evaluate(output);
            DefeatScreen.GetComponent<Image>().color = new Color(0, 0, 0, t);
            DefeatText.GetComponent<Text>().color = new Color(170, 0, 0, t);
            yield return null;
        }
        //DefeatScreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        //DefeatText.GetComponent<Text>().color = new Color(170, 0, 0, 1);
        //Time.timeScale = 1f;
        //yield return new WaitForSeconds(3f);
        //DefeatScreen.SetActive(false);
        //DefeatText.SetActive(false);
        //SceneManager.LoadScene("GameStage");
    }
}