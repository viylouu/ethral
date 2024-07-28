partial class basilisk {
    public class ctex {
        Color[,] tex;

        public ctex() {
            tex = new Color[chunkSize, chunkSize];
        }

        public ctex(ITexture tex2) {
            tex = ConvertSpanTo2DArray(tex2.Pixels, tex2.Width, tex2.Height);
        }

        public ITexture t() {
            ITexture t2 = Graphics.CreateTexture(tex.GetLength(0), tex.GetLength(1));

            for (int x = 0; x < tex.GetLength(0); x++)
                for (int y = 0; y < tex.GetLength(1); y++)
                    t2.SetPixel(x, y, tex[x,y]);

            t2.ApplyChanges();

            return t2;
        }

        T[,] ConvertSpanTo2DArray<T>(Span<T> span, int rows, int cols) {
            if (span.Length != rows * cols)
                throw new ArgumentException("The length of the span does not match the specified dimensions");

            T[,] result = new T[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = span[i * cols + j];

            return result;
        }
    }
}