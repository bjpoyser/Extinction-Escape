using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "mini-game", menuName = "Extintion Escape/Mini-Game")]
public class MiniGameScriptableObject : ScriptableObject
{
    public int order;
    public string GameName;
    public string SceneName;
    [TextArea] public string Description;
}
