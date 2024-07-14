partial class floopy_burd {
    public static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.Translate(80, birdy);
        c.Rotate(birdvel/384);
        c.DrawTexture(bird, 0, 0, 16, 16, Alignment.Center);
        c.ResetState();

        for(int i = 0; i < pipes.Count; i++) {
            c.DrawTexture(pipe, pipes[i].Item1.X, pipes[i].Item1.Y+30, 18, 160, Alignment.TopLeft);
            c.DrawTexture(pipe, pipes[i].Item1.X, pipes[i].Item1.Y-30, 18, 160, Alignment.BottomLeft);

            if(!menuOpen) {
                pipes[i] = (new Vector2(pipes[i].Item1.X - Time.DeltaTime*128, pipes[i].Item1.Y), pipes[i].Item2);

                if (pipes[i].Item1.X < 89 && !pipes[i].Item2 && !lost)
                { pipes[i] = (pipes[i].Item1, true); pointPS(); score++; }

                if (pipes[i].Item1.X < -18)
                { pipes.RemoveAt(i); i--; }
            }
        }

        c.Fill(Color.White);
        c.Antialias(false);
        c.FontSize(12);
        c.DrawText(score + "", new Vector2(160, 8), Alignment.Center);
        c.DrawText(songs[songplayed].Item3, new Vector2(3, 237), Alignment.BottomLeft);
        if(lost)
            c.DrawText("game over", new Vector2(160, 120), Alignment.Center);
    }
}