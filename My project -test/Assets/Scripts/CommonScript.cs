using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonScript
{
    public static Phase phase = Phase.STARTESCAPE;

    //true:罠設置フェーズ
    //false:逃走フェーズ
    public enum Phase
    {
        STARTESCAPE,
        ESCAPEPHASE,
        TRAPPHASE
    }

    //設置したトラップ
    public static List<GameObject> trapGameObjects = new List<GameObject>();
}
