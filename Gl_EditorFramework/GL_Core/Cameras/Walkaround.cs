﻿using GL_EditorFramework.GL_Core;
using GL_EditorFramework.Interfaces;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinInput = System.Windows.Input;

namespace GL_EditorFramework.StandardCameras
{
    public class WalkaroundCamera : AbstractCamera
    {
        private readonly float maxCamMoveSpeed;

        public WalkaroundCamera(float maxCamMoveSpeed = 0.1f)
        {
            this.maxCamMoveSpeed = maxCamMoveSpeed;
        }

        public override uint MouseClick(MouseEventArgs e, GL_ControlBase control)
        {
            if (WinInput.Keyboard.IsKeyDown(WinInput.Key.LeftCtrl) &&
                e.Button == MouseButtons.Right &&
                control.PickingDepth != control.ZFar)
            {
                control.CameraTarget = -control.CoordFor(e.Location.X, e.Location.Y, control.PickingDepth);
            }
            base.MouseDown(e, control);
            return UPDATE_CAMERA;
        }

        public override uint MouseMove(MouseEventArgs e, Point lastMouseLoc, GL_ControlBase control)
        {
            float deltaX = e.Location.X - lastMouseLoc.X;
            float deltaY = e.Location.Y - lastMouseLoc.Y;

            if (e.Button == MouseButtons.Right)
            {
                if (!WinInput.Keyboard.IsKeyDown(WinInput.Key.Y))
                    control.RotateCameraX(deltaX * 0.002f);
                if (!WinInput.Keyboard.IsKeyDown(WinInput.Key.X))
                    control.CamRotY += deltaY * 0.002f;
                return UPDATE_CAMERA;
            }
            else if (e.Button == MouseButtons.Left)
            {
                base.MouseMove(e, lastMouseLoc, control);

                if (WinInput.Keyboard.IsKeyDown(WinInput.Key.LeftCtrl))
                {
                    float delta = ((float)deltaY*-5 * Math.Min(0.01f, depth / 500f));
                    Vector3 vec;
                    vec.X = 0;
                    vec.Y = 0;
                    vec.Z = delta;

                    control.CameraTarget -= Vector3.Transform(control.InvertedRotationMatrix, vec);
                }
                else
                {

                    //code from Whitehole

                    Vector3 vec;
                    if (!WinInput.Keyboard.IsKeyDown(WinInput.Key.Y))
                        vec.X = deltaX * Math.Min(maxCamMoveSpeed, depth * control.FactorX);
                    else
                        vec.X = 0;
                    if (!WinInput.Keyboard.IsKeyDown(WinInput.Key.X))
                        vec.Y = -deltaY * Math.Min(maxCamMoveSpeed, depth * control.FactorY);
                    else
                        vec.Y = 0;

                    vec.Z = 0;
                    control.CameraTarget -= Vector3.Transform(control.InvertedRotationMatrix, vec);
                }

                return UPDATE_CAMERA;
            }
            return 0;
        }

        public override uint MouseWheel(MouseEventArgs e, GL_ControlBase control)
        {
            depth = control.PickingDepth;
            float delta = ((float)e.Delta * Math.Min(0.01f, depth / 500f));
            Vector3 vec;

            Vector2 normCoords = control.NormMouseCoords(e.Location.X, e.Location.Y);

            vec.X = (-normCoords.X * delta) * control.FactorX;
            vec.Y = ( normCoords.Y * delta) * control.FactorY;
            vec.Z = delta;

            control.CameraTarget -= Vector3.Transform(control.InvertedRotationMatrix, vec);
            
            return UPDATE_CAMERA;
        }
    }
}
