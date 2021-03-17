using UnityEditor;
using UnityEngine;
namespace Chess.LODGroupIJob.Slider
{
    public class SlideCursor
    {
        //�Ƿ�ʼ����
        bool m_Slide = false;

        //��ʼλ��ƫ��
        float m_XOffset = -13;

        //ָ���ڿ���λ��[0-1]
        float m_RelativeHeight = 0.9f;
        //���Icon
        static Texture2D s_CameraIcon;
        public static Texture2D S_CameraFrame
        {
            get
            {
                if (s_CameraIcon == null)
                    s_CameraIcon = AssetDatabase.LoadAssetAtPath("Assets/LODGroup/Images/cameraIcon.png", typeof(Texture2D)) as Texture2D;
                return s_CameraIcon;
            }
        }

        public float RelativeHeight
        {
            get
            {
               return Mathf.Pow(1 - m_RelativeHeight,2);
            }
            set
            {
                m_RelativeHeight = Mathf.Clamp(value, 0, 1);
                if(!m_Slide)
                    m_RelativeHeight = 1 - Mathf.Sqrt(m_RelativeHeight);
                
            }
        }
        public bool Slide { get => m_Slide; }

        public void Updata(Event curEvent)
        {
            if (curEvent == null)
                return;
            switch (curEvent.type)
            {
                //����������Inspector��Χ
                case EventType.MouseLeaveWindow:
                    m_Slide = false;
                    break;
               
            }
        }
        public void Draw(Rect sliderBarPosition)
        {
            if (sliderBarPosition.x == 0 && sliderBarPosition.y == 0)
                return;

            Event evt = Event.current;

            //���ݰٷֱȻ�ԭָ��λ��
                
            float pos = sliderBarPosition.width * m_RelativeHeight + sliderBarPosition.x;
            var slideRect = new Rect(pos + m_XOffset, sliderBarPosition.y - sliderBarPosition.height + 5, 40, 25);

            switch (evt.type)
            {
                case EventType.MouseDown:
                    if(slideRect.Contains(evt.mousePosition))
                    {
                        m_Slide = true;
                    }
                    break;
                case EventType.MouseDrag:
                    if(m_Slide)
                    {
                        //���ң�����ָ���ڿ��ڵİٷֱ�
                        float r = (evt.mousePosition.x - sliderBarPosition.x) / sliderBarPosition.width;
                        if(r < 0.99f)
                        {
                            RelativeHeight = r;
                        }
                        evt.Use();
                    }
                    break;
                case EventType.MouseUp:
                    m_Slide = false;
     
                    break;
            }
            //���ICON
            GUI.Label(slideRect, S_CameraFrame);
            //ָ��
            slideRect = new Rect(slideRect.x + 12.5f, sliderBarPosition.y, 1.4f, sliderBarPosition.height);
            EditorGUI.DrawRect(slideRect, new Color(0.35f, 0.35f, 0.35f, 1));
            //��ʾ�ٷֱ�
            slideRect = new Rect(slideRect.x , sliderBarPosition.y + sliderBarPosition.height, 35, 20);
            EditorGUI.LabelField(slideRect, string.Format("{0}%", (int)(RelativeHeight * 100.0f)));
        } 
    }
}

