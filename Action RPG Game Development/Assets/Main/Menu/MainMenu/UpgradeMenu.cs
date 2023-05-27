using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public TMP_Text healt;
    public TMP_Text stamina;
    public TMP_Text attack;

    //public Button upgradeHealth;
    //public Button upgradeStamina;
    //public Button upgradeAttack;

    public Text _upgradeHealthCost;
    public Text _upgradeStaminaCost;
    public Text _upgradeAttackCost;

    private int healthCost;
    private int staminaCost;
    private int attackCost;

    private int healthLevel;
    private int staminaLevel;
    private int attackLevel;

    private int coin;
    public TMP_Text _coin;


    private int maxLevel = 30;
    void Start()
    {
        healthLevel = PlayerPrefs.GetInt("Health");
        staminaLevel = PlayerPrefs.GetInt("Stamina");
        attackLevel = PlayerPrefs.GetInt("Attack");
        coin = PlayerPrefs.GetInt("Coin");

        UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Starting()
    {
        healthLevel = PlayerPrefs.GetInt("Health");
        staminaLevel = PlayerPrefs.GetInt("Stamina");
        attackLevel = PlayerPrefs.GetInt("Attack");
        coin = PlayerPrefs.GetInt("Coin");

        UpdateData();
    }

    public void UpdateData()
    {
        PlayerPrefs.SetInt("Health", healthLevel);
        PlayerPrefs.SetInt("Stamina", staminaLevel);
        PlayerPrefs.SetInt("Attack", attackLevel);
        PlayerPrefs.SetInt("Coin", coin);

        _coin.SetText(coin.ToString());

        healt.SetText((100 + healthLevel * 20).ToString());
        stamina.SetText((100 + staminaLevel * 10).ToString());
        attack.SetText((10 + attackLevel * 5).ToString());

        //Health
        if (healthLevel >= maxLevel)
        {
            _upgradeHealthCost.text = "Max";
        }
        else
        {
            healthCost = 5 + (3 * healthLevel);
            _upgradeHealthCost.text = UpgradeText(healthCost);
        }

        //Stamina
        if (staminaLevel >= maxLevel)
        {
            _upgradeStaminaCost.text = "Max";
        }
        else
        {
            staminaCost = 5 + (3 * staminaLevel);
            _upgradeStaminaCost.text = UpgradeText(staminaCost);
        }

        //Attack
        if (attackLevel >= maxLevel)
        {
            _upgradeAttackCost.text = "Max";
        }
        else
        {
            attackCost = 5 + (3 * attackLevel);
            _upgradeAttackCost.text = UpgradeText(attackCost);
        }

    }

    string UpgradeText(int cost)
    {
        return "LV+ (" + cost.ToString() + " COINS)";
    }

    public void UpgradeHealth()
    {
        if (healthLevel < maxLevel && coin >= healthCost)
        {
            coin -= healthCost;
            healthLevel += 1;
            UpdateData();
        }
    }

    public void UpgradeStamina()
    {
        if (staminaLevel < maxLevel && coin >= staminaCost)
        {
            coin -= staminaCost;
            staminaLevel += 1;
            UpdateData();
        }
    }

    public void UpgradeAttack()
    {
        if (attackLevel < maxLevel && coin >= attackCost)
        {
            coin -= attackCost;
            attackLevel += 1;
            UpdateData();
        }
    }
}
