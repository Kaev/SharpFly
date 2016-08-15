using System;
using System.Collections.Generic;

namespace SharpFly_Utility_Library.Configuration
{
    public abstract class Config
    {
        private IniFile m_IniFile;
        private Dictionary<string, object> m_Settings;

        public Config(string path)
        {
            m_IniFile = new IniFile(path);
            if (String.IsNullOrEmpty(m_IniFile.Path))
                throw new ArgumentNullException(String.Format("{0} doesn't exist!", path));
            m_Settings = new Dictionary<string, object>();
        }

        public void Read(string name, string configName, string configSection, Type type)
        {
            if (!this.m_Settings.ContainsKey(name))
                this.m_Settings.Add(name, Convert.ChangeType(m_IniFile.Read(configName, configSection), type));
        }

        public object GetSetting(string name)
        {
            if (this.m_Settings.ContainsKey(name))
                return this.m_Settings[name];
            throw new ArgumentNullException(String.Format("There isn't a setting with the name {0}", name));
        }
    }
}
