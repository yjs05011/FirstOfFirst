using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeyManger : GSingleton<GameKeyManger>
{
    public enum KeyAction { UP, DOWN, LEFT, RIGHT, INTERRUPT, EVASION, ATTACK, SKILL, WEAPONCHANGE, KEYCOUNT }
    public string[] keyString;
    public KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.E, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.Z };
    protected override void Init()
    {
        base.Init();
    }
    public static class KeySetting
    {
        public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();

    }
    public void DefaultKeySetting()
    {
        KeySetting.keys.Clear();
        Debug.Log("!");
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
    }
}
