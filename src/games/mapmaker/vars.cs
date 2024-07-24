partial class mapmaker {
    static ITexture guillermotiles;

    static float tileselX = 1280/1.5f;
    static bool movingtileselx = false;

    static Vector2 cam;
    static bool dragging;

    static ushort[,] map = new ushort[1,1];
    static int mapsizex=1, mapsizey=1;

    static bool drawing;
}