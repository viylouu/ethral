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

        for (int i = 0; i < gates.Count; i++) {
            float ssX = tossX(gates[i].pos.X),
                  ssY = tossY(gates[i].pos.Y),
                  ssS = 16/zoom;

            c.DrawTexture(
                gatespr, 
                new Rectangle(
                    (m.clmp(gates[i].gate,0,8)*2+(!gates[i].on?1:0))%5*16, 
                    m.flr((m.clmp(gates[i].gate,0,8)*2+(!gates[i].on?1:0))/5f)*16, 
                    16, 16
                ), 
                new Rectangle(ssX, ssY, ssS, ssS, Alignment.Center)
            );

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

            if ((gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10 && gates[i].in1!=null) {
                if (gates[i].in1.gate == 8) {
                    if (gates[i].in1.out1 == gates[i])
                        c.DrawLine(tossX(gates[i].pos.X - .45f), ssY, tossX(gates[i].in1.pos.X + .45f), tossY(gates[i].in1.pos.Y - .25f));
                    else
                        c.DrawLine(tossX(gates[i].pos.X - .45f), ssY, tossX(gates[i].in1.pos.X + .45f), tossY(gates[i].in1.pos.Y + .25f));
                } else
                    c.DrawLine(tossX(gates[i].pos.X - .45f), ssY, tossX(gates[i].in1.pos.X + .45f), tossY(gates[i].in1.pos.Y));
            }

            if (gates[i].gate < 8 && gates[i].gate > 1) {
                if (gates[i].in1 != null) { 
                    if (gates[i].in1.gate == 8) {
                        if (gates[i].in1.out1 == gates[i])
                            c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f), tossX(gates[i].in1.pos.X + .45f), tossY(gates[i].in1.pos.Y - .25f));
                        else
                            c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f), tossX(gates[i].in1.pos.X + .45f), tossY(gates[i].in1.pos.Y + .25f));
                    } else
                        c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f), tossX(gates[i].in1.pos.X + .45f), tossY(gates[i].in1.pos.Y));
                }
                if (gates[i].in2 != null) { 
                    if (gates[i].in2.gate == 8) {
                        if (gates[i].in2.out1 == gates[i])
                            c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f), tossX(gates[i].in2.pos.X + .45f), tossY(gates[i].in2.pos.Y - .25f));
                        else
                            c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f), tossX(gates[i].in2.pos.X + .45f), tossY(gates[i].in2.pos.Y + .25f));
                    } else
                        c.DrawLine(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f), tossX(gates[i].in2.pos.X + .45f), tossY(gates[i].in2.pos.Y));
                }
            }

            updnode(ref i);
        }

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
    }
}