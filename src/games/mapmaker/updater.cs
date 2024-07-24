partial class mapmaker {
    public static void update() {
        if (!Mouse.IsButtonDown(MouseButton.Left))
        { movingtileselx = false; drawing = false; }

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

        int mapmx = (int)m.flr((Mouse.Position.X+cam.X)/24), 
            mapmy = (int)m.flr((Mouse.Position.Y+cam.Y)/24);

        if (
            Mouse.IsButtonDown(MouseButton.Left) &&
            mapmx >= 0 && mapmx < map.GetLength(0) && mapmy >= 0 && mapmy < map.GetLength(1) &&
            (!drawing? !(Mouse.Position.X > tileselX - 3 && Mouse.Position.X < tileselX + 3) : true) &&
            !movingtileselx
        )
        { map[mapmx, mapmy] = packxy(1, 5); drawing = true; }
    }

    static T[,] ResizeArray<T>(T[,] original, int rows, int cols) {
        var newArray = new T[rows,cols];
        int minRows = Math.Min(rows, original.GetLength(0));
        int minCols = Math.Min(cols, original.GetLength(1));
        for(int i = 0; i < minRows; i++)
            for(int j = 0; j < minCols; j++)
               newArray[i, j] = original[i, j];
        return newArray;
    }
}