using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKTest
{
    class Game : GameWindow
    {
        List<TriangleFace> faces = CalculateMesh.SetTriangleFace();

        public static float rotation_speed = 90.0f;
        float angle;

        public static int framerate = 60;

        protected override void OnLoad(EventArgs e)
        {
			base.OnLoad(e);



            //Set FPS cap
            TargetRenderFrequency = framerate;

            GL.ClearColor(Color.Black);

            //Enable
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Light0);

            //Set faces' normals
            try
            {
                faces = CalculateMesh.SetNormalVector(faces);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex + " Lighting disabled!");
                GL.Enable(EnableCap.Lighting);
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
            angle += rotation_speed * 1/60;
            System.Threading.Thread.Sleep(1);
            GL.Rotate(angle, 1.0f, 1.0f, 1.0f);
        }
    }
}