using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeyManger : GSingleton<GameKeyManger>
{
    public enum KeyAction { UP, DOWN, LEFT, RIGHT, INTERRUPT, EVASION, ATTACK, KEYCOUNT }

    public KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.E, KeyCode.Space, KeyCode.J };
    protected override void Init()
    {

        base.Init();
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
    }
    public static class KeySetting
    {
        public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();
    }
}
