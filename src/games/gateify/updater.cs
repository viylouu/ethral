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
        { gates.Add(new node { gate = (byte)curselgate, on = false, pos = new Vector2(placeX, placeY) }); nodecrtPS(); }

        ImGui.End();

        if (Keyboard.IsKeyPressed(Key.Esc))
            menuOpen = !menuOpen;

        if (menuOpen) {
            ImGui.Begin("menu");

            if (ImGui.Button("close"))
            { thehub.rendact = null; Window.Resize(0, 0); stopsongs(); Window.Title = "ethral: hub"; }

            ImGui.End();
        }

        salImgui();

        if (Keyboard.IsKeyPressed(Key.W) || Keyboard.IsKeyPressed(Key.A) || Keyboard.IsKeyPressed(Key.S) || Keyboard.IsKeyPressed(Key.D))
            for (int i = 0; i < selects.Count; i++) {
                if (Keyboard.IsKeyPressed(Key.W))
                    gates[selects[i]].pos.Y--;
                if (Keyboard.IsKeyPressed(Key.S))
                    gates[selects[i]].pos.Y++;
                if (Keyboard.IsKeyPressed(Key.A))
                    gates[selects[i]].pos.X--;
                if (Keyboard.IsKeyPressed(Key.D))
                    gates[selects[i]].pos.X++;
            }

        if (selects.Count != 0) {
            ImGui.Begin("selection");

            ImGui.Text(selects.Count + " node"+(selects.Count==1?"":"s")+" selected");

            if (ImGui.Button("deselect"))
                selects = new List<int>();

            if (ImGui.Button("duplicate")) {
                int gatecount = gates.Count;
                selects = new List<int>();

                for (int i = 0; i < selects.Count; i++) {
                    node duplicated = new node();
                    duplicated.gate = gates[selects[i]].gate;
                    duplicated.in1 = gates[selects[i]].in1;
                    duplicated.in2 = gates[selects[i]].in2;
                    duplicated.out1 = gates[selects[i]].out1;
                    duplicated.out2 = gates[selects[i]].out2;

                    if (!selects.Contains(duplicated.in1))
                        duplicated.in1 = -1;
                    if (!selects.Contains(duplicated.in2))
                        duplicated.in2 = -1;
                    if (!selects.Contains(duplicated.out1))
                        duplicated.out1 = -1;
                    if (!selects.Contains(duplicated.out2))
                        duplicated.out2 = -1;

                    gates.Add(duplicated);
                    selects.Add(gatecount+i);
                }
            }

            ImGui.End();
        }
    }

    static void updnode(ref int i) {
        float ssX = tossX(gates[i].pos.X),
              ssY = tossY(gates[i].pos.Y),
              ssS = 16 / zoom;

        if (gates[i].in1.get() == null)
            gates[i].in1 = -1;
        if (gates[i].in2.get() == null)
            gates[i].in2 = -1;
        if (gates[i].out1.get() == null)
            gates[i].out1 = -1;
        if (gates[i].out2.get() == null)
            gates[i].out2 = -1;

        if (Mouse.Position.X > ssX-ssS/2 && Mouse.Position.X < ssX+ssS/2 && Mouse.Position.Y > ssY-ssS/2 && Mouse.Position.Y < ssY+ssS/2) {
            if (Mouse.IsButtonPressed(MouseButton.Left)) { 
                gates[i].dragged = true; 
                nodegrabPS(); 

                if (!Keyboard.IsKeyDown(Key.LeftShift)) 
                    selects = new List<int>();
                if (!selects.Contains(i)) 
                    selects.Add(i); 
            }
             
            if (Mouse.IsButtonPressed(MouseButton.Right))
            { gates.RemoveAt(i); nodedelPS(); decreaseamt++; i--; return; }

            if (Mouse.IsButtonPressed(MouseButton.Middle))
            { gates[i].on = !gates[i].on; inptogglePS(); }
        }

        if (Mouse.IsButtonPressed(MouseButton.Left))
            if (Mouse.Position.X > ssX-ssS/1.25f && Mouse.Position.X < ssX+ssS/1.25f && Mouse.Position.Y > ssY-ssS/1.25f && Mouse.Position.Y < ssY+ssS/1.25f) {
                if ((gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10)
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), ssY)) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 0; wire2b = false; wirestartPS(); }

                if (gates[i].gate < 8 && gates[i].gate > 1) {
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 0; wire2b = true; wirestartPS(); }
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 1; wire2b = true; wirestartPS(); }
                }

                if (gates[i].gate != 8 && gates[i].gate != 11)
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), ssY)) < 4/zoom)
                    { wiring = true; wireI = i; wireio = 2; wire2f = false; wirestartPS(); }

                if (gates[i].gate == 8) {
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 2; wire2f = true; wirestartPS(); }
                    if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom)
                    { wiring = true; wireI = i; wireio = 3; wire2f = true; wirestartPS(); }
                }
            }

        if (!Mouse.IsButtonDown(MouseButton.Left)) {
            if (gates[i].dragged)
                nodeplacePS();

            gates[i].dragged = false;

            if(wiring)
                if (Mouse.Position.X > ssX-ssS/1.25f && Mouse.Position.X < ssX+ssS/1.25f && Mouse.Position.Y > ssY-ssS/1.25f && Mouse.Position.Y < ssY+ssS/1.25f) {
                    if (wireio >= 2 && (gates[i].gate >= 8 || gates[i].gate <= 1) && gates[i].gate != 10)
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), ssY)) < 2/zoom) { 
                            gates[i].in1 = wireI;
                            if (wireio == 2) { gates[wireI].out1 = i; }
                            else { gates[wireI].out2 = i; }
                            wireendPS();
                        }

                    if (wireio >= 2 && gates[i].gate < 8 && gates[i].gate > 1) {
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom) { 
                            gates[i].in1 = wireI;
                            if (wireio == 2) { gates[wireI].out1 = i; }
                            else { gates[wireI].out2 = i; }
                            wireendPS();
                        }
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X - .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom) { 
                            gates[i].in2 = wireI;
                            if (wireio == 2) { gates[wireI].out1 = i; }
                            else { gates[wireI].out2 = i; }
                            wireendPS();
                        }
                    }

                    if (wireio <= 1 && gates[i].gate != 8 && gates[i].gate != 11)
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), ssY)) < 2/zoom) { 
                            gates[i].out1 = wireI;
                            if (wireio == 0) { gates[wireI].in1 = i; }
                            else { gates[wireI].in2 = i; }
                            wireendPS();
                        }

                    if (wireio <= 1 && gates[i].gate == 8) {
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y - .25f))) < 2/zoom) { 
                            gates[i].out1 = wireI;
                            if (wireio == 0) { gates[wireI].in1 = i; }
                            else { gates[wireI].in2 = i; }
                            wireendPS();
                        }
                        if (m.dist(Mouse.Position, new Vector2(tossX(gates[i].pos.X + .45f), tossY(gates[i].pos.Y + .25f))) < 2/zoom) { 
                            gates[i].out2 = wireI;
                            if (wireio == 0) { gates[wireI].in1 = i; }
                            else { gates[wireI].in2 = i; }
                            wireendPS();
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

        if (songs[songplayed].Item2.PlaybackState == PlaybackState.Stopped) {
            int prev = songplayed;

            do
                songplayed = (int)m.clmp(m.rand(0, songs.Length), 0, songs.Length - 1);
            while (songs.Length==1?false:songplayed==prev);

            PS(songplayed);
        }
    }

    static void updnodestate(int i) { 
        switch (gates[i].gate) {
            case 0 or 8 or 9 or 11:
                if (gates[i].in1.get() != null)
                    gates[i].on = gates[i].in1.get().on;
                break;
            case 1:
                if (gates[i].in1.get() != null)
                    gates[i].on = !gates[i].in1.get().on;
                break;
            case 2:
                if (gates[i].in1.get() != null && gates[i].in2.get() != null)
                    gates[i].on = gates[i].in1.get().on & gates[i].in2.get().on;
                break;
            case 3:
                if (gates[i].in1.get() != null && gates[i].in2.get() != null)
                    gates[i].on = gates[i].in1.get().on !& gates[i].in2.get().on;
                break;
            case 4:
                if (gates[i].in1.get() != null && gates[i].in2.get() != null)
                    gates[i].on = gates[i].in1.get().on | gates[i].in2.get().on;
                break;
            case 5:
                if (gates[i].in1.get() != null && gates[i].in2.get() != null)
                    gates[i].on = gates[i].in1.get().on !| gates[i].in2.get().on;
                break;
            case 6:
                if (gates[i].in1.get() != null && gates[i].in2.get() != null)
                    gates[i].on = gates[i].in1.get().on ^ gates[i].in2.get().on;
                break;
            case 7:
                if (gates[i].in1.get() != null && gates[i].in2.get() != null)
                    gates[i].on = gates[i].in1.get().on !^ gates[i].in2.get().on;
                break;
        }
    }
}