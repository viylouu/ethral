partial class mapmaker {
    public static void init() {
        Window.Title = "ethral: mapmaker";

        guillermotiles = Graphics.LoadTexture(@"assets\guillermo\tiles.png");

        guillermotilesXscale = (byte)(guillermotiles.Width / 8);
        guillermotilesYscale = (byte)(guillermotiles.Height / 8);

        Simulation.SetFixedResolution(1280, 720, Color.Black, false, false, false);

        map[0, 0] = packxy(1, 5);
    }
}