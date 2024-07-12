partial class thehub {
    static void init() {
        Window.Title = "ethral: hub";

        ITexture palette = Graphics.LoadTexture(@"assets\hub\palette.png");

        bgcol_dark = palette.GetPixel(0,0);
        bgcol_light = palette.GetPixel(1,0);
        butcol_dark = palette.GetPixel(2,0);
        butcol_light = palette.GetPixel(3,0);
        textcol = palette.GetPixel(4,0);

        palette.Dispose();

        bggrad = new LinearGradient(0, Window.Height, Window.Width, 0, new Color[] { bgcol_dark, bgcol_light });
    }
}