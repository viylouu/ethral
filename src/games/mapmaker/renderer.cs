partial class mapmaker {
    public static void rend(ICanvas c) {
        c.Clear(new Color(38, 38, 41));

        c.Fill(new Color(22, 22, 25));
        c.DrawRect(6, 6, tileselX-12, 720-12);

        c.DrawTexture(guillermotiles, tileselX, 6, 720-12, 720-12);

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