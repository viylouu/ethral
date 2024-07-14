partial class gateify {
    public static void update() {
        if (Mouse.IsButtonDown(MouseButton.Middle))
            cam -= Mouse.DeltaPosition;

        zoom -= Mouse.ScrollWheelDelta / 64;
        zoom = m.clmp(zoom, .015625f, 2);

        ImGui.Begin("spawner");

        ImGui.ListBox("gates", ref curselgate, gateenum, gateenum.Length);
        ImGui.InputInt("pos x", ref placeX);
        ImGui.InputInt("pos y", ref placeY);

        if (ImGui.Button("add"))
            gates.Add(new node { gate = (byte)curselgate, on = false, pos = new Vector2(placeX, placeY) });

        ImGui.End();
    }
}