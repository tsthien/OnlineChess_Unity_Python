using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class startButton : MonoBehaviour
{
    public Button startbutton;
    public networkManager nm;
    public positionManager pm;
    public void onclick()
    {
        startbutton.interactable = false;
        nm.uwin.gameObject.SetActive(false);
        nm.ulose.gameObject.SetActive(false);
        nm.send_start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
