partial class clonk {
    public static void update() {
        if (focused) {
            pitch += (Mouse.Position.X - m.rnd(Window.Width / 2f)) / 8;
            yaw += (Mouse.Position.Y - m.rnd(Window.Height / 2f)) / 8;
            yaw = m.clmp(yaw, -90, 90);
        }

        if (Mouse.IsButtonPressed(MouseButton.Left))
            focused = true;

        if (Keyboard.IsKeyPressed(Key.Esc))
            focused = false;

        if (focused)
            Mouse.Position = new Vector2(Window.Width / 2, Window.Height / 2);

        Mouse.Visible = !focused;

        float spd = 8;

        if (Keyboard.IsKeyDown(Key.A))
            cam += new Vector3(m.cos(m.d2r(pitch)), 0, m.sin(m.d2r(pitch))) * Time.DeltaTime * spd;
        if (Keyboard.IsKeyDown(Key.D))
            cam -= new Vector3(m.cos(m.d2r(pitch)), 0, m.sin(m.d2r(pitch))) * Time.DeltaTime * spd;
        if (Keyboard.IsKeyDown(Key.W))
            cam += new Vector3(m.cos(m.d2r(pitch + 90)), 0, m.sin(m.d2r(pitch + 90))) * Time.DeltaTime * spd;
        if (Keyboard.IsKeyDown(Key.S))
            cam -= new Vector3(m.cos(m.d2r(pitch + 90)), 0, m.sin(m.d2r(pitch + 90))) * Time.DeltaTime * spd;
        if (Keyboard.IsKeyDown(Key.Space))
            cam.Y -= Time.DeltaTime * spd;
        if (Keyboard.IsKeyDown(Key.LeftShift))
            cam.Y += Time.DeltaTime * spd;
    }
}