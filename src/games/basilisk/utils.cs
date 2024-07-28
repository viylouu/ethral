partial class basilisk {
    static Color tocol(col col) => new Color(col.r, col.g, col.b);
    static col tocol(Color col) => new col(col.R, col.G, col.B);

    static int to1D(float x, float y) => (int)y * chunkSize + (int)x;
}