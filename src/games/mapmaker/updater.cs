partial class mapmaker {
    public static void update() {
        if (!Mouse.IsButtonDown(MouseButton.Left))
            movingtileselx = false;

        tileselX = m.clmp(tileselX, 256, 1280 - 64);

        if (Mouse.IsButtonDown(MouseButton.Middle) && new Rectangle(6,6,tileselX-12,720-12).ContainsPoint(Mouse.Position))
            dragging = true;

        if (!Mouse.IsButtonDown(MouseButton.Middle))
            dragging = false;

        if (dragging)
            cam -= Mouse.DeltaPosition;
    }
}