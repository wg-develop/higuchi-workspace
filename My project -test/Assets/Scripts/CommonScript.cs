using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonScript
{
    //true:罠設置フェーズ
    //false:逃走フェーズ
    public static bool phaseFlag = false;

    //設置したトラップ
    public static List<GameObject> trapGameObjects = new List<GameObject>();
}
