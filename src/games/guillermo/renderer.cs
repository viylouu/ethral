partial class guillermo {
    public static void rend(ICanvas c) {
        c.DrawTexture(player.tex, player.getcurframesrc(), new Rectangle(Window.Width/2, Window.Height/2, 96, 96, Alignment.Center));
    }
}