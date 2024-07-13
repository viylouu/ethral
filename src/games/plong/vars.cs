partial class plong {
    static Vector2 paddleLpos = new Vector2(8, 60), paddleRpos = new Vector2(152, 60), ballpos = new Vector2(80, 60), ballvel = new Vector2(-1, 0);
    static int lscore, rscore;

    static bool ff = true;

    static WaveStream hitSound = new WaveFileReader(@"assets\plong\hit.wav"), scoreSound = new WaveFileReader(@"assets\plong\score.wav");
    static WaveOutEvent hitOut = new WaveOutEvent(), scoreOut = new WaveOutEvent();

    static LoopStream songStream = new LoopStream(new WaveFileReader(@"assets\plong\song.wav"));
    static WaveOutEvent songOut = new WaveOutEvent();

    static bool menuOpen;
}