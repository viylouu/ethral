partial class floopy_burd { 
    static void pointPS() {
        if (pointOut.PlaybackState is PlaybackState.Playing) { pointOut.Stop(); }
        pointSound.CurrentTime = new TimeSpan(0L);
        pointOut.Init(pointSound);
        pointOut.Play();
    }

    static void jumpPS() {
        if (jumpOut.PlaybackState is PlaybackState.Playing) { jumpOut.Stop(); }
        jumpSound.CurrentTime = new TimeSpan(0L);
        jumpOut.Init(jumpSound);
        jumpOut.Play();
    }

    static void losePS() {
        if (loseOut.PlaybackState is PlaybackState.Playing) { loseOut.Stop(); }
        loseSound.CurrentTime = new TimeSpan(0L);
        loseOut.Init(loseSound);
        loseOut.Play();
    }

    static void PS(int i) {
        if (songs[i].Item2.PlaybackState is PlaybackState.Playing) { songs[i].Item2.Stop(); }
        songs[i].Item1.CurrentTime = new TimeSpan(0L);
        songs[i].Item2.Init(songs[i].Item1);
        songs[i].Item2.Play();
    }

    static void stopsongs() {
        for (int i = 0; i < songs.Length; i++)
            if (songs[i].Item2.PlaybackState != PlaybackState.Stopped)
            { songs[i].Item2.Stop(); }
    }
}