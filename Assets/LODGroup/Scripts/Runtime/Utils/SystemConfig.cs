using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Chess.LODGroupIJob.Utils
{
    public class Config : ScriptableObject
    {
        //��ʽ���ؿ�ͬʱ�����첽���ص���Դ����
        public int asynLoadNum = 4;

        //���������ռ��
        public float cullInterval = 0.1f;

        //�Ƿ��ڱ༭��ģʽGame��ͼ��������ʽ����
        public bool editorStream = false;
    }
    public class SystemConfig
    {
        static SystemConfig _Instance;
        public static SystemConfig Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SystemConfig();
                }
                return _Instance;
            }
        }
        Config m_Config;
        static string s_ConfigAdress = "Config/";
        static string s_Name = "chess";
        public void RefreshConfig()
        {
            m_Config = Resources.Load<Config>(s_ConfigAdress + s_Name);
            if (m_Config == null)
            {
                m_Config = ScriptableObject.CreateInstance<Config>();
                m_Config.name = s_Name;
                SaveUniqueConfigAsset(m_Config);
            }
        }
        public Config Config
        {
            get
            {
                if (m_Config == null)
                {
                    RefreshConfig();
                }
                return m_Config;
            }
            set
            {
                if (m_Config != null)
                    SaveUniqueConfigAsset(m_Config);
            }
        }

        void SaveUniqueConfigAsset(Object asset)
        {
#if UNITY_EDITOR
            var directory = string.Format("{0}{1}", "Assets/LODGroup/Resources/", s_ConfigAdress);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var path = directory + asset.name;
            path = Path.ChangeExtension(path, "asset");
            AssetDatabase.CreateAsset(asset, path);
#endif
        }
    }
}