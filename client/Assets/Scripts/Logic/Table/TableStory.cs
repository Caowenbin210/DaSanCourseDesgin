using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryItem
{
    public int Id;
    public string Name;
    public string Talk;
    public int NextId;
}

public class TableStory : MonoBehaviour
{
    public Dictionary<int,StoryItem> mData = new Dictionary<int, StoryItem>();

    public void Init()
    {
        StoryItem item1 = new StoryItem();
        item1.Id = 1;
        item1.Name = "��������";
        item1.Talk = "�ֵܣ��������ӣ�";
        item1.NextId = 2;
        mData.Add(1, item1);

        StoryItem item2 = new StoryItem();
        item2.Id = 2;
        item2.Name = "";
        item2.Talk = "�ҹ��������";
        item2.NextId = 3;
        mData.Add(2, item2);

        StoryItem item3 = new StoryItem();
        item3.Id = 3;
        item3.Name = "��������";
        item3.Talk = "ok������ɶ����";
        item3.NextId = 4;
        mData.Add(3, item3);

        StoryItem item4 = new StoryItem();
        item4.Id = 4;
        item4.Name = "";
        item4.Talk = "���ˣ�����ɶ����Ҫ";
        item4.NextId = 0;
        mData.Add(4, item4);

    }

    public StoryItem GetData(int id)
    {
        if (mData.ContainsKey(id))
            return mData[id];
        return null;
    }
}
