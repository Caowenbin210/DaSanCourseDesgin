using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIData
{
    public int mID;
    public string mPrefab;
}

public class UIDefine
{
    public static int UI_LOGIN = 100;
    public static int UI_MAIN = 101;
    public static int UI_STORY = 102;
    public static int UI_SHOP = 103;

    private static Dictionary<int, string> uiData = new Dictionary<int, string>();

    public static void Init()
    {
        uiData.Add(UI_LOGIN, "Assets/Res/UI/Prefab/UI_Login.prefab");
        uiData.Add(UI_MAIN, "Assets/Res/UI/Prefab/UI_Main.prefab");
        uiData.Add(UI_STORY, "Assets/Res/UI/Prefab/UI_Story.prefab");
        uiData.Add(UI_SHOP, "Assets/Res/UI/Prefab/UI_Shop.prefab");
    }

    public static string GetUI(int id)
    {
        return uiData[id];
    }
}