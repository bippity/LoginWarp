using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using Newtonsoft.Json;

namespace LoginWarpPlugin
{
    public class Config
    {
        public string Warp = "spawn";

        public void Write(string path)
        {
            //File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
            File.WriteAllText("Name of warp:spawn");
        }

        public static Config Read(string path)
        {
            if (!File.Exists(path))
            {
                return new Config();
            }
            //return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
            var text = File.ReadAllText(path);
            return text.Split(':')[1].TrimStart(' ');
        }
    }
}
