partial class thehub {
    class audio {
        public string name;

        public WaveStream wstream;
        public WaveOutEvent wout;
    }

    static audio[] sounds = Array.Empty<audio>();

    static void playsound(string name) {
        for (int i = 0; i < sounds.Length; i++)
            if (sounds[i].name == name) {
                if (sounds[i].wout.PlaybackState != PlaybackState.Stopped) { sounds[i].wout.Stop(); }
                sounds[i].wstream.Position = 0L;
                sounds[i].wout.Init(sounds[i].wstream);
                sounds[i].wout.Play();
            }
    }

    static void addsound(string file_act) {
        string file = Directory.GetCurrentDirectory() + @"\" + file_act;

        Array.Resize(ref sounds, sounds.Length+1);

        sounds[sounds.Length - 1] = new audio();

        sounds[sounds.Length - 1].name = Path.GetFileNameWithoutExtension(file);
        sounds[sounds.Length - 1].wstream = new WaveFileReader(file);
        sounds[sounds.Length - 1].wout = new WaveOutEvent();
    }
}