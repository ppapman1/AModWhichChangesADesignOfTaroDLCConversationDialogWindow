using System.Reflection;
using UnityModManagerNet;
using HarmonyLib;
using UnityEngine;
using System.Text.RegularExpressions;

namespace ExampleMod
{
    public class SansTVYoutubeSubscribeAndLikePatches
    {

        [HarmonyPatch(typeof(TaroCutsceneScript), "DisplayText")]
        public static class TestPatch
        {
            [HarmonyPostfix]
            public static void GetTextPatch(TaroCutsceneScript __instance)
            { 
                var text = __instance.scene_text;
                text.text = "<line-height=75%>" + text.text.Replace("#CC6600", "#C6964F");
                text.alignment = TMPro.TextAlignmentOptions.Center;

                Main.Logger.Log(text.text);

                Regex colorRegex = new(@"(?<=<color=).*(?=>)");
                string textColor = colorRegex.Match(text.text).ToString();

                var box = __instance.scene_assets;
                box.bg.sprite = Main.spr;

                var rt = (RectTransform)box.bg.transform;
                rt.sizeDelta = new Vector2(1790, 719);

                if (ColorUtility.TryParseHtmlString(textColor, out Color resultColor))
                {
                    box.bg.color = resultColor;
                }

                __instance.scene_assets.nxt.gameObject.SetActive(false);
            }
        }
    }
}
