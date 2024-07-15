partial class floopy_burd {
    public static void update() {
        if(!menuOpen) {
            if(go)
                spawncounter -= Time.DeltaTime;

            if (spawncounter <= 0) {
                spawncounter = spawndelay;

                pipes.Add((new Vector2(320, m.rand(120-60, 120+60)), false));
            }

            raincrtcounter -= Time.DeltaTime;

            if (raincrtcounter <= 0) { 
                raincrtcounter = raincrtdelay;

                rain.Add(new Vector2(m.rand(0, 320), -4));
            }

            if (Keyboard.IsKeyPressed(Key.Space) || Mouse.IsButtonPressed(MouseButton.Left)) {
                go = true;
                birdvel = 164;

                jumpPS();

                if (lost) {
                    spawndelay = 1.65f;
                    spawncounter = 0;
                    birdy = 120;
                    pipes = new List<(Vector2, bool)>();
                    lost = false;
                    score = 0;
                }
            }

            if(go) {
                birdy -= birdvel * Time.DeltaTime;
                birdvel -= Time.DeltaTime * 512;
            }

            if (!lost)
                for (int i = 0; i < pipes.Count; i++)
                    if (pipes[i].Item1.X > 72-18 && pipes[i].Item1.X < 80+8)
                        if(birdy > pipes[i].Item1.Y + 30 || birdy < pipes[i].Item1.Y - 30)
                        { losePS(); lost = true; birdvel = 256; }

            if (!lost)
                if (birdy < 8 || birdy > 232)
                { losePS(); lost = true; birdvel = 256; }

            if (lost && birdy < 8 || birdy > 232)
            { birdvel *= -.65f; if (birdy < 8) { birdy = 8; } else { birdy = 232; } }
        }

        if (songs[songplayed].Item2.PlaybackState == PlaybackState.Stopped) {
            int prev = songplayed;

            do
                songplayed = (int)m.clmp(m.rand(0, songs.Length), 0, songs.Length - 1);
            while (songs.Length==1?false:songplayed==prev);

            PS(songplayed);
        }

        if (Keyboard.IsKeyPressed(Key.Esc))
            menuOpen = !menuOpen;

        if (menuOpen) {
            ImGui.Begin("menu");

            if (ImGui.Button("close"))
            { thehub.rendact = null; Window.Resize(0, 0); stopsongs(); Window.Title = "ethral: hub"; }

            ImGui.End();
        }
    }
}