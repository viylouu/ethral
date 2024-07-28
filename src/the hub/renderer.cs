partial class thehub {
    static void rend(ICanvas c) {
        if(rendact != null)
        { updact(); if(rendact != null) { rendact(c); } return; }

        Gradient bggrad = new LinearGradient(Window.Width/16*2, Window.Height, Window.Width/16*15, 0, new Color[] { bgcol_dark, bgcol_light });
        Gradient boxgrad = new LinearGradient(Window.Width/16*2, Window.Height, Window.Width/.75f, -Window.Height/16*3, new Color[] { bgcol_dark, bgcol_light });
        Gradient butgrad = new LinearGradient(Window.Width/16*2, Window.Height, Window.Width/16*4, 0, new Color[] { butcol_dark, butcol_light });

        c.Fill(bggrad);
        c.DrawRect(0, 0, Window.Width, Window.Height);

        boxaddcount -= Time.DeltaTime;

        if (boxaddcount <= 0) {
            boxaddcount = boxadddel;

            bgboxes.Add(
                new box { 
                    ang = m.rand(-m.pi, m.pi), 
                    angvel = m.rand(-m.pi/8, m.pi/8), 
                    pos = new Vector2(m.rand(-256, Window.Width+256), Window.Height + 512), 
                    vel = new Vector2(m.rand(-m.tau, m.tau), m.rand(6, 24)*25),
                    size = m.rand(32, 256)
                }
            );
        }

        for (int i = 0; i < bgboxes.Count; i++) {
            c.Fill(boxgrad);
            c.Translate(bgboxes[i].pos);
            c.Rotate(bgboxes[i].ang);
            c.DrawRect(0, 0, bgboxes[i].size, bgboxes[i].size, Alignment.Center);
            c.ResetState();

            bgboxes[i].pos -= bgboxes[i].vel * Time.DeltaTime;
            bgboxes[i].vel.Y += Time.DeltaTime * 12;
            bgboxes[i].ang += bgboxes[i].angvel * Time.DeltaTime;

            if (bgboxes[i].pos.Y <= -512)
            { bgboxes.RemoveAt(i); i--; }
        }

        but_br = m.max(Window.Width, Window.Height) / 32;
        but_x = Window.Width / 5;
        but_width = Window.Width / 3;
        but_height = Window.Height / 12;
        but_shad = Window.Height / 32;
        but_ts = Window.Height / 24;
        but_tshad = Window.Height / 128;
        but_hamt = m.max(Window.Width, Window.Height) / 96;

        for (int i = 0; i < games.Length; i++) {
            but_y = Window.Height/2+(i-games.Length/2)*Window.Height/10;
            float selprog = games[i].selprog;
            c.Fill(butgrad);
            c.DrawRoundedRect(but_x, but_y, but_width+(selprog*but_hamt), but_height+(selprog*but_hamt), but_br, Alignment.Center);

            c.FontSize(but_ts+(selprog*but_hamt/6));
            //c.Font(font);
            c.Fill(textcol);
            c.DrawText(games[i].name, but_x-but_width/2+but_ts+(selprog*but_hamt/6), but_y, Alignment.CenterLeft);

            but = new Rectangle(but_x, but_y, but_width, but_height, Alignment.Center);
            games[i].sel = but.ContainsPoint(Mouse.Position);
            games[i].selprog = e.dist(selprog, games[i].sel?1:0, 12);

            if (games[i].sel && Mouse.IsButtonPressed(MouseButton.Left))
            { rendact = games[i].rend; updact = games[i].update; games[i].init(); playsound("select"); }
        }

        //c.Font(font);
        c.FontSize(Window.Height/8);
        c.Fill(bgcol_dark);
        c.DrawText("ethral", new Vector2(Window.Width - Window.Width/24, Window.Height/2 + Window.Height/48), Alignment.CenterRight);
        c.Fill(textcol);
        c.DrawText("ethral", new Vector2(Window.Width - Window.Width/24, Window.Height/2), Alignment.CenterRight);

        konami();
    }

    static void unlockdevmode() {
        Array.Resize(ref games, gameslength + 3);
        games[gameslength] = sp_g;
        games[gameslength + 1] = mapmaker_g;
        games[gameslength + 2] = waves_g;
    }

    static void konami() { 
        if (konamicorr == 10 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.Enter)) { 
            konamicorr = 0; 
            playsound("11"); 
            devtools = !devtools; 
            playsound("correct");
            if (devtools)
                unlockdevmode();
            else
                Array.Resize(ref games, gameslength);
        }
        else if (konamicorr == 10 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.Enter))
        { konamicorr = 0; }

        if (konamicorr == 9 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.A))
        { konamicorr = 10; playsound("10"); }
        else if (konamicorr == 9 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.A))
        { konamicorr = 0; }

        if (konamicorr == 8 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.B))
        { konamicorr = 9; playsound("9"); }
        else if (konamicorr == 8 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.B))
        { konamicorr = 0; }

        if (konamicorr == 7 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.RightArrow))
        { konamicorr = 8; playsound("8"); }
        else if (konamicorr == 7 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.RightArrow))
        { konamicorr = 0; }

        if (konamicorr == 6 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.LeftArrow))
        { konamicorr = 7; playsound("7"); }
        else if (konamicorr == 6 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.LeftArrow))
        { konamicorr = 0; }

        if (konamicorr == 5 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.RightArrow))
        { konamicorr = 6; playsound("6"); }
        else if (konamicorr == 5 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.RightArrow))
        { konamicorr = 0; }

        if (konamicorr == 4 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.LeftArrow))
        { konamicorr = 5; playsound("5"); }
        else if (konamicorr == 4 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.LeftArrow))
        { konamicorr = 0; }

        if (konamicorr == 3 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.DownArrow))
        { konamicorr = 4; playsound("4"); }
        else if (konamicorr == 3 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.DownArrow))
        { konamicorr = 0; }

        if (konamicorr == 2 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.DownArrow))
        { konamicorr = 3; playsound("3"); }
        else if (konamicorr == 2 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.DownArrow))
        { konamicorr = 0; }

        if (konamicorr == 1 && Keyboard.PressedKeys.Count() == 1 && Keyboard.PressedKeys.Contains(Key.UpArrow))
        { konamicorr = 2; playsound("2"); }
        else if (konamicorr == 1 && Keyboard.PressedKeys.Count() == 1 && !Keyboard.PressedKeys.Contains(Key.UpArrow))
        { konamicorr = 0; }

        if (konamicorr == 0 && Keyboard.IsKeyPressed(Key.UpArrow))
        { konamicorr = 1; playsound("1"); }
    }
}