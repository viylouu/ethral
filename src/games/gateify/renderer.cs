partial class gateify {
    public static void rend(ICanvas c) {
        c.Clear(new Color(24, 24, 24));
        c.Antialias(false);
        
        c.Fill(Color.Black);
        float iadd = 24 / zoom;
        for (float i = -iadd; i < 1280; i+= iadd)
            c.DrawLine(i-cam.X%iadd,-24-cam.Y%iadd,i-cam.X%iadd,744-cam.Y%iadd);
        for (float i = -iadd; i < 720; i+=iadd)
            c.DrawLine(-24-cam.X%iadd,i-cam.Y%iadd,1304-cam.X%iadd,i-cam.Y%iadd);

        for(int i = 0; i < gates.Count; i++) {
            float ssX = tossX(gates[i].pos.X),
                  ssY = tossY(gates[i].pos.Y),
                  ssS = 16/zoom;

            c.DrawTexture(
                gatespr, 
                new Rectangle(
                    (m.clmp(gates[i].gate,0,8)*2+(!gates[i].on?1:0))%5*16, 
                    m.flr((m.clmp(gates[i].gate,0,8)*2+(!gates[i].on?1:0))/5f)*16, 
                    16, 16
                ), 
                new Rectangle(ssX, ssY, ssS, ssS, Alignment.Center)
            );

            if(Mouse.Position.X > ssX-ssS/2 && Mouse.Position.X < ssX+ssS/2 && Mouse.Position.Y > ssY-ssS/2 && Mouse.Position.Y < ssY+ssS/2) {
                if (Mouse.IsButtonDown(MouseButton.Left))
                    gates[i].dragged = true;
            }

            if (!Mouse.IsButtonDown(MouseButton.Left))
                gates[i].dragged = false;

            if (gates[i].dragged)
                gates[i].pos = new Vector2(m.rnd((Mouse.Position.X*zoom-cam.X) / 24), m.rnd((Mouse.Position.Y*zoom-cam.Y) / 24));
        }

        c.Fill(Color.White);
        c.DrawCircle(tossX(placeX), tossY(placeY), 4/zoom);
    }
}