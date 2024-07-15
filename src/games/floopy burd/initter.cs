partial class floopy_burd {
    public static void init() {
        Window.Title = "ethral: floopy burd";

        bird = Graphics.LoadTexture(@"assets\floopy burd\burd.png");
        pipe = Graphics.LoadTexture(@"assets\floopy burd\pip.png");

        Simulation.SetFixedResolution(320, 240, Color.Black, false, false, false);

        spawndelay = 1.65f;
        spawncounter = 0.0015f;
        go = false;
        birdy = 120;
        birdvel = 0;
        pipes = new List<(Vector2, bool)>();

        if (pointOut.PlaybackState != PlaybackState.Stopped) { pointOut.Stop(); }
        if (jumpOut.PlaybackState != PlaybackState.Stopped) { jumpOut.Stop(); }
        if (loseOut.PlaybackState != PlaybackState.Stopped) { loseOut.Stop(); }

        pointOut.Init(pointSound);
        jumpOut.Init(jumpSound);
        loseOut.Init(loseSound);

        if (songs != null)
            stopsongs();

        songs = new (WaveStream, WaveOutEvent, string)[Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\ambient\music\", "*.wav").Length];

        for (int i = 0; i < songs.Length; i++) {
            WaveStream stream = new WaveFileReader(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\ambient\music\", "*.wav")[i]);
            string name = Path.GetFileName(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\ambient\music\", "*.wav")[i]);

            songs[i] = (stream, new WaveOutEvent(), name);

            songs[i].Item2.Init(songs[i].Item1);
        }

        score = 0;

        lost = false;
    }
}