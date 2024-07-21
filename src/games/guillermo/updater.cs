partial class guillermo {
    public static void update() {
        spikepos = e.dist(spikepos, playerpos + m.dirv(playerpos - campos, Mouse.Position) * 12, 5);
        campos = e.dist(campos, playerpos - new Vector2(Window.Width / 2, Window.Height / 2), 5);
    }
}