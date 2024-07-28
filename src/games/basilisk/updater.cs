partial class basilisk {
    public static void update() { 
        /*if (Keyboard.IsKeyPressed(Key.Esc)) { 
            if(updbthigh)
                using (StreamWriter sw = new StreamWriter(@"assets\savedata\basilisk\broken.dat"))
                    sw.Write(bthigh);

            Environment.Exit(0);
        }*/

        map_upd -= Time.DeltaTime;

        expand_map();
        generate_nulls();

        if (map_upd <= 0) {
            map_upd = map_upd_speed;
            simulate_chunks();
        }

        if(Time.DeltaTime < 1f/12)
            cam = e.dist(cam, p.pos*2 - new Vector2(320, 180) + new Vector2(6, 8), 4);

        { //creative or something

            if (Keyboard.IsKeyDown(Key.W))
                p.pos.Y -= Time.DeltaTime * 48;
            if (Keyboard.IsKeyDown(Key.S))
                p.pos.Y += Time.DeltaTime * 48;
            if (Keyboard.IsKeyDown(Key.D))
                p.pos.X += Time.DeltaTime * 48;
            if (Keyboard.IsKeyDown(Key.A))
                p.pos.X -= Time.DeltaTime * 48;

            if (Mouse.IsButtonPressed(MouseButton.Left))
                p.pos = (Mouse.Position + cam) / 2 - new Vector2(3, 4);

            if (Keyboard.IsKeyPressed(Key.E))
                for (int y = -32; y < 32; y++)
                    for (int x = -32; x < 32; x++)
                        if (x * x + y * y <= 1024)
                            place(p.pos.X + 3 + x, p.pos.Y + 4 + y, null);

            if (Keyboard.IsKeyPressed(Key.C))
                for (int y = -8; y < 8; y++)
                    for (int x = -8; x < 8; x++)
                        if (x * x + y * y <= 64)
                            place(p.pos.X + 3 + x, p.pos.Y + 4 + y, grass);

        }

        if (Keyboard.IsKeyPressed(Key.Esc))
            menuOpen = !menuOpen;

        if (menuOpen) {
            ImGui.Begin("menu");

            if (ImGui.Button("close")) 
            { thehub.rendact = null; Window.Title = "ethral: hub"; }

            if(ImGui.Button("save silly data") && updbthigh)
                using (StreamWriter sw = new StreamWriter(@"assets\savedata\basilisk\broken.dat"))
                    sw.Write(bthigh);

            ImGui.End();
        }
    }

    static async void expand_map() {
        try {
            int windowHeight = (int)(Window.Height + cam.Y);
            int windowWidth = (int)(Window.Width + cam.X);

            int minYThreshold = -(int)cam.Y / chunkSize / 2 +1;
            int minXThreshold = -(int)cam.X / chunkSize / 2 +1;

            int j = 0;

            // expand downwards
            while ((chunks.Count - mapoffset.Y) * chunkSize < windowHeight) {
                List<chunk> newColumn = new List<chunk>();
                for (int i = 0; i < chunks[0].Count; i++) {
                    lock (newColumn)
                        newColumn.Add(new chunk { tex = null, created = false, data = new cdat[chunkSizeSqr] });

                    j = (j+1)%96;

                    if (j == 0)
                        await Task.Delay(1);
                }
                lock (chunks)
                    chunks.Add(newColumn);
            }

            // expand rightwards
            while ((chunks[0].Count - mapoffset.X) * chunkSize < windowWidth)
                foreach (var column in chunks) {
                    lock(column)
                        column.Add(new chunk { tex = null, created = false, data = new cdat[chunkSizeSqr] });

                    j = (j + 1) % 96;

                    if (j == 0)
                        await Task.Delay(1);
                }

            // expand upwards
            while (mapoffset.Y < minYThreshold) {
                List<chunk> newColumn = new List<chunk>();
                for (int i = 0; i < chunks[0].Count; i++) {
                    lock (newColumn)
                        newColumn.Add(new chunk { tex = null, created = false, data = new cdat[chunkSizeSqr] });

                    j = (j + 1) % 96;

                    if (j == 0)
                        await Task.Delay(1);
                }
                lock (chunks)
                    chunks.Insert(0, newColumn);
                mapoffset.Y++;
            }

            // expand leftwards
            while (mapoffset.X < minXThreshold) {
                foreach (var column in chunks) {
                    lock (column)
                        column.Insert(0, new chunk { tex = null, created = false, data = new cdat[chunkSizeSqr] });

                    j = (j + 1) % 96;

                    if (j == 0)
                        await Task.Delay(1);
                }
                mapoffset.X++;
            }
        } catch (Exception e) {
            broketimes++;

            if (broketimes > bthigh) {
                bthigh = broketimes;

                updbthigh = true;

                Console.WriteLine($"new highscore! oopsie doopsie #{broketimes}: {e.Message}");
            }
            else
                Console.WriteLine($"oopsie doopsie #{broketimes}: {e.Message}");
        }
    }

    static void place(float xt, float yt, cell c) {
        int x = (int)xt, y = (int)yt;
        int chunkX = (int)m.flr(x / chunkSize + mapoffset.X);
        int chunkY = (int)m.flr(y / chunkSize + mapoffset.Y);
        int localX = x % chunkSize;
        int localY = y % chunkSize;

        if (chunkX >= 0 && chunkY >= 0 && chunkX < chunks[0].Count && chunkY < chunks.Count) {
            try {
                chunk currentChunk = chunks[chunkY][chunkX];
                currentChunk.data[to1D(localX, localY)] = new cdat { cbehavior = (byte)Array.IndexOf(cells, c) };

                Color col = Color.Transparent;

                if (c != null) {
                    if (c.randcol)
                        col = Color.Lerp(tocol(c.c1), tocol(c.c2), m.rnd(m.rand(0, 1f) * 4) / 4);
                    else
                        col = Color.Lerp(tocol(c.c1), tocol(c.c2), m.rnd((fnl2.GetNoise((x % chunkSize / (float)chunkSize + (int)m.flr(x / chunkSize) - mapoffset.X) * chunkSize, (y % chunkSize / (float)chunkSize + (int)m.flr(y / chunkSize) - mapoffset.Y) * chunkSize) + m.rand(.35f, .65f)) * 4) / 4);

                    currentChunk.data[to1D(localX, localY)].mycolon = tocol(col);
                }

                currentChunk.tex.SetPixel(localX, localY, col);
                currentChunk.updated = true;
            } catch (Exception e) {
                // Handle exception or log it if necessary
            }
        }
    }
    
    static void simulate_chunks() {
        for (int j = (int)m.flr(p.pos.Y/chunkSize+mapoffset.Y)-8; j < (int)m.flr(p.pos.Y/chunkSize+mapoffset.Y)+8; j++)
            for (int i = (int)m.flr(p.pos.X/chunkSize+mapoffset.X)-8; i < (int)m.flr(p.pos.X/chunkSize+mapoffset.X)+8; i++)
                if(j>=0&&i>=0&&j<chunks.Count&&i<chunks[j].Count) {
                    if (chunks[j][i].updated)
                        chunks[j][i].updating = true;

                    if (chunks[j][i].updating) {
                        bool changed = false;

                        if (chunks[j][i].cellsmovedother == null)
                            chunks[j][i].cellsmoved = new bool[chunkSizeSqr];
                        else
                            chunks[j][i].cellsmoved = chunks[j][i].cellsmovedother;

                        chunks[j][i].cellsmovedother = new bool[chunkSizeSqr];

                        if (chunks[j][i] != null)
                            if (chunks[j][i].data != null)
                                for (int x = 0; x < chunkSize; x++)
                                    for (int y = 0; y < chunkSize; y++)
                                        if(chunks[j][i].data[to1D(x, y)] != null)
                                            if (chunks[j][i].data[to1D(x, y)].cbehavior != Array.IndexOf(cells, null)) {
                                                if (cells[chunks[j][i].data[to1D(x, y)].cbehavior].move && !chunks[j][i].cellsmoved[to1D(x, y)]) {
                                                    cell me = cells[chunks[j][i].data[to1D(x, y)].cbehavior];
                                                    
                                                    if(chunks[j][i].data[to1D(x, y)].vy == 0) {
                                                        if (movecell(j, i, x, y, 0, 1)) {
                                                            changed = true;
                                                            //chunks[j][i].data[to1D(x, y)].vy++;
                                                            continue;
                                                        }
                                                    } else { 
                                                        if (movecell(j, i, x, y, 0, chunks[j][i].data[to1D(x, y)].vy)) {
                                                            changed = true;
                                                            //chunks[j][i].data[to1D(x, y)].vy++;
                                                            continue;
                                                        }
                                                    }

                                                    if (me.sidemove) {
                                                        if(chunks[j][i].data[to1D(x, y)].vy != 0) {
                                                            if (movecell(j, i, x, y, (sbyte)-m.abs(chunks[j][i].data[to1D(x, y)].vy), (sbyte)-m.abs(chunks[j][i].data[to1D(x, y)].vy))) {
                                                                changed = true;
                                                                //chunks[j][i].data[to1D(x, y)].vy++;
                                                                //chunks[j][i].data[to1D(x, y)].vx = (sbyte)-m.abs(chunks[j][i].data[to1D(x, y)].vy);
                                                                continue;
                                                            }

                                                            if (movecell(j, i, x, y, (sbyte)m.abs(chunks[j][i].data[to1D(x, y)].vy), (sbyte)-m.abs(chunks[j][i].data[to1D(x, y)].vy))) {
                                                                changed = true;
                                                                chunks[j][i].data[to1D(x, y)].vy++;
                                                                chunks[j][i].data[to1D(x, y)].vx = (sbyte)m.abs(chunks[j][i].data[to1D(x, y)].vy);
                                                                continue;
                                                            }
                                                        } else { 
                                                            if (movecell(j, i, x, y, -1, 1)) {
                                                                changed = true;
                                                                //chunks[j][i].data[to1D(x, y)].vy++;
                                                                //chunks[j][i].data[to1D(x, y)].vx = (sbyte)-m.abs(chunks[j][i].data[to1D(x, y)].vy);
                                                                continue;
                                                            }

                                                            if (movecell(j, i, x, y, 1, 1)) {
                                                                changed = true;
                                                                //chunks[j][i].data[to1D(x, y)].vy++;
                                                                //chunks[j][i].data[to1D(x, y)].vx = (sbyte)m.abs(chunks[j][i].data[to1D(x, y)].vy);
                                                                continue;
                                                            }
                                                        }
                                                    }

                                                    if (chunks[j][i].data[to1D(x, y)].vx != 0) {
                                                        if (chunks[j][i].data[to1D(x, y)].vx > 0)
                                                            chunks[j][i].data[to1D(x, y)].vx--;
                                                        if (chunks[j][i].data[to1D(x, y)].vx < 0)
                                                            chunks[j][i].data[to1D(x, y)].vx++;
                                                    }

                                                    //chunks[j][i].data[to1D(x,y)].vy = 0;
                                                }
                                            }

                        if (changed || chunks[j][i].updated) {
                            if (chunks[j][i].tex != null)
                                chunks[j][i].tex.ApplyChanges(); 
                            chunks[j][i].nomoveframes = 0; 
                        }
                        else {
                            chunks[j][i].nomoveframes++;

                            if(chunks[j][i].nomoveframes >= 2)
                                chunks[j][i].updating = false;
                        }
                    }

                    chunks[j][i].updated = false;
                }
    }

    static bool movecell(int j, int i, int x, int y, int xOffset, int yOffset) {
        int newX = x + xOffset;
        int newY = y + yOffset;
        int newChunkJ = j;
        int newChunkI = i;

        while (newX < 0) {
            newX += chunkSize;
            newChunkI--;
        }
        while (newX >= chunkSize) {
            newX -= chunkSize;
            newChunkI++;
        }

        while (newY < 0) {
            newY += chunkSize;
            newChunkJ--;
        }
        while (newY >= chunkSize) {
            newY -= chunkSize;
            newChunkJ++;
        }
        
        if (newChunkJ < 0 || newChunkJ >= chunks.Count || newChunkI < 0 || newChunkI >= chunks[newChunkJ].Count)
            return false;

        if (chunks[newChunkJ][newChunkI].tex == null)
            return false;

        if (chunks[newChunkJ][newChunkI].data[to1D(newX, newY)] == null || (chunks[newChunkJ][newChunkI].data[to1D(newX, newY)] != null?(chunks[newChunkJ][newChunkI].data[to1D(newX, newY)].cbehavior == Array.IndexOf(cells, null)):false)) {
            chunks[newChunkJ][newChunkI].data[to1D(newX, newY)] = chunks[j][i].data[to1D(x, y)];
            chunks[newChunkJ][newChunkI].data[to1D(newX, newY)].mycolon = chunks[j][i].data[to1D(x, y)].mycolon;
            chunks[j][i].data[to1D(x, y)] = null;

            if(chunks[newChunkJ][newChunkI].data[to1D(newX, newY)] != null || (chunks[newChunkJ][newChunkI].data[to1D(newX, newY)] != null ? (chunks[newChunkJ][newChunkI].data[to1D(newX, newY)].cbehavior != Array.IndexOf(cells, null)) : true))
                chunks[newChunkJ][newChunkI].tex.SetPixel(newX, newY, tocol(chunks[newChunkJ][newChunkI].data[to1D(newX, newY)].mycolon));
            else
                chunks[newChunkJ][newChunkI].tex.SetPixel(newX, newY, Color.Transparent);
            chunks[j][i].tex.SetPixel(x, y, Color.Transparent);

            if (newChunkJ == j && newChunkI == i) {
                chunks[j][i].cellsmoved[to1D(newX, newY)] = true;

                if(newChunkJ>0)
                    chunks[newChunkJ-1][newChunkI].updated = true;
                if (newChunkJ < chunks.Count-1)
                    chunks[newChunkJ + 1][newChunkI].updated = true;
                if (newChunkI > 0)
                    chunks[newChunkJ][newChunkI - 1].updated = true;
                if (newChunkI < chunks[newChunkJ].Count - 1)
                    chunks[newChunkJ][newChunkI + 1].updated = true;
            }
            else if (chunks[newChunkJ][newChunkI].cellsmovedother != null) {
                chunks[newChunkJ][newChunkI].cellsmovedother[to1D(newX, newY)] = true;

                chunks[newChunkJ][newChunkI].updated = true;

                if(newChunkJ>0)
                    chunks[newChunkJ-1][newChunkI].updated = true;
                if (newChunkJ < chunks.Count-1)
                    chunks[newChunkJ + 1][newChunkI].updated = true;
                if (newChunkI > 0)
                    chunks[newChunkJ][newChunkI - 1].updated = true;
                if (newChunkI < chunks[newChunkJ].Count - 1)
                    chunks[newChunkJ][newChunkI + 1].updated = true;
            }

            return true;
        }

        return false;
    }

    static void generate_nulls() {
        int i = 0, j = 0, a = 0;

        for (int y = (int)m.flr(p.pos.Y/chunkSize+mapoffset.Y)-12; y < (int)m.flr(p.pos.Y/chunkSize+mapoffset.Y)+12; y++)
            for (int x = (int)m.flr(p.pos.X/chunkSize+mapoffset.X)-12; x < (int)m.flr(p.pos.X/chunkSize+mapoffset.X)+12; x++) 
                if(y>=0&&x>=0&&y<chunks.Count&&x<chunks[y].Count) {
                    if (chunks[y][x].tex == null)
                        chunks[y][x].tex = Graphics.CreateTexture(chunkSize, chunkSize);

                    var chunk = chunks[y][x];

                    if (!chunk.created && chunk.data != null) {
                        bool changed = false;
                        for (int x2 = 0; x2 < chunkSize; x2++)
                            for (int y2 = 0; y2 < chunkSize; y2++) {
                                if (fnl1.GetNoise(((x2 / (float)chunkSize) + x - mapoffset.X) * chunkSize, ((y2 / (float)chunkSize) + y - mapoffset.Y) * chunkSize) > 0 && (chunkSize * y + y2 - mapoffset.Y*chunkSize) > fnl1.GetNoise(((x2 / (float)chunkSize) + x - mapoffset.X) * chunkSize, 0) * chunkSize + chunkSize * 1.75f) {
                                    chunk.data[to1D(x2, y2)] = new cdat();

                                    if ((chunkSize * y + y2 - mapoffset.Y * chunkSize) < fnl1.GetNoise(((x2 / (float)chunkSize) + x - mapoffset.X) * chunkSize, 0) * chunkSize + chunkSize * 2) {
                                        chunk.data[to1D(x2, y2)].cbehavior = (byte)Array.IndexOf(cells, grass);
                                        if (!grass.randcol)
                                            chunk.data[to1D(x2, y2)].mycolon = tocol(Color.Lerp(tocol(grass.c1), tocol(grass.c2), m.rnd((fnl2.GetNoise((x2 / (float)chunkSize + x - mapoffset.X) * chunkSize, (y2 / (float)chunkSize + y - mapoffset.Y) * chunkSize) + m.rand(.35f, .65f)) * 4) / 4));
                                        else
                                            chunk.data[to1D(x2, y2)].mycolon = tocol(Color.Lerp(tocol(grass.c1), tocol(grass.c2), m.rnd(m.rand(0, 1f) * 4) / 4));
                                        chunk.tex.SetPixel(x2, y2, tocol(chunk.data[to1D(x2, y2)].mycolon));
                                        changed = true;
                                        chunk.updating = true;
                                    } else if ((chunkSize * y + y2 - mapoffset.Y * chunkSize) < fnl1.GetNoise(((x2 / (float)chunkSize) + x - mapoffset.X) * chunkSize, 0) * chunkSize + chunkSize * 2.35f) {
                                        chunk.data[to1D(x2, y2)].cbehavior = (byte)Array.IndexOf(cells, dirt);
                                        if (!dirt.randcol)
                                            chunk.data[to1D(x2, y2)].mycolon = tocol(Color.Lerp(tocol(dirt.c1), tocol(dirt.c2), m.rnd((fnl2.GetNoise((x2 / (float)chunkSize + x - mapoffset.X) * chunkSize, (y2 / (float)chunkSize + y - mapoffset.Y) * chunkSize) + m.rand(.35f, .65f)) * 4) / 4));
                                        else
                                            chunk.data[to1D(x2, y2)].mycolon = tocol(Color.Lerp(tocol(dirt.c1), tocol(dirt.c2), m.rnd(m.rand(0, 1f) * 4) / 4));
                                        chunk.tex.SetPixel(x2, y2, tocol(chunk.data[to1D(x2, y2)].mycolon));
                                        changed = true;
                                        chunk.updating = true;
                                    } else {
                                        chunk.data[to1D(x2, y2)].cbehavior = (byte)Array.IndexOf(cells, stone);
                                        if (!stone.randcol)
                                            chunk.data[to1D(x2, y2)].mycolon = tocol(Color.Lerp(tocol(stone.c1), tocol(stone.c2), m.rnd((fnl2.GetNoise((x2 / (float)chunkSize + x - mapoffset.X) * chunkSize, (y2 / (float)chunkSize + y - mapoffset.Y) * chunkSize) + m.rand(.35f, .65f)) * 4) / 4));
                                        else
                                            chunk.data[to1D(x2, y2)].mycolon = tocol(Color.Lerp(tocol(stone.c1), tocol(stone.c2), m.rnd(m.rand(0, 1f) * 4) / 4));
                                        chunk.tex.SetPixel(x2, y2, tocol(chunk.data[to1D(x2, y2)].mycolon));
                                        changed = true;
                                    }
                                }

                                j++;
                                j %= 96;
                            }
                        if (changed)
                            chunk.tex.ApplyChanges();
                        chunk.created = true;
                }
            }
    }
}