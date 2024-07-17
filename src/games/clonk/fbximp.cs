using Assimp;

partial class clonk {
    static (Vector3[] verts, int[] inds, Color[] cols) impfbx(string file) {
        try {
            AssimpContext context = new AssimpContext();

            Scene scene = context.ImportFile(file);

            List<Vector3> verts_l = new List<Vector3>();
            List<int> inds_l = new List<int>();
            List<Color> cols_l = new List<Color>();

            foreach (Mesh mesh in scene.Meshes) {
                foreach(var vert in mesh.Vertices)
                    verts_l.Add(new Vector3(vert.X, vert.Y, vert.Z));

                foreach (Face face in mesh.Faces) {
                    inds_l.AddRange(face.Indices);

                    int matidx = mesh.MaterialIndex;
                    var mat = scene.Materials[matidx];

                    if (mat.HasColorDiffuse)
                        cols_l.Add(new ColorF(mat.ColorDiffuse.R, mat.ColorDiffuse.G, mat.ColorDiffuse.B, mat.ColorDiffuse.A).ToColor());
                    else
                        cols_l.Add(Color.Pink);
                }
            }

            return (verts_l.ToArray(), inds_l.ToArray(), cols_l.ToArray());
        } catch (Exception e) {
            Console.WriteLine($"failed to load mesh at \"{file}\": {e}");
        }

        return (Array.Empty<Vector3>(), Array.Empty<int>(), Array.Empty<Color>());
    }
}