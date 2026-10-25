using System;
using TMPro;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Configuration;
using HarmonyLib;
using Reactor.Utilities;

namespace ChatUs;

[BepInAutoPlugin(ID, "ChatUS")]
[BepInDependency("gg.reactor.api")]
[BepInProcess("Among Us.exe")]
public partial class ChatUs : BasePlugin
{
    public const string ID = "com.Isax82.ChatUs";

    public Harmony Harmony { get; } = new(ID);

    public static ConfigEntry<bool> ChatKeyboardShortcuts;
    public static ConfigEntry<bool> AllowSymbols;
    public static ConfigEntry<bool> HigherCharacterLimit;
    public static ConfigEntry<bool> LowerChatCooldown;
    public static ConfigEntry<int> ChatHistoryLimit;
    public static ConfigEntry<bool> NoCharacterLimit;
    public static ConfigEntry<bool> NoChatCooldown;

    public static string DisplayVersion => Version.Contains('+')
        ? Version.Substring(0, Version.IndexOf("+", StringComparison.Ordinal))
        : Version;

    public static bool IsDevBuild => Version.Contains("dev");

    public override void Load()
    {
        ChatKeyboardShortcuts = Config.Bind("Chat", "ChatKeyboardShortcuts", true, "Use Ctrl+C, Ctrl+V and Ctrl+X to copy, paste and cut chat messages\nUse the up and down arrow keys to scroll through your previously sent chat messages");
        AllowSymbols = Config.Bind("Chat", "AllowSymbols", true, "Be able to send symbols, URLs and Email addresses");
        HigherCharacterLimit = Config.Bind("Chat", "HigherCharacterLimit", true, "Increase the character limit from 100 to 120");
        LowerChatCooldown = Config.Bind("Chat", "LowerChatCooldown", true, "Reduce the chat cooldown from 3s to 2.1s");
        ChatHistoryLimit = Config.Bind("Chat", "ChatHistoryLimit", 20, "The maximum amount of chat messages to keep in the chat history");
        NoCharacterLimit = Config.Bind("Unsafe", "NoCharacterLimit", false, "THESE ARE UNSAFE AND CAN GET YOU KICKED BY ANTI-CHEAT, USE WITH CAUTION\n\nNo character limit in chat");
        NoChatCooldown = Config.Bind("Unsafe", "NoChatCooldown", false, "No 3s cooldown between chat messages");

        // Reactor Version Patch
        ReactorCredits.Register(
            "ChatUs",
            DisplayVersion,
            false,
            ReactorCredits.AlwaysShow
        );

        // Patch Methods
        Harmony.PatchAll();
        Log.LogMessage("ChatUs Initialized.");
    }
}