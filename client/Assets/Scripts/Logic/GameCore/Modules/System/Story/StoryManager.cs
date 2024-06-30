using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : BaseMgr<StoryManager>
{
    private int mStoryId;


    public void StartStory(int storyId)
    {
        if (mStoryId != 0)
            return;

        mStoryId = storyId;

        UIManager.Instance.ShowUI(UIDefine.UI_STORY);
    }

    public void StopStory()
    {
        mStoryId = 0;
        UIManager.Instance.CloseUI(UIDefine.UI_STORY);
    }

    public int GetStoryId()
    {
        return mStoryId;
    }

    public bool NextStory()
    {
        StoryItem item = TableManager.Instance.DataStory.GetData(mStoryId);
        if (item == null)
        {
            return false;
        }

        if (item.NextId == 0)
        {
            return false;
        }

        mStoryId = item.NextId;

        return true;
    }
}
