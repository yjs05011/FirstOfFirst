using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeyManger : GSingleton<GameKeyManger>
{
    public enum KeyAction
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT,
        INTERRUPT,
        EVASION,
        ATTACK,
        SKILL,
        WEAPONCHANGE,
        USEPANDENT,
        INVENTORY,
        TOGGLEUPLEFT,
        TOGGLEUPRIGHT,
        TOGGLEDOWNLEFT,
        TOGGLEDOWNRIGHT,
        KEYCOUNT
    }

    public string[] keyString;
    public KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W,
     KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.E, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.Z, KeyCode.L, KeyCode.I, KeyCode.Q, KeyCode.E, KeyCode.Z, KeyCode.C };
    protected override void Init()
    {
        base.Init();
        DefaultKeySetting();

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
