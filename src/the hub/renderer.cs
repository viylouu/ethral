partial class thehub {
    static void rend(ICanvas c) {
        if(rendact != null)
        { updact(); if(rendact != null) { rendact(c); } return; }

        c.Fill(bgcol_light);
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
            c.Fill(bgcol_dark);
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
        but_x = Window.Width / 2;
        but_width = Window.Width / 3;
        but_height = Window.Height / 12;
        but_shad = Window.Height / 32;
        but_ts = Window.Height / 24;
        but_tshad = Window.Height / 128;
        but_hamt = m.max(Window.Width, Window.Height) / 96;

        for (int i = 0; i < games.Length; i++) {
            but_y = Window.Height/2+(i-games.Length/2)*Window.Height/10;
            float selprog = games[i].selprog;
            c.Fill(butcol_light);
            c.DrawRoundedRect(but_x, but_y, but_width+(selprog*but_hamt), but_height+(selprog*but_hamt), but_br, Alignment.Center);

            c.FontSize(but_ts+(selprog*but_hamt/6));
            c.Fill(textcol);
            c.DrawText(games[i].name, but_x, but_y, Alignment.Center);

            but = new Rectangle(but_x, but_y, but_width, but_height, Alignment.Center);
            games[i].sel = but.ContainsPoint(Mouse.Position);
            games[i].selprog = e.dist(selprog, games[i].sel?1:0, 12);

            if (games[i].sel && Mouse.IsButtonPressed(MouseButton.Left))
            { rendact = games[i].rend; updact = games[i].update; games[i].init(); }
        }
    }
}