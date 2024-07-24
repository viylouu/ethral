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

        ImGui.Begin("map properties");

        int pmapsizex = mapsizex, pmapsizey = mapsizey;

        ImGui.InputInt("x size", ref mapsizex);
        ImGui.InputInt("y size", ref mapsizey);

        mapsizex = m.max(1, mapsizex);
        mapsizey = m.max(1, mapsizey);

        if (mapsizex != pmapsizex || mapsizey != pmapsizey)
            map = ResizeArray(map, mapsizex, mapsizey);

        ImGui.End();
    }

    static T[,] ResizeArray<T>(T[,] original, int rows, int cols) {
        var newArray = new T[rows,cols];
        Array.Copy(original, newArray, original.Length);
        return newArray;
    }
}