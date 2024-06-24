using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum E_ServerStateType
{
    Busy,
    Hot,
    Smooth,
    Fixed,
    None
}
public class ServerInfo
{
    public int id;
    public string name;
    public E_ServerStateType state;
    public bool isNew;
}

