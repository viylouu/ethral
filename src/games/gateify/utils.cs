partial class gateify {
    static float tossX(float x) => x*24/zoom-cam.X;
    static float tossY(float x) => x*24/zoom-cam.Y;
    static Vector2 toss(Vector2 x) => x*24/zoom-cam;
}