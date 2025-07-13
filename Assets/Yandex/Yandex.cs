using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
    public Button Map8_button;
    public GameObject BlockingImage;

    public bool isMenu;

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [DllImport("__Internal")]
    private static extern void ShowRewardedAdv();

    [DllImport("__Internal")]
    private static extern void GameReady();

    [DllImport("__Internal")]
    private static extern void GameStart();

    [DllImport("__Internal")]
    private static extern void GameStop();

    private void Start()
    {
        GameReady();
        GameStart();
        if (isMenu)
        {
            if (PlayerPrefs.HasKey("Map8") == false)
            {
                PlayerPrefs.SetInt("Map8", 0);
            }
            else
            {
                Map8_button.interactable = true;
                BlockingImage.SetActive(false);
            }
        }
        
    }

    public void ShowAd()
    {
        ShowAdv();
        GameStop();
        Time.timeScale = 0;
    }
    public void ShowRewaededAd()
    {
        Time.timeScale = 0;
        ShowRewardedAdv();
        
    }
    public void GetReward()
    {
        Map8_button.interactable = true;
        BlockingImage.SetActive(false);
        PlayerPrefs.SetInt("Map8", 1);
    }
    public void TimeGo()
    {
        Time.timeScale = 1;
        GameStart();
    }

}
