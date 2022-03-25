using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTrapButtonScript : MonoBehaviour
{
    public TrapFactoryScript trapFactoryScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(int num)
    {
        trapFactoryScript.CreateTrap(num);
    }
}
