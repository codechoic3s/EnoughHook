using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHook.CFG
{
    public class ConfigManager
    {
        public Config Current;
        public List<Config> Configs;

        private string _Path;

        public ConfigManager(string path)
        {
            _Path = path;
            Current = new Config();
        }

        public void Refresh()
        {
            var files = Directory.GetFiles(_Path);
            var fco = files.Length;
            for (var i = 0; i < fco; i++)
            {
                if (files[i].EndsWith(".kcf"))
                {
                    var read = File.ReadAllBytes(_Path + files[i]);
                    var parsed = Parse(read);
                    if (!Configs.Contains(parsed))
                    {
                        Configs.Add(parsed);
                    }
                }
            }
        }

        /*
        private bool IsContains(Config cfg)
        {
            var cco = Configs.Count;
            for (var i = 0; i < cco; i++)
            {
                var cc = Configs[i];
                if (cc.AutoFire == cfg.AutoFire && cc.Movement == cfg.AutoFire)
                {
                    return true;
                }
            }
        }
        */

        private Config Parse(byte[] data)
        {
            Config cfg = new Config();

            return cfg;
        }
    }
}
