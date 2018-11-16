using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKTest
{
    public class Game : GameWindow
    {
        List<TriangleFace> faces = CalculateMesh.SetTriangleFace();

        public static string imagePath;

        //Rotaition variables
        #region
        public static float rotation_speed_x = 0.0f;
        public static float rotation_speed_y = 0.0f;
        public static float rotation_speed_z = 0.0f;
        float angle_x = 0;
        float angle_y = 0;
        float angle_z = 0;
        #endregion

        public static int framerate = 60;

        protected override void OnLoad(EventArgs e)
        {
			base.OnLoad(e);

            //Set FPS cap
            TargetRenderFrequency = framerate;

            GL.ClearColor(Color.Black);

            //Enable
            #region
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Light0);

            //GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.LineSmooth);

            GL.Enable(EnableCap.Texture2D);
            #endregion

            //Set faces' normals
            try
            {
                faces = CalculateMesh.SetNormalVector(faces);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex + "\n Disabled lighting!");
                GL.Disable(EnableCap.Lighting);
            }

            try
            {
                faces = CalculateMesh.SetTextureCords(faces);
                CalculateMesh.LoadImage(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex + "\n Problem with texture cordinates!");
                GL.Disable(EnableCap.Texture2D);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            Rotate();

            CalculateMesh.DrawTriangleFaces(faces);

            this.SwapBuffers();

			Title = $"FPS: { 1f / e.Time:0}";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        void Rotate()
        {
            angle_x = ( rotation_speed_x / framerate) + angle_x;
            angle_y = ( rotation_speed_y / framerate) + angle_y;
            angle_z = ( rotation_speed_z / framerate) + angle_z;
            //System.Threading.Thread.Sleep(1);
            GL.Rotate(angle_x, 1f,0f,0f);
            GL.Rotate(angle_y, 0f,1f,0f);
            GL.Rotate(angle_z, 0f,0f,1f);
        }
    }
}