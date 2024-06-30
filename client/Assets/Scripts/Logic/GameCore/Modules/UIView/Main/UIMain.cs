using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public Image HeadIcon;
    public Text Name;

    public Image HpForce;
    public Text HpText;

    public Text Money;

    public void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        EntityMainPlayer mainPlayer = EntityManager.Instance.MainPlayer;
        Name.text = mainPlayer.GetName();
        HpText.text = string.Format("{0}",mainPlayer.GetHp());

        HpForce.fillAmount = (float)mainPlayer.GetHp() / (float)mainPlayer.GetMaxHp();

        Money.text = string.Format("{0}", mainPlayer.GetMoney());
        string imageName = string.Format("Assets/SIMPLE Avatars Icons/64X64/{0}", mainPlayer.HeadIcon);
        Sprite sprite = ResManager.Instance.LoadImage(imageName);
        HeadIcon.sprite = sprite;
    }
}
