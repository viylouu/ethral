partial class gateify {
    static float tossX(float x) => x*24/zoom-cam.X;
    static float tossY(float x) => x*24/zoom-cam.Y;
    static Vector2 toss(Vector2 x) => x*24/zoom-cam;

    static void inptogglePS() {
        if (inptoggleO.PlaybackState is PlaybackState.Playing) { inptoggleO.Stop(); }
        inptoggle.CurrentTime = new TimeSpan(0L);
        inptoggleO.Init(inptoggle);
        inptoggleO.Play();
    }

    static void nodecrtPS() {
        if (nodecrtO.PlaybackState is PlaybackState.Playing) { nodecrtO.Stop(); }
        nodecrt.CurrentTime = new TimeSpan(0L);
        nodecrtO.Init(nodecrt);
        nodecrtO.Play();
    }

    static void nodedelPS() {
        if (nodedelO.PlaybackState is PlaybackState.Playing) { nodedelO.Stop(); }
        nodedel.CurrentTime = new TimeSpan(0L);
        nodedelO.Init(nodedel);
        nodedelO.Play();
    }

    static void nodegrabPS() {
        if (nodegrabO.PlaybackState is PlaybackState.Playing) { nodegrabO.Stop(); }
        nodegrab.CurrentTime = new TimeSpan(0L);
        nodegrabO.Init(nodegrab);
        nodegrabO.Play();
    }

    static void nodeplacePS() {
        if (nodeplaceO.PlaybackState is PlaybackState.Playing) { nodeplaceO.Stop(); }
        nodeplace.CurrentTime = new TimeSpan(0L);
        nodeplaceO.Init(nodeplace);
        nodeplaceO.Play();
    }

    static void wireendPS() {
        if (wireendO.PlaybackState is PlaybackState.Playing) { wireendO.Stop(); }
        wireend.CurrentTime = new TimeSpan(0L);
        wireendO.Init(wireend);
        wireendO.Play();
    }

    static void wirestartPS() {
        if (wirestartO.PlaybackState is PlaybackState.Playing) { wirestartO.Stop(); }
        wirestart.CurrentTime = new TimeSpan(0L);
        wirestartO.Init(wirestart);
        wirestartO.Play();
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