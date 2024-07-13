partial class plong {
    public static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.Antialias(false);

        c.Fill(Color.White);

        c.DrawRect(paddleLpos, new Vector2(2, 16), Alignment.Center);
        c.DrawRect(paddleRpos, new Vector2(2, 16), Alignment.Center);
        c.DrawRect(ballpos, new Vector2(2, 2), Alignment.Center);
                                                                                                                                                        
        c.FontSize(12);
        c.DrawText(lscore + "", new Vector2(16, 8), Alignment.Center);
        c.DrawText(rscore + "", new Vector2(144, 8), Alignment.Center);
    }
}