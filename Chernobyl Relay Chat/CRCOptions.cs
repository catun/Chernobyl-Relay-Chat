﻿using Chernobyl_Relay_Chat.Properties;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

namespace Chernobyl_Relay_Chat
{
    class CRCOptions
    {
        private static RegistryKey registry = Registry.CurrentUser.CreateSubKey(@"Software\Chernobyl Relay Chat");

        public const string Server = "irc.slashnet.org";
#if DEBUG
        public const string Channel = "#crc_debug";
#else
        public const string Channel = "#crc_beta";
#endif
        public const string InPath = @"\..\gamedata\configs\crc_input.txt";
        public const string OutPath = @"\..\gamedata\configs\crc_output.txt";

        public static Point DisplayLocation;
        public static Size DisplaySize;
        public static bool AutoFaction;
        public static string GameFaction;
        public static string ManualFaction;
        public static string Name;
        public static bool SendDeath;
        public static bool ReceiveDeath;
        public static int DeathInterval;

        public static string GetIrcName()
        {
            return Name.Replace(' ', '_');
        }

        public static string GetFaction()
        {
            if (AutoFaction)
                return GameFaction;
            else
                return ManualFaction;
        }

        public static bool Load()
        {
            try
            {
                DisplayLocation = new Point((int)registry.GetValue("DisplayLocationX", 0),
                    (int)registry.GetValue("DisplayLocationY", 0));
                DisplaySize = new Size((int)registry.GetValue("DisplayWidth", 0),
                    (int)registry.GetValue("DisplayHeight", 0));
                AutoFaction = Convert.ToBoolean((string)registry.GetValue("AutoFaction", "True"));
                GameFaction = (string)registry.GetValue("GameFaction", "actor_stalker");
                ManualFaction = (string)registry.GetValue("ManualFaction", "actor_stalker");
                Name = (string)registry.GetValue("Name", CRCStrings.RandomName(GetFaction()));
                SendDeath = Convert.ToBoolean((string)registry.GetValue("SendDeath", "True"));
                ReceiveDeath = Convert.ToBoolean((string)registry.GetValue("ReceiveDeath", "True"));
                DeathInterval = (int)registry.GetValue("DeathInterval", 60);

                Save();
                return true;
            }
            catch(Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
            {
                return false;
            }
        }

        public static void Save()
        {
            registry.SetValue("DisplayLocationX", DisplayLocation.X);
            registry.SetValue("DisplayLocationY", DisplayLocation.Y);
            registry.SetValue("DisplayWidth", DisplaySize.Width);
            registry.SetValue("DisplayHeight", DisplaySize.Height);
            registry.SetValue("AutoFaction", AutoFaction);
            registry.SetValue("GameFaction", GameFaction);
            registry.SetValue("ManualFaction", ManualFaction);
            registry.SetValue("Name", Name);
            registry.SetValue("SendDeath", SendDeath);
            registry.SetValue("ReceiveDeath", ReceiveDeath);
            registry.SetValue("DeathInterval", DeathInterval);
        }
    }
}