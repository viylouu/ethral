partial class floopy_burd {
    static ITexture bird;
    static ITexture pipe;

    static List<(Vector2, bool)> pipes = new List<(Vector2, bool)>();

    static float spawndelay = 2.45f;
    static float spawncounter = 0;

    static float birdy = 120, birdvel = 0;

    static bool go;

    static WaveStream pointSound = new WaveFileReader(@"assets\floopy burd\point.wav");
    static WaveStream loseSound = new WaveFileReader(@"assets\floopy burd\lose.wav");
    static WaveStream jumpSound = new WaveFileReader(@"assets\floopy burd\jump.wav");

    static WaveOutEvent pointOut = new WaveOutEvent();
    static WaveOutEvent loseOut = new WaveOutEvent();
    static WaveOutEvent jumpOut = new WaveOutEvent();

    static (WaveStream, WaveOutEvent, string)[] songs;
    static int songplayed;

    static int score;

    static bool lost;

    static bool menuOpen;
}