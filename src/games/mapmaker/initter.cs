partial class mapmaker {
    public static void init() {
        Window.Title = "ethral: mapmaker";

        guillermotiles = Graphics.LoadTexture(@"assets\guillermo\tiles.png");

        Simulation.SetFixedResolution(1280, 720, Color.Black, false, false, false);
    }
}