using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Map;

public class MapLoder : MonoBehaviour
{
    private string dataPath;
    private Map map;

    private void Awake()
    {
        Debug.Log("awake");
        dataPath = ".\\Assets\\Data\\map.json";
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        StreamReader reader = new StreamReader(dataPath); //受け取ったパスのファイルを読み込む
        string datastr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
        //Debug.Log(datastr);
        reader.Close();//ファイルを閉じる
        
        map = JsonUtility.FromJson<Map>(datastr);
        
        for(int i = 0; i < map.blocks.Length; i++){
            Debug.Log(map.blocks[i].type);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
