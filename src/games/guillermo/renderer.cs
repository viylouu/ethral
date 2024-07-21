partial class guillermo {
    public static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.DrawTexture(player.tex, player.getcurframesrc(), new Rectangle(playerpos-campos + new Vector2(0,6), Vector2.One*24, Alignment.BottomCenter));

        c.Translate(spikepos-campos);
        c.Rotate(m.v2r(m.dirv(playerpos-campos, Mouse.Position))+m.rad);
        c.DrawTexture(spike, Vector2.Zero, new Vector2(5, 24), Alignment.Center);
        c.ResetState();
    }
}