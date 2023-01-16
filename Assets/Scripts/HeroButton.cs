using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroButton : MonoBehaviour
{
    public Color upgradebleColor = Color.blue;
    public Color notUpgradebleColor = Color.red;

    public Image colorImage;

    public Text ItemDisplayer;

    public CanvasGroup canvasGroup;

    public Slider slider;

    public string itemName;

    public int level;

    [HideInInspector]
    public int currentCost;

    public int startCurrentCost = 1;

    [HideInInspector]
    public int goldPerSec;
    public int startGoldPerSec = 1;

    public float costPow = 3.14f;
    public float upgradePow = 1.07f;

    [HideInInspector]
    public bool isPurchased = false;

    void Start()
    {
        DataController.Instance.LoadHeroButton(this);

        StartCoroutine("AddGoldLoop");

        UpdateUI();
    }

    public void PurchaseItem()
    {
        if (DataController.Instance.gold >= currentCost)
        {
            isPurchased = true;
            DataController.Instance.gold -= currentCost;
            level++;

            UpdateItem();
            UpdateUI();

            DataController.Instance.SaveHeroButton(this);
        }
    }

    IEnumerator AddGoldLoop()
    {
        while (true)
        {
            if(isPurchased)
            {
                DataController.Instance.gold += goldPerSec;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void UpdateItem()
    {
        goldPerSec = goldPerSec + startGoldPerSec * (int) Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (int) Mathf.Pow(costPow, level);
    }

    public void UpdateUI()
    {
        ItemDisplayer.text = itemName + "\nLevel: " + level + "\nCost: " + currentCost + 
            "\nGold Per Sec: " + goldPerSec;

        slider.minValue = 0;
        slider.maxValue = currentCost;

        slider.value = DataController.Instance.gold;

        if (isPurchased)
        {
            canvasGroup.alpha = 1.0f;
        }
        else
        {
            canvasGroup.alpha = 0.6f;
        }

        if (currentCost <= DataController.Instance.gold)
        {
            colorImage.color = upgradebleColor;
        }
        else
        {
            colorImage.color = notUpgradebleColor;
        }
    }

    private void Update()
    {
        UpdateUI();
    }
}
