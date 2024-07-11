partial class thehub {
    static void Main(string[] args) {
        Simulation sim = Simulation.Create(init, rend);
        sim.Run(new DesktopPlatform());
    }
}