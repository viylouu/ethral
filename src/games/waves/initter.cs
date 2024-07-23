partial class waves {
    public static void init() {
        Window.Title = "ethral: waves";

        Simulation.SetFixedResolution(960, 540, Color.Black, false, false, false);

        for (int i = 0; i < ys.Length; i++)
            ys[i] = 540/2f;
    }
}