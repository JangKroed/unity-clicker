using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private static DataController instance;

    public static DataController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataController>();

                if (instance == null )
                {
                    GameObject container = new("DataController");

                    instance = container.AddComponent<DataController>();
                }
            }
            return instance;
        }
    }

    private HeroButton[] HeroButtons;

    public long gold
    {
        get
        {
            if (!PlayerPrefs.HasKey("Gold"))
            {
                return 0;
            }

            string tmpGold = PlayerPrefs.GetString("Gold");
            return long.Parse(tmpGold);
        }
        set
        {
            PlayerPrefs.SetString("Gold", value.ToString());
        }
    }
    public long goldPerClick
    {
        get
        {
            if (!PlayerPrefs.HasKey("GoldPerClick"))
            {
                return 1;
            }
            string tmpGoldPerClick = PlayerPrefs.GetString("GoldPerClick");
            return long.Parse(tmpGoldPerClick);
        }
        set
        {
            PlayerPrefs.SetString("GoldPerClick", value.ToString());
        }
    }

    void Awake()
    {
        // 플레이어 저장정보 삭제
        // PlayerPrefs.DeleteAll();
        HeroButtons = FindObjectsOfType<HeroButton>();
    }

    public void LoadUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        upgradeButton.level = PlayerPrefs.GetInt(key + "_level", 1);
        upgradeButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade",
            upgradeButton.startGoldByUpgrade);
        upgradeButton.currentCost = PlayerPrefs.GetInt(key + "_cost", upgradeButton.startCurrentCost);
    }

    public void SaveUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        PlayerPrefs.SetInt(key + "_level", upgradeButton.level);
        PlayerPrefs.SetInt(key + "_goldByUpgrade", upgradeButton.goldByUpgrade);
        PlayerPrefs.SetInt(key + "_cost", upgradeButton.currentCost);
    }

    public void LoadHeroButton(HeroButton HeroButton)
    {
        string key = HeroButton.itemName;

        HeroButton.level = PlayerPrefs.GetInt(key + "_level");
        HeroButton.currentCost = PlayerPrefs.GetInt(key + "_cost", HeroButton.startCurrentCost) ;
        HeroButton.goldPerSec = PlayerPrefs.GetInt(key + "_goldPerSec");

        if (PlayerPrefs.GetInt(key + "_isPurchased") == 1)
        {
            HeroButton.isPurchased = true;
        }
        else
        {
            HeroButton.isPurchased= false;
        }
    }
    
    public void SaveHeroButton(HeroButton HeroButton)
    {
        string key = HeroButton.itemName;

        PlayerPrefs.SetInt(key + "_level", HeroButton.level);
        PlayerPrefs.SetInt(key + "_cost", HeroButton.currentCost);
        PlayerPrefs.SetInt(key + "_goldPerSec", HeroButton.goldPerSec);

        if (HeroButton.isPurchased == true)
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_isPurchased",01);
        }

    }
    public int GetGoldPerSec()
    {
        int goldPerSec = 0;
        for (int i = 0; i < HeroButtons.Length; i++)
        {
            goldPerSec += HeroButtons[i].goldPerSec;
        }
        return goldPerSec;
    }
}
