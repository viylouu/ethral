partial class gateify {
    public static void rend(ICanvas c) {
        c.Clear(new Color(24, 24, 24));
        c.Antialias(false);
        
        c.Fill(Color.Black);
        float iadd = 24 / zoom;
        for (float i = -iadd; i < 1280; i+= iadd)
            c.DrawLine(i-cam.X%iadd,-24-cam.Y%iadd,i-cam.X%iadd,744-cam.Y%iadd);
        for (float i = -iadd; i < 720; i+=iadd)
            c.DrawLine(-24-cam.X%iadd,i-cam.Y%iadd,1304-cam.X%iadd,i-cam.Y%iadd);

        c.Fill(Color.White);

        decreaseamt = 0;

        if (tickcounter <= 0) {
            pausedgates = new List<node>();

            for (int i = 0; i < gates.Count; i++) {
                node gate = new node();

                gate.out1 = gates[i].out1;
                gate.out2 = gates[i].out2;
                gate.in1 = gates[i].in1;
                gate.in2 = gates[i].in2;
                gate.gate = gates[i].gate;
                gate.on = gates[i].on;
                gate.special = gates[i].special;
                gate.pos = gates[i].pos;

                pausedgates.Add(gate);
            }
        }

        for (int i = 0; i < gates.Count; i++)
            drawgate(c, i);

        if (wiring)
            c.DrawLine(
                wireio<=1? 
                (
                    wire2b?
                    new Vector2(tossX(gates[wireI].pos.X-.45f),tossY(gates[wireI].pos.Y+(wireio==0?-.25f:.25f)))
                    :
                    new Vector2(tossX(gates[wireI].pos.X-.45f),tossY(gates[wireI].pos.Y))
                ) 
                : 
                (
                    wire2f?
                    new Vector2(tossX(gates[wireI].pos.X+.45f),tossY(gates[wireI].pos.Y+(wireio==2?-.25f:.25f)))
                    :
                    new Vector2(tossX(gates[wireI].pos.X+.45f),tossY(gates[wireI].pos.Y))
                ),

                Mouse.Position
            );

        c.DrawTexture(dot, tossX(placeX), tossY(placeY), 4/zoom, 4/zoom, Alignment.Center);

        if (!Mouse.IsButtonDown(MouseButton.Left))
            wiring = false;

        c.FontSize(12);
        c.DrawText(songs[songplayed].Item3, new Vector2(3, 717), Alignment.BottomLeft);

        if (tickcounter <= 0) {
            tickcounter = tickspeed;
            pulse = !pulse;
        }
    }

    static void drawgate(ICanvas c, int i) { 
        float ssX = tossX(gates[i].pos.X),
              ssY = tossY(gates[i].pos.Y),
              ssS = 16/zoom;

        c.DrawTexture(
            gates[i].special?specspr:gatespr,
            new Rectangle(
                (m.clmp(gates[i].gate, 0, 8) * 2 + (!gates[i].on ? 1 : 0)) % 5 * 16,
                m.flr((m.clmp(gates[i].gate, 0, 8) * 2 + (!gates[i].on ? 1 : 0)) / 5f) * 16,
                16, 16
            ),
            new Rectangle(ssX, ssY, ssS, ssS, Alignment.Center)
        );

        if (gates[i].in1 != -1)
            gates[i].in1 -= decreaseamt;
        if (gates[i].in2 != -1)
            gates[i].in2 -= decreaseamt;
        if (gates[i].out1 != -1)
            gates[i].out1 -= decreaseamt;
        if (gates[i].out2 != -1)
            gates[i].out2 -= decreaseamt;

        int sel = -1;

        if (Mouse.Position.X > ssX-ssS/1.25f && Mouse.Position.X < ssX+ssS/1.25f && Mouse.Position.Y > ssY-ssS/1.25f && Mouse.Position.Y < ssY+ssS/1.25f) {
            if ((gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10)
                if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), ssY)) < 2 / zoom)
                    sel = 0;

            if (gates[i].gate < 8 && gates[i].gate > 1) {
                if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f))) < 2 / zoom)
                    sel = 0;
                if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f))) < 2 / zoom)
                    sel = 1;
            }

            if (gates[i].gate != 8 && gates[i].gate != 11)
                if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), ssY)) < 2 / zoom)
                    sel = 2;

            if (gates[i].gate == 8) {
                if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y - .25f))) < 2 / zoom)
                    sel = 2;
                if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y + .25f))) < 2 / zoom)
                    sel = 3;
            }
        }

        if ((gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10)
            c.DrawTexture(sel==0?dotemp:dot, tossX(gates[i].pos.X - .45f), ssY, 4/zoom, 4/zoom, Alignment.Center);

        if (gates[i].gate < 8 && gates[i].gate > 1) {
            c.DrawTexture(sel==0?dotemp:dot, tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f), 4 / zoom, 4 / zoom, Alignment.Center);
            c.DrawTexture(sel==1?dotemp:dot, tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f), 4 / zoom, 4 / zoom, Alignment.Center);
        }

        if (gates[i].gate != 8 && gates[i].gate != 11)
            c.DrawTexture(sel==2?dotemp:dot, tossX(gates[i].pos.X + .45f), ssY, 4 / zoom, 4 / zoom, Alignment.Center);

        if (gates[i].gate == 8) {
            c.DrawTexture(sel==2?dotemp:dot, tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y - .25f), 4 / zoom, 4 / zoom, Alignment.Center);
            c.DrawTexture(sel==3?dotemp:dot, tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y + .25f), 4 / zoom, 4 / zoom, Alignment.Center);
        }

        if ((gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10 && gates[i].in1.get() != null) {
            if (gates[i].in1.get().gate == 8) {
                if (gates[i].in1.get().out1 == i)
                    c.DrawLine(tossX(gates[i].pos.X - .45f), ssY, tossX(gates[i].in1.get().pos.X + .45f), tossY(gates[i].in1.get().pos.Y - .25f));
                else
                    c.DrawLine(tossX(gates[i].pos.X - .45f), ssY, tossX(gates[i].in1.get().pos.X + .45f), tossY(gates[i].in1.get().pos.Y + .25f));
            } else
                c.DrawLine(tossX(gates[i].pos.X - .45f), ssY, tossX(gates[i].in1.get().pos.X + .45f), tossY(gates[i].in1.get().pos.Y));
        }

        if (gates[i].gate < 8 && gates[i].gate > 1) {
            if (gates[i].in1.get() != null) { 
                if (gates[i].in1.get().gate == 8) {
                    if (gates[i].in1.get().out1 == i)
                        c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f), tossX(gates[i].in1.get().pos.X + .45f), tossY(gates[i].in1.get().pos.Y - .25f));
                    else
                        c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f), tossX(gates[i].in1.get().pos.X + .45f), tossY(gates[i].in1.get().pos.Y + .25f));
                } else
                    c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f), tossX(gates[i].in1.get().pos.X + .45f), tossY(gates[i].in1.get().pos.Y));
            }
            if (gates[i].in2.get() != null) { 
                if (gates[i].in2.get().gate == 8) {
                    if (gates[i].in2.get().out1 == i)
                        c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f), tossX(gates[i].in2.get().pos.X + .45f), tossY(gates[i].in2.get().pos.Y - .25f));
                    else
                        c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f), tossX(gates[i].in2.get().pos.X + .45f), tossY(gates[i].in2.get().pos.Y + .25f));
                } else
                    c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f), tossX(gates[i].in2.get().pos.X + .45f), tossY(gates[i].in2.get().pos.Y));
            }
        }

        updnode(ref i);
    }
}