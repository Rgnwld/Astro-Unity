using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;
    [TextArea(1, 20)]
    public string[] sentences;
}
