using System.Reflection;
using UnityModManagerNet;
using HarmonyLib;
using UnityEngine;

namespace ExampleMod
{
    [EnableReloading]

    static class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony harmony;
        public static bool IsEnabled;
        public static Texture2D texture;
        public static Sprite spr;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            // 이 메서드는 게임 실행 시 UnityModManager가 호출할 함수입니다.
            // Info.json의 EntryMethod 값을 바꾸어 다른 메서드가 호출되게 설정할 수 있습니다.

            Logger = modEntry.Logger;
            harmony = new Harmony(modEntry.Info.Id);

            modEntry.OnToggle = OnToggle;

            texture = new(1, 1);
            texture.LoadImage(File.ReadAllBytes(Path.Combine(modEntry.Path, "DialogBG.png")));
            spr = Sprite.Create(texture, new Rect(0, 0, 1790, 719), new Vector2(0.5f, 0.5f), 100);

            return true;
            // 반환된 값이 false라면 UnityModManager가 사용자에게 모드가 정상적으로 로드되지 않았다고 표시합니다.
            // 굳이 필요없다고 느껴지면 삭제하고 반환 값을 void로 바꾸어도 됩니다.
        }

        // 이 메서드는 모드를 켜거나 끌 때 호출됩니다. 이 메서드가 없으면 모드를 켜거나 끌 때 마다 게임을 재시작해야 합니다.
        // value 파라미터에는 모드가 켜졌는지(true) 꺼졌는지(false)가 인자로 들어갑니다.
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            IsEnabled = value;

            if (value)
            {
                // 모드가 켜질 때 아래 코드를 실행합니다.
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                // 모드가 꺼질 때 아래 코드를 실행합니다.
                harmony.UnpatchAll(modEntry.Info.Id);
            }

            return true;
            // 반환된 값이 true라면, 모드 상태를 전환합니다. false라면 전환하지 않습니다.
        }
    }
}