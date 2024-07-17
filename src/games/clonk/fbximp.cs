partial class clonk {
    static (Vector3[] verts, int[] inds) impfbx(string file) {
        try {
            AssimpContext context = new AssimpContext();

            Scene scene = context.ImportFile(file);

            List<Vector3> verts_l = new List<Vector3>();
            List<int> inds_l = new List<int>();

            foreach (Mesh mesh in scene.Meshes) {
                foreach(var vert in mesh.Vertices)
                    verts_l.Add(new Vector3(vert.X, vert.Y, vert.Z));

                foreach (Face face in mesh.Faces)
                    inds_l.AddRange(face.Indices);
            }

            return (verts_l.ToArray(), inds_l.ToArray());
        } catch (Exception e) {
            Console.WriteLine($"failed to load mesh at \"{file}\": {e}");
        }

        return (Array.Empty<Vector3>(), Array.Empty<int>());
    }
}