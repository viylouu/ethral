partial class waves {
    public static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.Antialias(false);

        c.Fill(Color.CornflowerBlue);

        for (int i = 0; i < ys.Length-1; i++)
            c.DrawLine(i*8, ys[i], (i+1)*8, ys[i+1]);
    }
}