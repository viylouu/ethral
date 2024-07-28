partial class thehub {
    class box {
        public Vector2 pos;
        public float ang;
        public Vector2 vel;
        public float angvel;
        public float size;
    }

    static gamebut plong_g = new gamebut { name = "plong", init = plong.init, rend = plong.rend, update = plong.update };
    static gamebut floopy_burd_g = new gamebut { name = "floopy burd", init = floopy_burd.init, rend = floopy_burd.rend, update = floopy_burd.update };
    static gamebut gateify_g = new gamebut { name = "gateify", init = gateify.init, rend = gateify.rend, update = gateify.update };
    static gamebut clonk_g = new gamebut { name = "clonk", init = clonk.init, rend = clonk.rend, update = clonk.update };
    static gamebut sp_g = new gamebut { name = "shader playground", init = shad_play.init, rend = shad_play.rend, update = shad_play.update };
    static gamebut guillermo_g = new gamebut { name = "¡guillermo!", init = guillermo.init, rend = guillermo.rend, update = guillermo.update };
    static gamebut mapmaker_g = new gamebut { name = "mapmaker", init = mapmaker.init, rend = mapmaker.rend, update = mapmaker.update };
    static gamebut waves_g = new gamebut { name = "waves", init = waves.init, rend = waves.rend, update = waves.update };
    static gamebut basilisk_g = new gamebut { name = "basilisk", init = basilisk.init, rend = basilisk.rend, update = basilisk.update };

    static gamebut[] games = { 
        plong_g,
        floopy_burd_g,
        gateify_g,
        clonk_g,
        guillermo_g,
        basilisk_g
    };

    static Color bgcol_dark, bgcol_light, butcol_dark, butcol_light, textcol;

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

    static List<box> bgboxes = new List<box>();
    static float boxadddel = 1f / 6;
    static float boxaddcount = 0;

    static int konamicorr;
    static bool devtools = true;
    static int gameslength = games.Length;
}