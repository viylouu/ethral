partial class waves {
    public static void update() {
        updcount -= Time.DeltaTime;

        if (updcount <= 0) {
            updcount = updfps;

            float[] prevys = new float[ys.Length];
            float[] prevvs = new float[ys.Length];

            for (int i = 0; i < ys.Length; i++) {
                prevvs[i] = vs[i];
                prevys[i] = ys[i];
            }

            for (int i = 0; i < ys.Length; i++) {
                vs[i] += (540 / 2 - ys[i]) * .35f;

                if(i!=0)
                    vs[i-1] += vs[i]/9;
                if(i!=ys.Length-1)
                    vs[i+1] += vs[i]/9;

                vs[i] *= .99f;

                ys[i] += vs[i];
            }
        }

        if (Keyboard.IsKeyPressed(Key.Space))
            vs[960 / 16] = 64;
    }
}