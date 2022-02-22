using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPing : MonoBehaviour
{
    public positionManager pm;
    public networkManager nm;
    // Start is called before the first frame update
    public void onclick()
    {
        nm.ping();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
