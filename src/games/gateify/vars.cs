partial class gateify {
    class node {
        public byte gate;
        public Vector2 pos;
        public bool on;
        public bool dragged;
        public node in1, in2, out1, out2;
    }

    enum gate {
        buff,
        not,
        and,
        nand,
        or,
        nor,
        xor,
        xnor,
        disp
    }

    static List<node> gates = new List<node>();

    static ITexture gatespr, dot, dotemp;

    static Vector2 cam;
    static float zoom = 1;

    static int curselgate;
    static string[] gateenum = { "buffer", "not", "and", "nand", "or", "nor", "xor", "xnor", "splitter", "continuer", "input", "output" };
    static int placeX,placeY;

    static bool wiring, wire2b, wire2f;
    static int wireI, wireio;

    static bool menuOpen;

    static WaveStream inptoggle = new WaveFileReader(@"assets\gateify\inputtoggle.wav"),
                      nodecrt = new WaveFileReader(@"assets\gateify\nodecreate.wav"),
                      nodedel = new WaveFileReader(@"assets\gateify\nodedelete.wav"),
                      nodegrab = new WaveFileReader(@"assets\gateify\nodegrab.wav"),
                      nodeplace = new WaveFileReader(@"assets\gateify\nodeplace.wav"),
                      wireend = new WaveFileReader(@"assets\gateify\wireend.wav"),
                      wirestart = new WaveFileReader(@"assets\gateify\wirestart.wav");

    static WaveOutEvent inptoggleO = new WaveOutEvent(),
                        nodecrtO = new WaveOutEvent(),
                        nodedelO = new WaveOutEvent(),
                        nodegrabO = new WaveOutEvent(),
                        nodeplaceO = new WaveOutEvent(),
                        wireendO = new WaveOutEvent(),
                        wirestartO = new WaveOutEvent();
} 