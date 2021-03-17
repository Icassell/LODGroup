using Chess.LODGroupIJob.Utils;
using UnityEditor;
using UnityEngine;
namespace Chess.LODGroupIJob.Config
{
    public class ConfigWindow : EditorWindow
    {
        [MenuItem("Chess/LODGroupConfig")]
        public static void Init()
        {
            Rect wr = new Rect(0, 0, 200, 80);
            var windows = (ConfigWindow)EditorWindow.GetWindowWithRect(typeof(ConfigWindow), wr, true, "LODGroupConfig");
            windows.Show();
        }
        private void OnGUI()
        {
            var config = SystemConfig.Instance.Config;
            config.asynLoadNum = EditorGUILayout.IntField("ͬʱ�첽��������", config.asynLoadNum);
            config.cullInterval = EditorGUILayout.FloatField("���ʱ�¼�����ռ��", config.cullInterval);
            EditorGUI.BeginChangeCheck();
            config.editorStream = EditorGUILayout.Toggle("�༭����������ʽ����", config.editorStream);
            if(EditorGUI.EndChangeCheck())
            {
                if (config.editorStream)
                    return;

                //�رձ༭������ʽ������ʽ���ص���Դȫ��ɾ��
                var lodGroups = GameObject.FindObjectsOfType<LODGroup>();
                if (lodGroups == null)
                    return;
                foreach(var g in lodGroups)
                {
                    foreach(var lod in g.GetLODs())
                    {
                        if(lod.Handle != null && lod.Handle.Result != null)
                            GameObject.DestroyImmediate(lod.Handle.Result);
                    }
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}