partial class basilisk { 
    class chunk {
        public ITexture tex;
        public bool created;
        public cdat[] data;
        public bool[] cellsmoved;
        public bool[] cellsmovedother;
        public bool updating;
        public bool updated;
        public int nomoveframes;
    }

    class datchunk {
        public ctex tex;
        public bool created;
        public cdat[] data;
        public bool[] cellsmoved;
        public bool[] cellsmovedother;
        public bool updating;
        public bool updated;
        public int nomoveframes;
    }

    class cdat {
        public byte cbehavior=0;
        public col mycolon;
        public sbyte vx=0, vy=0;
    }

    class cell {
        public bool move, 
                    sidemove;
        public col c1, c2;
        public bool randcol;
        public int tileidx;
        public bool hastile;
    }

    class player {
        public Vector2 pos;
        public Vector2 vel;
    }

    class fatterthanurmomdata {
        public datchunk[,] chunks;
    }

    class smalldata {
        public player p;
        public int seed1, seed2;
        public Vector2 mapoffset;

        public long i, j;
    }

    static cell stone = new cell {
        c1 = new col(34, 35, 38),
        c2 = new col(51, 52, 56),
        hastile = true,
        tileidx = 0
    };

    static cell andesite = new cell {
        c1 = new col(47, 47, 51),
        c2 = new col(70, 70, 74),
        hastile = true,
        tileidx = 1
    };

    static cell sand = new cell {
        move = true,
        sidemove = true,
        c1 = new col(237, 171, 71),
        c2 = new col(247, 193, 92),
        randcol = true
    };

    static cell dirt = new cell { 
        //move = true,
        c1 = new col(70, 50, 40),
        c2 = new col(110, 80, 60)
    };

    static cell grass = new cell { 
        //move = true,
        //sidemove = true,
        c1 = new col(40, 60, 40),
        c2 = new col(60, 80, 50)
    };

    public struct col {
        public byte r, g, b;

        public col(byte R, byte G, byte B) {
            r = R;
            g = G;
            b = B;
        }
    }

    static cell[] cells = { null, stone, andesite, sand, dirt, grass };

    public static int chunkSize = 64;
    static int chunkSizeSqr = chunkSize * chunkSize;

    static List<List<chunk>> chunks = new List<List<chunk>>();

    static Vector2 mapoffset;

    static FNL fnl1, fnl2, fnl3;

    static Vector2 cam;

    static float map_upd_speed = 1f / 60;
    static float map_upd;

    static player p = new player();

    static int seed, texseed;

    static int broketimes = 0;

    static int bthigh = 0;

    static bool updbthigh;

    static bool menuOpen;

    static Gradient sky;
    static Gradient undgrad;

    static ITexture tiles;
}