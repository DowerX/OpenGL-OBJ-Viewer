namespace OpenTKTest
{
  //  class QuadFace
  //  {
		//public Vertex[] point = new Vertex[4];
  //      public int normal = 0;
		//public float[] normals = new float[3];
  //  }

    class TriangleFace
    {
		public Vertex[] point = new Vertex[3];
        public int normal = 0;
		public float[] normals = new float[3];

        public int texCord = 0;
        public float[] texCords = new float[2];
    }
}
