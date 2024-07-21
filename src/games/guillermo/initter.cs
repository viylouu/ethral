partial class guillermo {
    public static void init() {
        Window.Title = "ethral: guillermo";

        player.loaddata(@"assets\guillermo\guillermo");
        player.setanim("idle");
        spike = Graphics.LoadTexture(@"assets\guillermo\spike.png");

        string[] audiofiles = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\guillermo\audio\", "*.wav");
        sounds = new audio[audiofiles.Length];

        for (int i = 0; i < audiofiles.Length; i++) {
            sounds[i] = new audio();
            sounds[i].name = Path.GetFileNameWithoutExtension(audiofiles[i]);
            sounds[i].wstream = new WaveFileReader(audiofiles[i]);
            sounds[i].wout = new WaveOutEvent();
        }

        addsound(@"assets\guillermo\music\jarabe tapatio.wav");

        //playsound("jarabe tapatio");

        Simulation.SetFixedResolution(320, 180, Color.Black, false, false, false);
    }
}