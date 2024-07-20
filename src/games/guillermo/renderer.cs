partial class guillermo {
    public static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.DrawTexture(player.tex, player.getcurframesrc(), new Rectangle(Window.Width/2, Window.Height/2, 512, 512, Alignment.Center));
    }
}5