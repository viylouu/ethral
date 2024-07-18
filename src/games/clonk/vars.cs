partial class clonk {
    static Vector3[] map_verts;
    static int[] map_inds;
    static Color[] map_cols;

    static List<Vector3> r_verts = new List<Vector3>();
    static List<int> r_inds = new List<int>();
    static List<Vector2> r_verts2D = new List<Vector2>();
    static List<Color> r_cols = new List<Color>();
    static List<int> r_oinds = new List<int>();
    static List<Vector3> r_verts3D = new List<Vector3>();

    static Matrix4x4 projmat,
                     viewmat,
                     mapmodelmat;

    static Vector3 cam;
    static float pitch, yaw;

    static ParallelOptions parallelopts = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

    static bool focused;

    static float FOV = 90;
}