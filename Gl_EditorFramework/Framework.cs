﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GL_EditorFramework.GL_Core;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GL_EditorFramework
{
    public sealed class Framework {
        public static float TWO_PI = (float)Math.PI*2;
        public static float PI = (float)Math.PI;
        public static float HALF_PI = (float)Math.PI/2;

        public static bool ShowShaderErrors = false;

        public delegate void ListEventHandler(object sender, ListEventArgs e);

        public class ListEventArgs : EventArgs
        {
            public IList List { get; set; }
            public ListEventArgs(IList list)
            {
                List = list;
            }
        }

        public static Matrix3 Mat3FromEulerAnglesDeg(Vector3 eulerAngles) =>
            Extensions.CreateRotationX(Math.PI * eulerAngles.X / 180.0) *
            Extensions.CreateRotationY(Math.PI * eulerAngles.Y / 180.0) *
            Extensions.CreateRotationZ(Math.PI * eulerAngles.Z / 180.0);

        public static void Initialize()
        {
            if (initialized)
                return;

            //string GetCondition(int row, Vector3 vec)
            //{
            //    if (vec.X != 0)
            //        return $"mtx.M{row}1=={vec.X}";
            //    else if (vec.Y != 0)
            //        return $"mtx.M{row}2=={vec.Y}";
            //    else
            //        return $"mtx.M{row}3=={vec.Z}";
            //}

            //string edgeCases = "";

            //for (float x = -90; x <= 180; x+=90)
            //{
            //    for (float y = -90; y <= 180; y += 90)
            //    {
            //        for (float z = -90; z <= 180; z += 90)
            //        {
            //            var mat = Mat3FromEulerAnglesDeg(new Vector3(x, y, z));

            //            var mat2 = Mat3FromEulerAnglesDeg(mat.ExtractDegreeEulerAngles());

            //            if (mat != mat2)
            //            {
            //                edgeCases += $"if({GetCondition(1,mat.Row0)} && {GetCondition(2, mat.Row1)} && {GetCondition(3, mat.Row2)})\n";
            //                edgeCases += $"    return new Vector3({x},{y},{z});\n";
            //            }
            //        }
            //    }
            //}

            //texture sheet
            TextureSheet = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureSheet);

            var bmp = Properties.Resources.TextureSheet;
            var bmpData = bmp.LockBits(
                new System.Drawing.Rectangle(0, 0, 128*4, 128*2),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, 128*4, 128*2, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            bmp.UnlockBits(bmpData);
            
            initialized = true;
        }
        private static bool initialized = false;
        public static int TextureSheet;
    }





    public static class Extensions
    {
        public static Vector3 ExtractDegreeEulerAngles(this Matrix3 mtx)
        {
            #region generated code
            if (mtx.M13 == 1 && mtx.M22 == -1 && mtx.M31 == 1)
                return new Vector3(-90, -90, -90);
            if (mtx.M13 == 1 && mtx.M21 == 1 && mtx.M32 == 1)
                return new Vector3(-90, -90, 0);
            if (mtx.M13 == 1 && mtx.M21 == -1 && mtx.M32 == -1)
                return new Vector3(-90, -90, 180);
            if (mtx.M13 == -1 && mtx.M21 == -1 && mtx.M32 == 1)
                return new Vector3(-90, 90, 0);
            if (mtx.M13 == -1 && mtx.M22 == -1 && mtx.M31 == -1)
                return new Vector3(-90, 90, 90);
            if (mtx.M13 == -1 && mtx.M21 == 1 && mtx.M32 == -1)
                return new Vector3(-90, 90, 180);
            if (mtx.M13 == 1 && mtx.M21 == 1 && mtx.M32 == 1)
                return new Vector3(0, -90, -90);
            if (mtx.M13 == 1 && mtx.M21 == -1 && mtx.M32 == -1)
                return new Vector3(0, -90, 90);
            if (mtx.M13 == 1 && mtx.M22 == -1 && mtx.M31 == 1)
                return new Vector3(0, -90, 180);
            if (mtx.M13 == -1 && mtx.M21 == 1 && mtx.M32 == -1)
                return new Vector3(0, 90, -90);
            if (mtx.M13 == -1 && mtx.M21 == -1 && mtx.M32 == 1)
                return new Vector3(0, 90, 90);
            if (mtx.M13 == 1 && mtx.M21 == -1 && mtx.M32 == -1)
                return new Vector3(90, -90, 0);
            if (mtx.M13 == 1 && mtx.M22 == -1 && mtx.M31 == 1)
                return new Vector3(90, -90, 90);
            if (mtx.M13 == 1 && mtx.M21 == 1 && mtx.M32 == 1)
                return new Vector3(90, -90, 180);
            if (mtx.M13 == -1 && mtx.M22 == -1 && mtx.M31 == -1)
                return new Vector3(90, 90, -90);
            if (mtx.M13 == -1 && mtx.M21 == 1 && mtx.M32 == -1)
                return new Vector3(90, 90, 0);
            if (mtx.M13 == -1 && mtx.M21 == -1 && mtx.M32 == 1)
                return new Vector3(90, 90, 180);
            if (mtx.M13 == 1 && mtx.M21 == -1 && mtx.M32 == -1)
                return new Vector3(180, -90, -90);
            if (mtx.M13 == 1 && mtx.M22 == -1 && mtx.M31 == 1)
                return new Vector3(180, -90, 0);
            if (mtx.M13 == 1 && mtx.M21 == 1 && mtx.M32 == 1)
                return new Vector3(180, -90, 90);
            if (mtx.M13 == -1 && mtx.M21 == -1 && mtx.M32 == 1)
                return new Vector3(180, 90, -90);
            if (mtx.M13 == -1 && mtx.M22 == -1 && mtx.M31 == -1)
                return new Vector3(180, 90, 0);
            if (mtx.M13 == -1 && mtx.M21 == 1 && mtx.M32 == -1)
                return new Vector3(180, 90, 90);
            if (mtx.M13 == -1 && mtx.M22 == 1 && mtx.M31 == 1)
                return new Vector3(180, 90, 180);
            #endregion

            return new Vector3(
                        (float)(180 * Math.Atan2(mtx.M23, mtx.M33) / Math.PI),
                        (float)(-180 * Math.Asin(mtx.M13) / Math.PI),
                        (float)(180 * Math.Atan2(mtx.M12, mtx.M11) / Math.PI));
        }

        public static Matrix3 CreateRotationX(double angle)
        {
            Matrix3 result;
            float cos = (float)(Math.Round(Math.Cos(angle)*256f)/256.0);
            float sin = (float)(Math.Round(Math.Sin(angle)*256f)/256.0);

            result.Row0 = Vector3.UnitX;
            result.Row1 = new Vector3(0.0f, cos, sin);
            result.Row2 = new Vector3(0.0f, -sin, cos);
            return result;
        }

        public static Matrix3 CreateRotationY(double angle)
        {
            Matrix3 result;
            float cos = (float)(Math.Round(Math.Cos(angle) * 256f) / 256.0);
            float sin = (float)(Math.Round(Math.Sin(angle) * 256f) / 256.0);

            result.Row0 = new Vector3(cos, 0.0f, -sin);
            result.Row1 = Vector3.UnitY;
            result.Row2 = new Vector3(sin, 0.0f, cos);
            return result;
        }

        public static Matrix3 CreateRotationZ(double angle)
        {
            Matrix3 result;
            float cos = (float)(Math.Round(Math.Cos(angle) * 256f) / 256.0);
            float sin = (float)(Math.Round(Math.Sin(angle) * 256f) / 256.0);

            result.Row0 = new Vector3(cos, sin, 0.0f);
            result.Row1 = new Vector3(-sin, cos, 0.0f);
            result.Row2 = Vector3.UnitZ;
            return result;
        }
    }
}
