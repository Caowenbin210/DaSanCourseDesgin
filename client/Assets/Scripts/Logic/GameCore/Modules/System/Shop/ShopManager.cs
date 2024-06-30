using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : BaseMgr<ShopManager>
{
    public void OpenShop()
    {
        UIManager.Instance.ShowUI(UIDefine.UI_SHOP);
    }
}
