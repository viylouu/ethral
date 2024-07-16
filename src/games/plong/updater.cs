partial class plong {
    public static void update() {
        if(!menuOpen) {
            if (Keyboard.IsKeyDown(Key.W)) {
                paddleLpos.Y -= 96 * Time.DeltaTime;
                if(ff)
                    ff = false;
            }
            if (Keyboard.IsKeyDown(Key.S)) {
                paddleLpos.Y += 96 * Time.DeltaTime;
                if(ff)
                    ff = false;
            }

            if (Keyboard.IsKeyDown(Key.UpArrow)) {
                paddleRpos.Y -= 96 * Time.DeltaTime;
                if(ff)
                    ff = false;
            }
            if (Keyboard.IsKeyDown(Key.DownArrow)) {
                paddleRpos.Y += 96 * Time.DeltaTime;
                if(ff)
                    ff = false;
            }

            paddleLpos.Y = m.clmp(paddleLpos.Y, 8, 112);
            paddleRpos.Y = m.clmp(paddleRpos.Y, 8, 112);

            if (!ff) {
                ballpos += ballvel * 64 * Time.DeltaTime;

                if (ballvel.X < 0) {
                    if (ballpos.Y > paddleLpos.Y - 9 && ballpos.Y < paddleLpos.Y + 9 && ballpos.X < paddleLpos.X + 2 && ballpos.X > paddleLpos.X - 2)
                    { ballvel.X *= -1; ballvel.Y = (ballpos.Y-paddleLpos.Y)/8; hitOut.Stop(); hitPS(); }
                } else {
                    if (ballpos.Y > paddleRpos.Y - 9 && ballpos.Y < paddleRpos.Y + 9 && ballpos.X > paddleRpos.X - 2 && ballpos.X > paddleRpos.X - 2)
                    { ballvel.X *= -1; ballvel.Y = (ballpos.Y-paddleRpos.Y)/8; hitOut.Stop(); hitPS(); }
                }

                if(ballpos.Y > 118)
                { ballpos.Y = 118; ballvel.Y *= -1; hitOut.Stop(); hitPS(); }
                if (ballpos.Y < 2)
                { ballpos.Y = 2; ballvel.Y *= -1; hitOut.Stop(); hitPS(); }

                if (ballpos.X < 1) {
                    ballpos = new Vector2(80, 60);
                    paddleLpos = new Vector2(8, 60);
                    paddleRpos = new Vector2(152, 60);
                    ff = true;
                    rscore++;
                    scorePS();
                } else if (ballpos.X > 159) {
                    ballpos = new Vector2(80, 60);
                    paddleLpos = new Vector2(8, 60);
                    paddleRpos = new Vector2(152, 60);
                    ff = true;
                    lscore++;
                    scorePS();
                }
            }
        }
        
        if (Keyboard.IsKeyPressed(Key.Esc))
            menuOpen = !menuOpen;

        if (menuOpen) {
            ImGui.Begin("menu");

            if (ImGui.Button("close"))
            { thehub.rendact = null; songOut.Stop(); Window.Title = "ethral: hub"; }

            ImGui.End();
        }
    }
}