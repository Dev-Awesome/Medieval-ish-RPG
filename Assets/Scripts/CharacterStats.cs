using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    public int currentHP;
    public int maxHP = 100;
    public int currentMagicka;
    public int maxMagicka = 30;
    public int[] magickaLvlBonus;
    public int strength;
    public int defence;
    public int weaponPower;
    public int armor;
    public int[] expToNextLevel;
    public int maxLevel = 90;
    public int baseEXP = 3000;
    public string equippedWeapon;
    public string equippedArmor;
    public Sprite charImage;

	// Use this for initialization
	void Start () {
		expToNextLevel = new int[maxLevel];
	    expToNextLevel[1] = baseEXP;

	    for (int i = 2; i < expToNextLevel.Length; i++)
	    {
	        expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.K))
	    {
            AddEXP(1000);
	    }
	}

    public void AddEXP(int expToAdd)
    {
        currentEXP += expToAdd;
        if (playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                playerLevel++;
                if (playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                maxMagicka += magickaLvlBonus[playerLevel];
                currentMagicka = maxMagicka;
            }
        }
        
        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
