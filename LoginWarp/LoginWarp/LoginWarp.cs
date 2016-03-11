﻿using System;
using System.IO;
using Terraria;
using TShockAPI;
using TShockAPI.DB;
using TShockAPI.Hooks;
using TerrariaApi;
using TerrariaApi.Server;
using Newtonsoft.Json;

namespace LoginWarpPlugin
{
	[ApiVersion(1, 22)]
	public class LoginWarp : TerrariaPlugin
	{
		private Config Config = new Config();
		public override Version Version
		{
			get { return new Version("1.3"); }
		}
		public override string Name
		{
			get { return "LoginWarp"; }
		}
		public override string Author
		{
			get { return "nicatronTg, Colin, SnirkImmington"; }
		}
		public override string Description
		{
			get { return "Warps player to warp on login"; }
		}

		public LoginWarp(Main game)
			: base(game)
		{
			OnInitialize();
		}
		public override void Initialize()
		{
			PlayerHooks.PlayerPreLogin += onLogin;
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
		
		void onLogin(PlayerPreLoginEventArgs ply)
		{
			Warp warp = TShock.Warps.Find(Config.Warp);
			if (warp != null)
			{
				if (ply.Player.Group.Name == Config.GroupName)
				{
					ply.Player.Teleport((int)warp.Position.X * 16, (int)warp.Position.Y * 16);
				}
			}
		}
	}
}
