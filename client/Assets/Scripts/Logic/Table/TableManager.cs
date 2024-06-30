using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : BaseMgr<TableManager>
{
    public TableStory DataStory = new TableStory();

    public void Init()
    {
        DataStory.Init();
    }
}
