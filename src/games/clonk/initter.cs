partial class clonk {
    public static void init() {
        Window.Title = "ethral: clonk";

        (map_verts, map_inds, map_cols) = impfbx(Directory.GetCurrentDirectory() + @"\assets\clonk\de_dust.fbx");

        focused = false;

        for (int i = 0; i < map_verts.Length; i++)
            map_verts[i] = Vector3.Transform(map_verts[i], Matrix4x4.CreateRotationZ(-m.pi));
    }
}