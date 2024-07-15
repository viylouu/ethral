partial class gateify {
    public static void update() {
        if (Mouse.IsButtonDown(MouseButton.Middle))
            cam -= Mouse.DeltaPosition;

        zoom -= Mouse.ScrollWheelDelta / 64;
        zoom = m.clmp(zoom, .015625f, 2);

        ImGui.Begin("spawner");

        ImGui.ListBox("gates", ref curselgate, gateenum, gateenum.Length);
        ImGui.InputInt("pos x", ref placeX);
        ImGui.InputInt("pos y", ref placeY);

        if (ImGui.Button("add"))
            gates.Add(new node { gate = (byte)curselgate, on = false, pos = new Vector2(placeX, placeY) });

        ImGui.End();
    }

    static void updnode(ref int i) {
        float ssX = tossX(gates[i].pos.X),
              ssY = tossY(gates[i].pos.Y),
              ssS = 16 / zoom;

        if (Mouse.Position.X > ssX-ssS/2 && Mouse.Position.X < ssX+ssS/2 && Mouse.Position.Y > ssY-ssS/2 && Mouse.Position.Y < ssY+ssS/2) {
            if (Mouse.IsButtonPressed(MouseButton.Left))
                gates[i].dragged = true;
             
            if (Mouse.IsButtonPressed(MouseButton.Middle))
            { gates.RemoveAt(i); return; }

            if (Mouse.IsButtonPressed(MouseButton.Right))
                gates[i].on = !gates[i].on;
        }

        if (Mouse.IsButtonPressed(MouseButton.Left))
            if (Mouse.Position.X > ssX-ssS/1.25f && Mouse.Position.X < ssX+ssS/1.25f && Mouse.Position.Y > ssY-ssS/1.25f && Mouse.Position.Y < ssY+ssS/1.25f) {
                if ((gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10)
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), ssY)) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 0; wire2b = false; }

                if (gates[i].gate < 8 && gates[i].gate > 1) {
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 0; wire2b = true; }
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 1; wire2b = true; }
                }

                if (gates[i].gate != 8 && gates[i].gate != 11)
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), ssY)) < 4/zoom)
                    { wiring = true; wireI = i; wireio = 2; wire2f = false; }

                if (gates[i].gate == 8) {
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 2; wire2f = true; }
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 3; wire2f = true; }
                }
            }

        if (!Mouse.IsButtonDown(MouseButton.Left)) {
            gates[i].dragged = false;

            if(wiring)
                if (Mouse.Position.X > ssX-ssS/1.25f && Mouse.Position.X < ssX+ssS/1.25f && Mouse.Position.Y > ssY-ssS/1.25f && Mouse.Position.Y < ssY+ssS/1.25f) {
                    if (wireio >= 2 && (gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10)
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), ssY)) < 2/zoom) { 
                            gates[i].in1 = gates[wireI];
                            if (wireio == 2) { gates[wireI].out1 = gates[i]; }
                            else { gates[wireI].out2 = gates[i]; }
                        }

                    if (wireio >= 2 && gates[i].gate < 8 && gates[i].gate > 1) {
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom) { 
                            gates[i].in1 = gates[wireI];
                            if (wireio == 2) { gates[wireI].out1 = gates[i]; }
                            else { gates[wireI].out2 = gates[i]; }
                        }
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom) { 
                            gates[i].in2 = gates[wireI];
                            if (wireio == 2) { gates[wireI].out1 = gates[i]; }
                            else { gates[wireI].out2 = gates[i]; }
                        }
                    }

                    if (wireio <= 1 && gates[i].gate != 8 && gates[i].gate != 11)
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), ssY)) < 2/zoom) { 
                            gates[i].out1 = gates[wireI];
                            if (wireio == 0) { gates[wireI].in1 = gates[i]; }
                            else { gates[wireI].in2 = gates[i]; }
                        }

                    if (wireio <= 1 && gates[i].gate == 8) {
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom) { 
                            gates[i].out1 = gates[wireI];
                            if (wireio == 0) { gates[wireI].in1 = gates[i]; }
                            else { gates[wireI].in2 = gates[i]; }
                        }
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom) { 
                            gates[i].out2 = gates[wireI];
                            if (wireio == 0) { gates[wireI].in1 = gates[i]; }
                            else { gates[wireI].in2 = gates[i]; }
                        }
                    }
                }
        }
        
        if (gates[i].dragged) {
            Vector2 pos = new Vector2(m.rnd((Mouse.Position.X + cam.X) * zoom / 24), m.rnd((Mouse.Position.Y + cam.Y) * zoom / 24));
            if (pos != gates[i].pos)
                gates[i].pos = pos;
        }

        updnodestate(i);
    }

    static void updnodestate(int i) { 
        switch (gates[i].gate) {
            case 0 or 8 or 9 or 11:
                if (gates[i].in1 != null)
                    gates[i].on = gates[i].in1.on;
                break;
            case 1:
                if (gates[i].in1 != null)
                    gates[i].on = !gates[i].in1.on;
                break;
            case 2:
                if (gates[i].in1 != null && gates[i].in2 != null)
                    gates[i].on = gates[i].in1.on & gates[i].in2.on;
                break;
            case 3:
                if (gates[i].in1 != null && gates[i].in2 != null)
                    gates[i].on = gates[i].in1.on !& gates[i].in2.on;
                break;
            case 4:
                if (gates[i].in1 != null && gates[i].in2 != null)
                    gates[i].on = gates[i].in1.on | gates[i].in2.on;
                break;
            case 5:
                if (gates[i].in1 != null && gates[i].in2 != null)
                    gates[i].on = gates[i].in1.on !| gates[i].in2.on;
                break;
            case 6:
                if (gates[i].in1 != null && gates[i].in2 != null)
                    gates[i].on = gates[i].in1.on ^ gates[i].in2.on;
                break;
            case 7:
                if (gates[i].in1 != null && gates[i].in2 != null)
                    gates[i].on = gates[i].in1.on !^ gates[i].in2.on;
                break;
        }
    }
}