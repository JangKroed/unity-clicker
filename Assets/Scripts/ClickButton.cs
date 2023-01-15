using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public void OnClick()
    {
        // gold = gold + goldPerClick;
        int goldPerClick = DataController.GetInstance().GetGoldPerClick();
        DataController.GetInstance().AddGold(goldPerClick);
    }
}
