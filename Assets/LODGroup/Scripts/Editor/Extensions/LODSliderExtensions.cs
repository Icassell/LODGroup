using UnityEngine;
using Chess.LODGroupIJob.Slider;
using System.Collections.Generic;
namespace Chess.LODGroupIJob.Extensions
{
    public static class LODSliderEditorExtensions
    {
        //���LOD,userData = Vector2(��ѡ�е�lod�±꣬ѡ�е�λ��)
        public static void InsertBeforeOnClick(this LODSlider lodSlider, object userData)
        {
            var lodGroupEditor = lodSlider.LODGroupEditor;
            var lodGroup = lodGroupEditor.LODGroup;
            var lods = new  List<LOD>(lodGroup.GetLODs());

            Vector2 data = (Vector2)userData;
            
            LOD lod = new LOD(data.y);
            if(data.x == -1)
            {
                lod.Priority = lods.Count - 1;
                lods.Insert(lods.Count, lod);
            }
            else
            {
                lod.Priority = (int)data.x;
                lods.Insert((int)data.x, lod);
                lodSlider.SelectedShowIndex += 1;
               
            }
            lodGroup.SetLODs(lods.ToArray());
            lodGroupEditor.RefreshLOD();




        }
        //ɾ��LOD�� userData = int����ѡ�е��±�
        public static void DeleteOnClick(this LODSlider lodSlider, object userData)
        {
            var lodGroupEditor = lodSlider.LODGroupEditor;
            var lodGroup = lodGroupEditor.LODGroup;
            var lods = new List<LOD>(lodGroup.GetLODs());

            int index = (int)userData;
            lods.RemoveAt(index);
            lodGroup.SetLODs(lods.ToArray());
            lodGroupEditor.RefreshLOD();
            if(lodSlider.SelectedShowIndex != -1)
            {
                lodSlider.SelectedShowIndex = Mathf.Clamp(index - 1, 0, index - 1);
            }

        }
    }
}