using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace LODGroup.Scripts.UI
{
    public class ChangeScene : MonoBehaviour
    {
        private void Awake()
        {
            //Addressables.InitializeAsync();
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        void UnloadAssets(AsyncOperationHandle<SceneInstance> asyncOperationHandle)
        {
            Resources.UnloadUnusedAssets();
            Debug.Log("Clear Done!");
        }

        public void ChangeToBaseScene()
        {
            var handle = Addressables.LoadSceneAsync("01Base", LoadSceneMode.Single, true);
            handle.Completed += UnloadAssets;
        }

        public void ChangeToStreamingScene()
        {
            var handle = Addressables.LoadSceneAsync("02Streaming", LoadSceneMode.Single, true);
            handle.Completed += UnloadAssets;
        }

        public void ChangeToBlendScene()
        {
            var handle = Addressables.LoadSceneAsync("03Blend", LoadSceneMode.Single, true);
            handle.Completed += UnloadAssets;
        }

        public void ChangeToStreamExample()
        {
            var handle = Addressables.LoadSceneAsync("04StreamExample", LoadSceneMode.Single, true);
            handle.Completed += UnloadAssets;
        }
    }
}