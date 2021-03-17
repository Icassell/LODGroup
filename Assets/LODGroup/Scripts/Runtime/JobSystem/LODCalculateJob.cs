using Chess.LODGroupIJob.SpaceManager;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Chess.LODGroupIJob.JobSystem
{
    [BurstCompile(CompileSynchronously = true)]
    public struct LODCalculateJob : IJobParallelFor
    {
        [ReadOnly]
        public bool orthographic;
        [ReadOnly]
        public float orthographicSize;
        [ReadOnly]
        public float fieldOfView;
        [ReadOnly]
        public float lodBias;
        //��������
        [ReadOnly]
        public Vector3 camPosition;
        //centerת����������
        [ReadOnly]
        public NativeArray<Bounds> bounds;
        //����[x�������ռ�ȣ�y����������]
        public NativeArray<Vector2> result;
        public void Execute(int index)
        {
            result[index] = QuadTreeSpaceManager.SettingCameraJob(orthographic, orthographicSize, fieldOfView, lodBias, bounds[index], camPosition);
        }
    }
}
