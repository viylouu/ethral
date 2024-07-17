partial class clonk {
    public static void rend(ICanvas c) {
        c.Clear(Color.Black);

        viewmat = Matrix4x4.CreateTranslation(cam) * Matrix4x4.CreateRotationY(m.d2r(pitch)) * Matrix4x4.CreateRotationX(m.d2r(yaw));
        projmat = Matrix4x4.CreatePerspectiveFieldOfView(m.d2r(90), Window.Width/Window.Height, .1f, 100);

        cam = new Vector3(0, 0, -3);

        r_verts.Clear();
        r_inds.Clear();
        r_cols.Clear();

        r_verts.AddRange(map_verts);
        r_inds.AddRange(map_inds);
        r_cols.AddRange(map_cols);

        r_verts2D.Clear();
        r_verts3D.Clear();

        for (int i = 0; i < r_verts.Count; i++) {
            Vector3 vert = Vector3.Transform(r_verts[i], viewmat * projmat);
            r_verts3D.Add(new Vector3(vert.X / vert.Z, vert.Y / vert.Z, vert.Z));
            r_verts2D.Add(new Vector2(vert.X / vert.Z, vert.Y / vert.Z));
        }

        var partitioner = Partitioner.Create(0, r_inds.Count, 3);

        ConcurrentBag<(float mvertz, int[] inds)> localResults = new ConcurrentBag<(float, int[])>();

        Parallel.ForEach(partitioner, parallelopts, range => {
            List<(float mvertz, int[] inds)> threadLocal = new List<(float, int[])>();

            for (int i = range.Item1; i < range.Item2; i += 3) {
                float mvertz = (r_verts[r_inds[i]].Z + r_verts[r_inds[i + 1]].Z + r_verts[r_inds[i + 2]].Z) / 3;
                threadLocal.Add((mvertz, new int[] { r_inds[i], r_inds[i + 1], r_inds[i + 2] }));
            }

            foreach (var result in threadLocal)
                localResults.Add(result);
        });

        var sortedResults = localResults.ToList();
        sortedResults.Sort((a, b) => b.mvertz.CompareTo(a.mvertz));

        r_oinds = new List<int>();
        foreach (var result in sortedResults)
            r_oinds.AddRange(result.inds);

        int[] r_indsARR = r_inds.ToArray();

        c.Translate(Window.Width/2, Window.Height/2);
        c.Scale(Window.Width/2, -Window.Height/2);

        for (int i = 0; i < r_oinds.Count; i += 3) {
            if (r_verts3D[r_oinds[i]].Z <= 0 || r_verts3D[r_oinds[i + 1]].Z <= 0 || r_verts3D[r_oinds[i + 2]].Z <= 0)
                continue;

            int colorIndex = (int)m.clmp(Array.IndexOf(r_indsARR, r_oinds[i]) / 3, 0, r_cols.Count - 1);
            c.Fill(r_cols[colorIndex]);
            c.DrawPolygon(new Vector2[] { r_verts2D[r_oinds[i]], r_verts2D[r_oinds[i + 1]], r_verts2D[r_oinds[i + 2]] });
        }

        c.ResetState();

        c.Fill(Color.White);
        c.DrawText(m.rnd(1/Time.DeltaTime) + " fps", Vector2.Zero);
    }
}