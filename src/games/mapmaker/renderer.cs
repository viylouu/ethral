partial class mapmaker {
    public static void rend(ICanvas c) {
        c.Clear(new Color(38, 38, 41));

        c.Fill(new Color(22, 22, 25));
        c.DrawRect(6, 6, tileselX-12, 720-12);

        for(int x = 0; x < 32; x++)
            for(int y = 0; y < 32; y++)
                c.DrawTexture(guillermotiles, new Rectangle(x*8,y*8,8,8), new Rectangle(tileselX+x*35, y*35, 28, 28));

        c.Fill(Color.White);

        for (int x = 1; x < 32; x++)
            c.DrawLine(tileselX+x*35-3.5f, 6, tileselX+x*35-3.5f, 720-6);

        for (int y = 1; y < 31; y++)
            c.DrawLine(tileselX+6, y*35-3.5f, 1280, y*35-3.5f);

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