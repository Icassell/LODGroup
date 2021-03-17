using Chess.LODGroupIJob.Streaming;
using Chess.LODGroupIJob.Utils;
using System;
using UnityEngine;

namespace Chess.LODGroupIJob
{
    public enum State
    {
        None,
        UnLoaded,
        Loading,
        Loaded,
        Failed
    }

    //��ʹ�ü̳�ԭ��������޷����л�����ScriptableObject���ڿ�����ʱ�����ò����鷳
    [Serializable]
    public sealed class LOD
    {
        //����Ļ��ռ�ȸ߶�[0-1]
        [SerializeField]
        private float screenRelativeTransitionHeight;
        //��ǰ�����Renderer
        [SerializeField]
        public Renderer[] m_Renderers;
        //��ǰ״̬
        [SerializeField]
        private State m_CurrentState;
        //��һ֡״̬
        [SerializeField]
        private State m_LastState;
        #region ��ʽ����
        //�Ƿ���ʽ

        [SerializeField]
        private bool m_Streaming;
        [SerializeField]
        private string address;
        [SerializeField]
        private int priority;
        [SerializeField]
        private Handle handle;
        #endregion
        public LOD(float screenRelative)
        {
            screenRelativeTransitionHeight = screenRelative;
            m_CurrentState = State.None;
        }
        public Renderer[] Renderers { get => m_Renderers; set => m_Renderers = value; }
        public State CurrentState { get => m_CurrentState; set => m_CurrentState = value; }
        public State LastState { get => m_LastState; set => m_LastState = value; }
        public bool Streaming { get => m_Streaming; set => m_Streaming = value; }
        public float ScreenRelativeTransitionHeight { get => screenRelativeTransitionHeight; set => screenRelativeTransitionHeight = value; }
        public string Address { get => address; set => address = value; }
        public int Priority { get => priority; set => priority = value; }
        public Handle Handle { get => handle; set => handle = value; }
        
        //����true��ʾ�ռ�����ɣ����򷵻�false
        public void SetState(bool active, LODGroup lodGroup, float distance, CameraType type)
        {
            if(m_Streaming)
            {
#if UNITY_EDITOR
                //�༭��ģʽ����������ʽ������ôҲ��Ч
                if (!Application.isPlaying && (type != CameraType.Game || !SystemConfig.Instance.Config.editorStream))
                {
                    return;
                }
#endif
                StreamingLOD.SetState(active, this, lodGroup, distance, type);
            }
            else
            {
                NormalLOD.SetState(active, this, lodGroup);
            } 
        }
    }
}
