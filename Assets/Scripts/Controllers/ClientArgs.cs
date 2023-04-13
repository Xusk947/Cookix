using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientArgs : EventArgs
{
    public ClientController Client;
    public ClientArgs(ClientController client)
    {
        Client = client;
    }
}
