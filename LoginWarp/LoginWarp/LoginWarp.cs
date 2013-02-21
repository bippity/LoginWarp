﻿using System;
using System.IO;
using Terraria;
using TShockAPI;
using TShockAPI.DB;
using TShockAPI.Hooks;
using Newtonsoft.Json;

namespace LoginWarpPlugin
{
    [APIVersion(1, 12)]

    public class LoginWarp : TerrariaPlugin
    {
        private Config Config = new Config();
        public override Version Version
        {
            get { return new Version("1.0"); }
        }
        public override string Name
        {
            get { return "LoginWarp"; }
        }
        public override string Author
        {
            get { return "Colin"; }
        }
        public override string Description
        {
            get { return "Warps player to warp on login"; }
        }

        public LoginWarp(Main game)
            : base(game)
        {
            Order = 10;
        }
        public override void Initialize()
        {
            PlayerHooks.PlayerLogin += onLogin;
            Hooks.GameHooks.Initialize += OnInitialize;
        }
        void OnInitialize()
		{
			string path = Path.Combine(TShock.SavePath, "login-warp.json");
			if (File.Exists(path))
			{
				Config = Config.Read(path);
			}
			Config.Write(path);
		}
        void onLogin(TShockAPI.Hooks.PlayerLoginEventArgs ply)
        {
           Warp warp = TShock.Warps.FindWarp(Config.Warp);
           ply.Player.Teleport((int)warp.WarpPos.X, (int)warp.WarpPos.Y + 3);
        }
    }
}