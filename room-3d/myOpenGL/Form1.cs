using System;
using System.Drawing;
using System.Windows.Forms;
using OpenGL;

namespace myOpenGL
{
    public partial class Form1 : Form
    {
        private bool m_IsMiddleMouseDown = false;
        private Point m_AnchorMousePosition;
        cOGL cGL;

        public Form1()
        {

            InitializeComponent();
            cGL = new cOGL(panel1);
            //apply the bars values as cGL.ScrollValue[..] properties 
                                         //!!!
            hScrollBarScroll(hScrollBar1, null);
            hScrollBarScroll(hScrollBar2, null);
            hScrollBarScroll(hScrollBar3, null);
            hScrollBarScroll(hScrollBar4, null);
            hScrollBarScroll(hScrollBar5, null);
            hScrollBarScroll(hScrollBar6, null);
            hScrollBarScroll(hScrollBar7, null);
            hScrollBarScroll(hScrollBar8, null);
            hScrollBarScroll(hScrollBar9, null);

            this.KeyPreview = true;

            this.KeyPress += Form1_KeyPress;
            panel1.MouseDown += Panel_MouseDown;
            panel1.MouseMove += Panel_MouseMove;
            panel1.MouseUp += Panel_MouseUp;
            panel1.MouseWheel += Panel_MouseWheel;
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                cGL.zShift += 100f;
                cGL.intOptionC = 6;
            }
            else if (e.Delta < 0)
            {
                cGL.zShift -= 100f;
                cGL.intOptionC = -6;
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                m_IsMiddleMouseDown = true;
                m_AnchorMousePosition = e.Location;
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_IsMiddleMouseDown)
            {
                float deltaX = (e.X - m_AnchorMousePosition.X) / 20f;
                float deltaY = (e.Y - m_AnchorMousePosition.Y) / 20f;

                cGL.ScrollValue[0] = deltaX;
                cGL.ScrollValue[1] = deltaY;
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                m_IsMiddleMouseDown = false;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            switch (char.ToUpper(e.KeyChar))
            {
                case 'P':
                    cGL.isCeilingLightBulbOn = !cGL.isCeilingLightBulbOn;
                    break;
                case 'L':
                    cGL.applyShadows = !cGL.applyShadows;
                    break;
                case 'K':
                    cGL.m_Closet.OpenAllObjects();
                    break;
                case 'M':
                    cGL.m_Closet.CloseAllObjects();
                    break;
                case 'H':
                    cGL.m_Closet.SwitchSelectedObject();
                    break;
                case 'N':
                    cGL.m_Closet.UnselectObjects();
                    break;
                case 'U':
                    cGL.m_Closet.OpenSelectedObject();
                    break;
                case 'Y':
                    cGL.m_Closet.CloseSelectedObject();
                    break;
                case 'F':
                    cGL.m_Football.MoveForward();
                    break;
                case 'B':
                    cGL.m_Football.MoveBackwards();
                    break;
                case 'R':
                    cGL.ReflectiveFloor = !cGL.ReflectiveFloor;
                    break;
                case 'E':
                    cGL.ReflectiveMirror = !cGL.ReflectiveMirror;
                    break;
            }
        }


        private void panel1_Resize(object sender, EventArgs e)
        {
            if (cGL != null)
            {
                cGL.OnResize();
            }
        }

        private void hScrollBarScroll(object sender, ScrollEventArgs e)
        {
            cGL.intOptionC = 0;
            HScrollBar hb = (HScrollBar)sender;
            int n = int.Parse(hb.Name.Substring(hb.Name.Length - 1));
            cGL.ScrollValue[n - 1] = (hb.Value - 100) / 10.0f;
        }

        public float[] oldPos = new float[7];

        private void numericUpDownValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nUD = (NumericUpDown)sender;
            int i = int.Parse(nUD.Name.Substring(nUD.Name.Length - 1));
            int pos = (int)nUD.Value;
            switch(i)
            {
                case 1:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.xShift += 0.25f;
                        cGL.intOptionC = 4;
                    }
                    else
                    {
                        cGL.xShift -= 0.25f;
                        cGL.intOptionC = -4;
                    }
                    break;
                case 2:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.yShift += 0.25f;
                        cGL.intOptionC = 5;
                    }
                    else
                    {
                        cGL.yShift -= 0.25f;
                        cGL.intOptionC = -5;
                    }
                    break;
                case 3:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.zShift += 0.25f;
                        cGL.intOptionC = 6;
                    }
                    else
                    {
                        cGL.zShift -= 0.25f;
                        cGL.intOptionC = -6;
                    }
                    break;
                case 4:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.xAngle += 5;
                        cGL.intOptionC = 1;
                    }
                    else
                    {
                        cGL.xAngle -= 5;
                        cGL.intOptionC = -1;
                    }
                    break;
                case 5:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.yAngle += 5;
                        cGL.intOptionC = 2;
                    }
                    else
                    {
                        cGL.yAngle -= 5;
                        cGL.intOptionC = -2;
                    }
                    break;
                case 6: 
	                if (pos>oldPos[i-1]) 
	                {
		                cGL.zAngle+=5;
		                cGL.intOptionC=3;
	                }
	                else
	                {
                        cGL.zAngle -= 5;
                        cGL.intOptionC = -3;
                    }
                    break;
            }

            oldPos[i - 1] = pos;

        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            cGL.Draw();
        }

        private void lightXPos_Scroll(object sender, ScrollEventArgs e)
        {
            HScrollBar xPosScroller = (HScrollBar)sender;
            cGL.lightPos[0] = xPosScroller.Value;
        }

        private void lightYPos_Scroll(object sender, ScrollEventArgs e)
        {
            HScrollBar xPosScroller = (HScrollBar)sender;
            cGL.lightPos[1] = xPosScroller.Value;
        }

        private void lightZPos_Scroll(object sender, ScrollEventArgs e)
        {
            HScrollBar xPosScroller = (HScrollBar)sender;
            cGL.lightPos[2] = xPosScroller.Value;
        }
    }
}