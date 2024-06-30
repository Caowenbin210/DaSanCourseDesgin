using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStory : MonoBehaviour
{
    public Text Name;
    public Text Talk;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        int storyId = StoryManager.Instance.GetStoryId();
        StoryItem item = TableManager.Instance.DataStory.GetData(storyId);
        if (item == null)
        {
            return;
        }

        if (item.Name == "")
        {
            Name.text = EntityManager.Instance.MainPlayer.GetName();
        }
        else
        {
            Name.text = item.Name;
        }

        Talk.text = item.Talk;
    }

    public void OnClick()
    {
        // 点击屏幕的时候播放下一句对话，如果没有下一句对话，关闭对话界面
        if (!StoryManager.Instance.NextStory())
        {
            StoryManager.Instance.StopStory();
            return;
        }


        UpdateUI();
    }
}
