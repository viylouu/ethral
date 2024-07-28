partial class basilisk {
    public static void rend(ICanvas c) { 
        c.Clear(Color.Black);

        c.Antialias(false);
        
        for (int y = (int)m.flr(cam.Y/2/chunkSize+mapoffset.Y-1); y < (int)m.flr((cam.Y+Window.Height)/2/chunkSize+mapoffset.Y+1); y++)
            for (int x = (int)m.flr(cam.X/2/chunkSize+mapoffset.X-1); x < (int)m.flr((cam.X+Window.Width)/2/chunkSize+mapoffset.X+1); x++)
                if(x >= 0 && y >= 0 && x < chunks[(int)m.flr(y/chunkSize)].Count && y < chunks.Count)
                    if (chunks[y][x] != null && (x-mapoffset.X)*chunkSize*2-cam.X>-chunkSize*2&&(y-mapoffset.Y)*chunkSize*2-cam.Y>-chunkSize*2&&(x-mapoffset.X)*chunkSize*2-cam.X<Window.Width&&(y-mapoffset.Y)*chunkSize*2-cam.Y<Window.Height)
                        if(chunks[y][x].created) {
                            c.DrawTexture(chunks[y][x].tex, (new Vector2(x, y) - mapoffset) * chunkSize * 2-cam, Vector2.One * chunkSize * 2);

                            c.Stroke(Color.Green);

                            if (chunks[y][x].updating)
                                c.DrawRect((new Vector2(x, y) - mapoffset) * chunkSize * 2 - cam, Vector2.One * chunkSize * 2);

                            c.Stroke(Color.Red);
                        }

        c.Fill(Color.White);
        c.DrawRect(p.pos*2 - cam, new Vector2(6,8)*2);

        c.Fill(Color.White);
        c.FontSize(12);
        c.DrawText(m.rnd(1/Time.DeltaTime) + " fps", Vector2.One*3);
        c.DrawText("seed: " + seed, new Vector2(3, 19));
        c.DrawText($"(x:{m.rnd(p.pos.X)},y:{m.rnd(p.pos.Y)})", new Vector2(3, 35));
        c.DrawText($"(i:{m.flr(p.pos.X/chunkSize*2)},j:{m.flr(p.pos.Y/chunkSize*2)})", new Vector2(3, 51));
        c.DrawText($"(r:{chunks[0].Count},g:{chunks.Count})", new Vector2(3, 67));
    }
}