partial class plong { 
    static void scorePS() {
        if (scoreOut.PlaybackState is PlaybackState.Playing) { scoreOut.Stop(); }
        scoreSound.CurrentTime = new TimeSpan(0L);
        scoreOut.Init(scoreSound);
        scoreOut.Play();
    }

    static void hitPS() {
        if (hitOut.PlaybackState is PlaybackState.Playing) { hitOut.Stop(); }
        hitSound.CurrentTime = new TimeSpan(0L);
        hitOut.Init(hitSound);
        hitOut.Play();
    }
}