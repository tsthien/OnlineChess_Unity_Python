using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class networkManager : MonoBehaviour
{
    public Text uwin;
    public Text ulose;
    public Camera main1;
    public Camera main2;
    public bool started = false;
    public Button startbutton;
    [SerializeField]
    clickManager cm;
    [SerializeField]
    positionManager pm;
    UdpClient client = new UdpClient();
    IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20001); // endpoint where server is listening

    public class infoSend
    {
        public int ping;
        public int start;
        public int[] row1;
        public int[] row2;
        public int[] row3;
        public int[] row4;
        public int[] row5;
        public int[] row6;
        public int[] row7;
        public int[] row8;
    }
    public class infoRecive
    {
        public int turn; //1,2
        public int win; //0,1,2
        public int team; //1,2
        public int[] row1;
        public int[] row2;
        public int[] row3;
        public int[] row4;
        public int[] row5;
        public int[] row6;
        public int[] row7;
        public int[] row8;
    }

    int[,] row_to_matrix(int[] a0, int[] a1, int[] a2, int[] a3, int[] a4, int[] a5, int[] a6, int[] a7)
    {
        var new_m88 = new int[8, 8];
        for (int i = 0; i < 8; i++)
        {
            new_m88[0, i] = a0[i];
        }
        for (int i = 0; i < 8; i++)
        {
            new_m88[1, i] = a1[i];
        }
        for (int i = 0; i < 8; i++)
        {
            new_m88[2, i] = a2[i];
        }
        for (int i = 0; i < 8; i++)
        {
            new_m88[3, i] = a3[i];
        }
        for (int i = 0; i < 8; i++)
        {
            new_m88[4, i] = a4[i];
        }
        for (int i = 0; i < 8; i++)
        {
            new_m88[5, i] = a5[i];
        }
        for (int i = 0; i < 8; i++)
        {
            new_m88[6, i] = a6[i];
        }
        for (int i = 0; i < 8; i++)
        {
            new_m88[7, i] = a7[i];
        }
        return new_m88;
    }
    int[] get_row(int row, int[,] matrix)
    {
        var array = new int[8];
        for (int i = 0; i < 8; i++)
        {
            array[i] = matrix[row - 1, i];
        }
        return array;
    }

    public void send_start()
    {
        var infosend = new infoSend
        {
            start = 1,
        };
        string jsonString = JsonUtility.ToJson(infosend);
        
        Byte[] sendBytes = Encoding.ASCII.GetBytes(jsonString);
        client.Send(sendBytes, sendBytes.Length);

        // then receive data
        var receivedData = client.Receive(ref ep);
        Debug.Log("receive data from " + ep.ToString());
        string result = System.Text.Encoding.UTF8.GetString(receivedData);
        Debug.Log(result);
        started = true;
    }
    public void ping()
    {
        var infosend = new infoSend
        {
            ping = 1,
        };
        string jsonString = JsonUtility.ToJson(infosend);

        Byte[] sendBytes = Encoding.ASCII.GetBytes(jsonString);
        client.Send(sendBytes, sendBytes.Length);

        // then receive data
        try
        {
            var receivedData = client.Receive(ref ep);
            Debug.Log("receive data from " + ep.ToString());

            string result = System.Text.Encoding.UTF8.GetString(receivedData);
            Debug.Log(result);
            var inforecive = JsonUtility.FromJson<infoRecive>(result);

            if (inforecive.win != 0)
            {
                Debug.Log(inforecive.win);
                Debug.Log(cm.myteam);
                if (inforecive.win == cm.myteam)
                {
                    //win
                    end_signal();
                    cm.win = 0;
                    cm.myteam = 0;
                    cm.turn = 0;
                    startbutton.interactable = true;
                    uwin.gameObject.SetActive(true);

                }
                else if (inforecive.win != cm.myteam)
                {
                    //lose
                    end_signal();
                    cm.win = 0;
                    cm.myteam = 0;
                    cm.turn = 0;
                    startbutton.interactable = true;
                    ulose.gameObject.SetActive(true);
                }
            }
            else
            {
                cm.win = inforecive.win;
                cm.myteam = inforecive.team;
                cm.turn = inforecive.turn;
                pm.m88 = row_to_matrix(inforecive.row1, inforecive.row2, inforecive.row3, inforecive.row4, inforecive.row5, inforecive.row6, inforecive.row7, inforecive.row8);
                pm.matrixToReal();
                
            }
        }
        catch
        {
            Debug.Log("Missing package!");
        }
        
    }
    public void update_board()
    {
        var infosend = new infoSend
        {
            ping = 0,
            start = 0,
            row1 = get_row(1, pm.m88),
            row2 = get_row(2, pm.m88),
            row3 = get_row(3, pm.m88),
            row4 = get_row(4, pm.m88),
            row5 = get_row(5, pm.m88),
            row6 = get_row(6, pm.m88),
            row7 = get_row(7, pm.m88),
            row8 = get_row(8, pm.m88),
        };
        string jsonString = JsonUtility.ToJson(infosend);

        Byte[] sendBytes = Encoding.ASCII.GetBytes(jsonString);
        client.Send(sendBytes, sendBytes.Length);

        // then receive data
        var receivedData = client.Receive(ref ep);
        Debug.Log("receive data from " + ep.ToString());

        string result = System.Text.Encoding.UTF8.GetString(receivedData);
        Debug.Log(result);
    }

    public void end_signal()
    {
        var infosend = new infoSend
        {
            ping = 0,
            start = 0,
        };
        string jsonString = JsonUtility.ToJson(infosend);

        Byte[] sendBytes = Encoding.ASCII.GetBytes(jsonString);
        client.Send(sendBytes, sendBytes.Length);

        // then receive data
        var receivedData = client.Receive(ref ep);
        Debug.Log("receive data from " + ep.ToString());

        string result = System.Text.Encoding.UTF8.GetString(receivedData);
        Debug.Log(result);
        if (result == "999")
        {
            started = false;
        }
    }

    void Start()
    {
        client.Client.ReceiveTimeout = 2000;
        client.Connect(ep);

    }
    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            ping();
        }
        
        if (cm.myteam == 1 && main1.enabled == false)
        {
            main1.enabled = true;
            main1.GetComponent<AudioListener>().enabled = true;
            main2.enabled = false;
            main2.GetComponent<AudioListener>().enabled = false;
        }
        else if (cm.myteam == 2 && main2.enabled == false)
        {
            main1.enabled = false;
            main1.GetComponent<AudioListener>().enabled = false;
            main2.enabled = true;
            main2.GetComponent<AudioListener>().enabled = true;
        }
    }
}
