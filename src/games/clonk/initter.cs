partial class clonk {
    public static void init() {
        Window.Title = "ethral: clonk";

        (map_verts, map_inds, map_cols) = impfbx(Directory.GetCurrentDirectory() + @"\assets\clonk\de_dust.fbx");

        focused = false;

        mapmodelmat = Matrix4x4.CreateTranslation(0, 0, 150);

        for (int i = 0; i < map_verts.Length; i++)
            map_verts[i] = Vector3.Transform(map_verts[i], mapmodelmat * Matrix4x4.CreateRotationZ(-m.pi) * Matrix4x4.CreateRotationY(-m.pi));
    }
}