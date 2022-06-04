using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "mini-game", menuName = "Extintion Escape/Mini-Game")]
public class MiniGameScriptableObject : ScriptableObject
{
    public int order;
    public bool show;
    public string GameName;
    public string SceneName;
    [TextArea(3, 8)] public string Description;
}
