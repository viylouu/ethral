partial class thehub {
    static gamebut plong_g = new gamebut { name = "plong", init = plong.init, rend = plong.rend, update = plong.update };
    static gamebut floopy_burd_g = new gamebut { name = "floopy burd", init = floopy_burd.init, rend = floopy_burd.rend, update = floopy_burd.update };

    static gamebut[] games = { 
        plong_g,
        floopy_burd_g,
    };

    static Color bgcol_dark, bgcol_light, butcol_dark, butcol_light, textcol;

    static Gradient bggrad;

    static float but_br = m.max(Window.Width, Window.Height) / 32,
                 but_x = Window.Width / 2,
                 but_y,
                 but_width = Window.Width / 3,
                 but_height = Window.Height / 12,
                 but_shad = Window.Height / 32,
                 but_ts = Window.Height / 24,
                 but_tshad = Window.Height / 128,
                 but_hamt = m.max(Window.Width, Window.Height) / 48;

    static Rectangle but;

    public static Action<ICanvas> rendact;
    static Action updact;
}