partial class gateify {
    class node {
        public byte gate;
        public Vector2 pos;
        public bool on;
        public bool dragged;
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

    static ITexture gatespr;

    static Vector2 cam;
    static float zoom = 1;

    static int curselgate;
    static string[] gateenum = { "buffer", "not", "and", "nand", "or", "nor", "xor", "xnor", "splitter", "continuer", "input", "output" };
    static int placeX,placeY;
} 