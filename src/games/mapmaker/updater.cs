partial class mapmaker {
    public static void update() {
        if (!Mouse.IsButtonDown(MouseButton.Left))
            movingtileselx = false;

        tileselX = m.clmp(tileselX, 64, 1280 - 64);
    }
}