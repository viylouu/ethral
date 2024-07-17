partial class gateify {
    public static void init() {
        Window.Title = "ethral: gateify";

        Simulation.SetFixedResolution(1280, 720, Color.Black, false, true, false);

        gatespr = Graphics.LoadTexture(@"assets\gateify\gates.png");
        specspr = Graphics.LoadTexture(@"assets\gateify\specials.png");
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

        if (!Path.Exists(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\"))
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\");

        if(Path.Exists(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\")) {
            savefiles = new string[Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\", "*.json").Length];

            for (int i = 0; i < savefiles.Length; i++)
                savefiles[i] = Path.GetFileNameWithoutExtension(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\", "*.json")[i]);
        }

        selects = new List<int>();

        pulse = false;
    }
}