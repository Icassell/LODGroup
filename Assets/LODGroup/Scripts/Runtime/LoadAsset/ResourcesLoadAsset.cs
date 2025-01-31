using Chess.LODGroupIJob.Interface;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Chess.LODGroupIJob.LoadAsset
{
    [ExecuteInEditMode]
    public class ResourcesLoadAsset : MonoBehaviour
    {
        LoadAsset m_LoadAsset;

        private void Start()
        {
            m_LoadAsset = new LoadAsset();
        }
    }

    public class LoadAsset : ILoadAsset
    {
        uint id = 0;

        HashSet<uint> m_AllObjs = new HashSet<uint>();

        public override uint LoadAsync(string address, int priority, float distance, Action<uint, GameObject> action)
        {
            return 0;
        }

        public override uint LoadAsync(string address, Action<uint, GameObject> action)
        {
            id++;
            //用Resources测试
            // address = address.Replace("Assets/LODGroup/Resources/", "");
            // address = address.Replace(".prefab", "");
            //  var request = Resources.LoadAsync<GameObject>(address);

            //用Addr测试
            var request = Addressables.LoadAssetAsync<GameObject>(address);
            request.Completed += h => { action?.Invoke(id, request.Result as GameObject); };
            m_AllObjs.Add(id);
            return id;
        }

        public override bool UnloadAsset(uint id)
        {
            return m_AllObjs.Remove(id);
        }
    }
}