partial class plong { 
    public static void init() {
        Window.Title = "ethral: plong";

        Simulation.SetFixedResolution(160, 120, Color.Black, false, false, false);

        ff = true;
        paddleLpos = new Vector2(8, 60); 
        paddleRpos = new Vector2(152, 60); 
        ballpos = new Vector2(80, 60); 
        ballvel = new Vector2(-1, 0);
        lscore = 0;
        rscore = 0;

        hitOut = new WaveOutEvent(); scoreOut = new WaveOutEvent();

        hitOut.Init(hitSound); scoreOut.Init(scoreSound);

        menuOpen = false;

        if (songOut.PlaybackState != PlaybackState.Stopped) { songOut.Stop(); }
        songOut.Init(songStream);
        songOut.Play();
    }
}