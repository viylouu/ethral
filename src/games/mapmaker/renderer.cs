partial class mapmaker {
    public static void rend(ICanvas c) {
        c.Clear(new Color(22, 22, 25)); //22, 22, 25

        //c.Fill(new Color(22, 22, 25));
        //c.DrawRect(6, 6, tileselX-12, 720-12);

        c.Fill(new Color(38, 38, 41));

        for (int x = 6; x < tileselX-12; x+=24)
            c.DrawLine(x-cam.X%24, 6, x-cam.X%24, 720-6);

        for (int y = 6; y < 720-12+1; y+=24)
            c.DrawLine(6, y-cam.Y%24, tileselX-12, y-cam.Y%24);

        c.DrawRect(-cam.X+6, -cam.Y+6, map.GetLength(0)*24, map.GetLength(1)*24);

        for (int x = 0; x < map.GetLength(0); x++)
            for (int y = 0; y < map.GetLength(1); y++)
                if (map[x,y] != 0) {
                    (byte i, byte j) = unpack(map[x,y]);
                    c.DrawTexture(guillermotiles, new Rectangle(i*8, j*8, 8, 8), new Rectangle((x*24)-cam.X+6, (y*24)-cam.Y+6, 24, 24));
                }
        
        if(Mouse.Position.X >= 6 && Mouse.Position.Y >= 6 && Mouse.Position.X <= tileselX-12 && Mouse.Position.Y <= 720-12) {
            c.Fill(new Color(255, 255, 255, 150));
            c.DrawRect(m.flr((Mouse.Position.X+cam.X%24)/24)*24-cam.X%24+6, m.flr((Mouse.Position.Y+cam.Y%24)/24)*24-cam.Y%24+6, 24, 24);
        }

        c.Fill(new Color(38, 38, 41));
        c.DrawRect(0,0,6,720); c.DrawRect(6,0,1280,6); c.DrawRect(tileselX-6,0,1280,720); c.DrawRect(0,720-6,1280,6);

        for(int x = 0; x < 32; x++)
            for(int y = 0; y < 32; y++)
                c.DrawTexture(guillermotiles, new Rectangle(x*8,y*8,8,8), new Rectangle(tileselX+x*35, y*35, 28, 28));

        c.Fill(new Color(53, 53, 59));

        for (int x = 1; x < 32; x++)
            c.DrawLine(tileselX+x*35-3.5f, 0, tileselX+x*35-3.5f, 720);

        for (int y = 0; y < 32; y++)
            c.DrawLine(tileselX, y*35-3.5f, 1280, y*35-3.5f);

        c.Antialias(false);
        c.Fill(Color.White);
        c.StrokeWidth(3);
        if (Mouse.Position.X > tileselX - 3 && Mouse.Position.X < tileselX + 3) {
            c.DrawLine(tileselX, 0, tileselX, 720);

            if (Mouse.IsButtonDown(MouseButton.Left) && !drawing)
                movingtileselx = true;
        }

        if(movingtileselx) {
            c.DrawLine(tileselX, 0, tileselX, 720);
            tileselX = Mouse.Position.X;
        }

        c.Fill(Color.White);
        c.DrawText(m.rnd(1/Time.DeltaTime) + " fps", 0, 0);
    }

    static ushort packxy(byte x, byte y) => (ushort)((x<<8)|y);
    static (byte, byte) unpack(ushort a) => ((byte)((a >> 8) & 0xFF), (byte)(a & 0xFF));
}