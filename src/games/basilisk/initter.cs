partial class basilisk {
    public static void init() {
        Window.Title = "ethral: basilisk";

        Simulation.SetFixedResolution(640, 360, Color.Black, false, false, true);

        seed = m.rand(int.MinValue, int.MaxValue);
        texseed = m.rand(int.MinValue, int.MaxValue);

        fnl1 = new FNL();
        fnl1.SetSeed(seed);
        fnl1.SetNoiseType(FNL.NoiseType.Perlin);
        fnl1.SetFrequency(0.0075f);
        fnl2 = new FNL();
        fnl2.SetSeed(texseed);
        fnl2.SetNoiseType(FNL.NoiseType.Perlin);
        fnl2.SetFrequency(0.0045f);

        chunks.Add(new List<chunk> { new chunk { tex = Graphics.CreateTexture(chunkSize, chunkSize), created = false, data = new cdat[chunkSizeSqr] } });

        p.pos = new Vector2(0, 0);
        p.vel = new Vector2(0, 0);

        string dat = "0";

        if (File.Exists(@"assets\savedata\broken.dat"))
            using (StreamReader sr = new StreamReader(@"assets\savedata\broken.dat"))
                dat = sr.ReadToEnd();

        bthigh = Convert.ToInt32(dat);
    }
}