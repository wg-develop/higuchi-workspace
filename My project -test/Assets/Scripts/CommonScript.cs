using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonScript
{
    public static Phase phase = Phase.STARTESCAPE;

    //true:㩐ݒu�t�F�[�Y
    //false:�����t�F�[�Y
    public enum Phase
    {
        STARTESCAPE,
        ESCAPEPHASE,
        TRAPPHASE
    }

    //�ݒu�����g���b�v
    public static List<GameObject> trapGameObjects = new List<GameObject>();
}
