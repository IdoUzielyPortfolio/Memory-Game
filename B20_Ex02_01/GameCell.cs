using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02_01
{
    public struct GameCell
    {
        private int m_Value;//indexs of the array
        private bool m_IsVisible;
        
        public GameCell(int i_Value)
        {
            m_Value = i_Value;
            m_IsVisible = false;
        }
        public bool IsVisible
        {
            get
            {
                return m_IsVisible;
            }
            set
            {
                m_IsVisible = value;
            }
        }
        public int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

    }
}
