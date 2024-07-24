partial class mapmaker {
    public static void rend(ICanvas c) {
        c.Clear(new Color(38, 38, 41));

        c.Fill(new Color(22, 22, 25));
        c.DrawRect(6, 6, tileselX-12, 720-12);

        c.Fill(new Color(38, 38, 41));

        for (int x = 6; x < tileselX-12; x+=24)
            c.DrawLine(x-cam.X%24, 6, x-cam.X%24, 720-6);

        for (int y = 6; y < 720-12; y+=24)
            c.DrawLine(6, y-cam.Y%24, tileselX-12, y-cam.Y%24);

        c.DrawRect(-cam.X+6, -cam.Y+6, map.GetLength(0)*24, map.GetLength(1)*24);

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

            if (Mouse.IsButtonDown(MouseButton.Left))
                movingtileselx = true;
        }

        if(movingtileselx) {
            c.DrawLine(tileselX, 0, tileselX, 720);
            tileselX = Mouse.Position.X;
        }
    }
}