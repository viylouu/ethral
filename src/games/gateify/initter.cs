partial class gateify {
    public static void init() {
        Window.Title = "ethral: gateify";

        Simulation.SetFixedResolution(1280, 720, Color.Black, false, true, false);

        gatespr = Graphics.LoadTexture(@"assets\gateify\gates.png");
        dot = Graphics.LoadTexture(@"assets\gateify\dot.png");
        dotemp = Graphics.LoadTexture(@"assets\gateify\dot_empty.png");

        gates = new List<node>();

        menuOpen = false;

        if (songs != null)
            stopsongs();

        songs = new (WaveStream, WaveOutEvent, string)[Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\ambient\music\", "*.wav").Length];

        for (int i = 0; i < songs.Length; i++) {
            WaveStream stream = new WaveFileReader(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\ambient\music\", "*.wav")[i]);
            string name = Path.GetFileName(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\ambient\music\", "*.wav")[i]);

            songs[i] = (stream, new WaveOutEvent(), name);

            songs[i].Item2.Init(songs[i].Item1);
        }
    }
}