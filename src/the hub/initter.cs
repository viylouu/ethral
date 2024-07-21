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

        addsound(@"assets\hub\select.wav");
        addsound(@"assets\hub\correct.wav");

        for(int i = 1; i <= 11; i++)
            addsound(@"assets\hub\"+i+".wav");
    }
}