partial class gateify {
    public static void init() {
        Window.Title = "ethral: gateify";

        Simulation.SetFixedResolution(1280, 720, Color.Black, false, true, false);

        gatespr = Graphics.LoadTexture(@"assets\gateify\gates.png");
        dot = Graphics.LoadTexture(@"assets\gateify\dot.png");
        dotemp = Graphics.LoadTexture(@"assets\gateify\dot_empty.png");

        gates = new List<node>();
    }
}