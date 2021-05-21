using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ch13", menuName = "NPC Files Archive")]
public class NPC : ScriptableObject
{
    public string name;
    [TextArea(3, 15)]
    public string[] dialogue;
    [TextArea(3, 15)]
    public string[] playerDialogue;
}
