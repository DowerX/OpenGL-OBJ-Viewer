﻿using System.Collections.Generic;
using System.IO;
using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Globalization;

namespace OpenTKTest
{
    class CalculateMesh
    {
        public static string[] lines = null;

        public static void GetFile(string filePath)
        {
            string[] _lines = File.ReadAllLines(filePath);
            lines = _lines;
        }

        public static List<Vertex> SetVertices()
        {
            List<Vertex> vertices = new List<Vertex>();
            foreach (string line in lines)
            {
                if (line[0].ToString() == "v" & line[1].ToString() == " ")
                {
                    string space = " ";
                    string[] parts = line.ToString().Split(space.ToCharArray());

                    //Create new vertex
                    Vertex _vertex = new Vertex
                    {
                        x = float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture),
                        y = float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture),
                        z = float.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture)
                    };
                    //Add new vertex to vertices
                    vertices.Add(_vertex);
                }
            }

            Console.WriteLine("Number of vertices: " + vertices.Count);
            return (vertices);
        }

        //public static List<QuadFace> SetQuadFace()
        //{
        //    List<QuadFace> faces = new List<QuadFace>();
        //    List<Vertex> _vertices = new List<Vertex>();
        //    _vertices = SetVertices();

        //    foreach (string line in lines)
        //    {
        //        if (line[0].ToString() == "f" & line[1].ToString() == " ")
        //        {
        //            string space = " ";
        //            string[] parts = line.ToString().Split(space.ToCharArray());

        //            Vertex _vertex = new Vertex();

        //            QuadFace _face = new QuadFace();

        //            int vertexNum = 0;


        //            foreach (string part in parts)
        //            {
        //                if (part[0].ToString() != "f")
        //                {
        //                    int point;

        //                    string slash = "//";
        //                    string[] bits = part.Split(slash.ToCharArray());

        //                    point = Convert.ToInt16(bits[0].ToString());
        //                    --point;


        //                    _vertex = _vertices[point];
        //                    _face.point[vertexNum] = _vertex;
        //                    ++vertexNum;
        //                    if (vertexNum > 3)
        //                        vertexNum = 0;
        //                }
        //            }
        //            faces.Add(_face);
        //        }
        //    }
        //    return (faces);
        //}

        public static List<TriangleFace> SetTriangleFace()
        {
            List<TriangleFace> faces = new List<TriangleFace>();

            List<Vertex> _vertices = new List<Vertex>();
            _vertices = SetVertices();

            foreach (string line in lines)
            {
                if (line[0].ToString() == "f" & line[1].ToString() == " ")
                {
                    string space = " ";
                    string[] parts = line.ToString().Split(space.ToCharArray());
                    
                    Vertex _vertex = new Vertex();

                    TriangleFace _face = new TriangleFace();

                    int vertexNum = 0;

                    foreach (string part in parts)
                    {
                        if (part[0].ToString() != "f")
                        {
                            string[] bits = new string[2];
                            string slash = "/";
                            bits = part.Split(slash.ToCharArray());
                            int point = Convert.ToInt16(bits[0].ToString());
                            int normal = Convert.ToInt16(bits[bits.Length - 1].ToString());

                            --point;
                            --normal;

                            _vertex = _vertices[point];
                            _face.point[vertexNum] = _vertex;

                            _face.normal = normal;

                            #region
                            ++vertexNum;
                            if (vertexNum > 2)
                            {
                                vertexNum = 0;
                            }
                            #endregion
                        }
                    }
                    faces.Add(_face);
                }
            }

            Console.WriteLine("Number of triangle faces: " + faces.Count);
            return (faces);
        }

        public static List<TriangleFace> SetNormalVector(List<TriangleFace> faces)
        {
            List<TriangleFace> vectors = new List<TriangleFace>();

            foreach (string line in lines)
            {
                if (line[0].ToString() == "v" & line[1].ToString() == "n")
                {
                    string space = " ";
                    string[] parts = line.ToString().Split(space.ToCharArray());

                    TriangleFace triangle = new TriangleFace();

                    triangle.normals[0] = float.Parse(parts[1], CultureInfo.InvariantCulture.NumberFormat);
                    triangle.normals[1] = float.Parse(parts[2], CultureInfo.InvariantCulture.NumberFormat);
                    triangle.normals[2] = float.Parse(parts[3], CultureInfo.InvariantCulture.NumberFormat);

                    vectors.Add(triangle);
                }
            }

            Console.WriteLine("Number of normal vectors: " + vectors.Count);

            foreach (TriangleFace face in faces)
            {
                face.normals[0] = vectors[face.normal].normals[0];
                face.normals[1] = vectors[face.normal].normals[1];
                face.normals[2] = vectors[face.normal].normals[2];
            }
            return faces;
        }

        //public static void DrawQuadFaces(List<QuadFace> _faces)
        //{
        //    GL.Begin(PrimitiveType.Quads);
        //    GL.Color3(Color.BurlyWood);
        //    foreach (QuadFace face in _faces)
        //    {
        //        foreach(Vertex vertex in face.point)
        //        {
        //            GL.Normal3(face.normals[0], face.normals[1], face.normals[2]);
        //            GL.Vertex3(vertex.x,vertex.y,vertex.z);
        //        }
        //    }
        //    GL.End();
        //}

        public static void DrawTriangleFaces(List<TriangleFace> _faces)
        {
            GL.Begin(PrimitiveType.Triangles);
            //GL.Color3(Color.BurlyWood);
            GL.Color3(Color.Cyan);
        
            foreach (TriangleFace face in _faces)
            {
                foreach (Vertex vertex in face.point)
                {
                    GL.Normal3(face.normals[0], face.normals[1], face.normals[2]);
                    GL.Vertex3(vertex.x, vertex.y, vertex.z);
                }
            }
            GL.End();
        }
    }
}
