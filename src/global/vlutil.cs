public class m { //math
    public static Random r = new Random(Guid.NewGuid().GetHashCode());

    //random
    public static float rand() => (float)r.NextDouble();
    public static int rand(int min, int max) => r.Next(min, max);
    public static float rand(float min, float max) => ((float)r.NextDouble() + min) * (max - min);

    //values
    public static float pi = 3.14159265358979323846264338327950288420f;
    public static float e = 2.718281828459045235360287471352662497757f;
    public static float tau = 6.2831853071795864769252867665590057683943f;

    //misc values
    public static float v180dpi = 57.295779513082320876798154814105170417f;
    public static float vpid180 = 0.0174532925199432957692369076848861271344f;

    //radians and degrees
    public static float r2d(float a) => v180dpi * a;
    public static float d2r(float a) => vpid180 * a;

    //trig
    public static float sin(float a) => MathF.Sin(a);
    public static float cos(float a) => MathF.Cos(a);
    public static float tan(float a) => MathF.Tan(a);
    public static float cot(float a) => 1 / MathF.Tan(a);
    public static float sec(float a) => 1 / MathF.Cos(a);
    public static float csc(float a) => 1 / MathF.Sin(a);
    public static float atan(float a) => MathF.Atan(a);
    public static float atan2(float y, float x) => MathF.Atan2(y, x);
    public static float acot(float a) => MathF.Atan(1 / a);
    public static float asec(float a) => MathF.Acos(1 / a);
    public static float acsc(float a) => MathF.Asin(1 / a);
    public static float asin(float a) => MathF.Asin(a);
    public static float acos(float a) => MathF.Acos(a);
    public static float tanh(float a) => MathF.Tanh(a);
    public static float sinh(float a) => MathF.Sinh(a);
    public static float cosh(float a) => MathF.Cosh(a);
    public static float coth(float a) => MathF.Cosh(a)/MathF.Sinh(a);
    public static float sech(float a) => 1 / MathF.Cosh(a);
    public static float csch(float a) => 1 / MathF.Sinh(a);
    public static float atanh(float a) => MathF.Atanh(a);
    public static float asinh(float a) => MathF.Asinh(a);
    public static float acosh(float a) => MathF.Acosh(a);
    public static float acoth(float a) => MathF.Atanh(1 / a);
    public static float asech(float a) => MathF.Acosh(1 / a);
    public static float acsch(float a) => MathF.Asinh(1 / a);

    //misc
    public static float abs(float a) => MathF.Abs(a);
    public static float flr(float a) => MathF.Floor(a);
    public static float ceil(float a) => MathF.Ceiling(a);
    public static float rnd(float a) => MathF.Round(a);
    public static float log(float a) => MathF.Log(a);
    public static float clmp(float n, float min, float max) => (float)Math.Clamp(n, min, max);

    //exponents and roots
    public static float pow(float n, float p) => MathF.Pow(n, p);
    public static float sqr(float a) => a * a;
    public static float cbe(float a) => a * a * a;
    public static float qrt(float a) => a * a * a * a;
    public static float qin(float a) => a * a * a * a * a;
    public static float root(float n, float r) => pow(n, 1/r);
    public static float sqrt(float a) => MathF.Sqrt(a);
    public static float cbrt(float a) => MathF.Cbrt(a);
    public static float qrtt(float a) => MathF.Pow(a, 0.25f);
    public static float qint(float a) => MathF.Pow(a, 0.2f);

    //geometry
    public static float dist(float a, float b) => abs(a - b);
    public static float dist(Vector2 a, Vector2 b) => sqrt(sqr(b.X - a.X) + sqr(b.Y - a.Y));
    public static float dirb(Vector2 a, Vector2 b) => atan2(b.Y - a.Y, b.X - a.X);

    public static Vector3 eul2vec(float pitch, float yaw) => new Vector3(cos(d2r(pitch)) * cos(d2r(yaw)), cos(d2r(pitch)) * sin(d2r(yaw)), sin(d2r(pitch)));

    public static float min(float a, float b) => MathF.Min(a, b);
    public static float max(float a, float b) => MathF.Max(a, b);

    public static int min(int a, int b) => (int)MathF.Min(a, b);
    public static int max(int a, int b) => (int)MathF.Max(a, b);
}

public class e { //easings
    public static float dist(float cur, float targ, float smth) => cur + (targ - cur) / (smth / (Time.DeltaTime * 60));
    public static Vector2 dist(Vector2 cur, Vector2 targ, float smth) => cur + (targ - cur) / (smth / (Time.DeltaTime * 60));

    static float 
        c1 = 1.70158f,
        c2 = 2.70158f,
        c3 = 2.5949095f,
        c4 = 3.5949095f,
        c5 = 2.09439510239319542564176225551966858947f,
        c6 = 1.39626340159546357643464573390622350408f;

    public static float isin(float x) => 1 - m.cos(x * m.pi / 2);
    public static float osin(float x) => m.sin(x * m.pi / 2);
    public static float iosin(float x) => -(m.cos(m.pi * x) - 1) / 2;

    public static float isqr(float x) => m.sqr(x);
    public static float osqr(float x) => 1 - m.sqr(1 - x);
    public static float iosqr(float x) => x < .5f ? 2 * m.sqr(x) : 1 - m.sqr(-2 * x + 2) / 2;

    public static float icbe(float x) => m.cbe(x);
    public static float ocbe(float x) => 1 - m.cbe(1 - x);
    public static float iocbe(float x) => x < .5f ? 4 * m.cbe(x) : 1 - m.cbe(-2 * x + 2) / 2;

    public static float iqrt(float x) => m.qrt(x);
    public static float oqrt(float x) => 1 - m.qrt(1 - x);
    public static float ioqrt(float x) => x < .5f ? 8 * m.qrt(x) : 1 - m.qrt(-2 * x + 2) / 2;

    public static float iqin(float x) => m.qin(x);
    public static float oqin(float x) => 1 - m.qin(1 - x);
    public static float ioqin(float x) => x < .5f ? 8 * m.qin(x) : 1 - m.qin(-2 * x + 2) / 2;

    public static float iexp(float x) => x == 0 ? 0 : m.pow(2, 10 * x - 10);
    public static float oexp(float x) => x == 1 ? 1 : 1 - m.pow(2, -10 * x);
    public static float ioexp(float x) => x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? m.pow(2, 20 * x - 10) / 2 : (2 - m.pow(2, -20 * x + 10)) / 2;

    public static float icir(float x) => 1 - m.sqrt(1 - m.sqr(x));
    public static float ocir(float x) => m.sqrt(1-m.sqr(x-1));
    public static float iocir(float x) => x < 0.5 ? (1 - m.sqrt(1 - m.sqr(2 * x))) / 2 : (m.sqrt(1 - m.sqr(-2 * x + 2)) + 1) / 2;

    public static float iback(float x) => c2 * m.cbe(x) - c1 * m.sqr(x);
    public static float oback(float x) => 1 + c2 * m.cbe(x - 1) + c1 * m.sqr(x - 1);
    public static float ioback(float x) => x < 0.5f ? (m.sqr(2 * x) * (c4 * 2 * x - c3)) / 2 : (m.sqr(2 * x - 2) * (c4 * (x * 2 - 2) + c3) + 2) / 2;

    public static float ielas(float x) => x == 0 ? 0 : x == 1 ? 1 : -m.pow(2, 10 * x - 10) * m.sin((x * 10 - 10.75f) * c5);
    public static float oelas(float x) => x == 0 ? 0 : x == 1 ? 1 : m.pow(2, -10 * x) * m.sin((x * 10 - .75f) * c5) + 1;
    public static float ioelas(float x) => x == 0 ? 0 : x == 1 ? 1 : x < .5f ? -(m.pow(2, 20 * x - 10) * m.sin((20 * x - 11.125f) * c6)) / 2 : m.pow(2, -20 * x + 10) * m.sin((20 * x - 11.125f) * c6) / 2 + 1;

    public static float iboun(float x) => 1 - oboun(1 - x);
    public static float oboun(float x) {
        if (x < 1 / 2.75f)
            return 7.5625f * x * x;
        else if (x < 2 / 2.75f)
            return 7.5625f * (x -= 1.5f / 2.75f) * x + 0.75f;
        else if (x < 2.5 / 2.75)
            return 7.5625f * (x -= 2.25f / 2.75f) * x + 0.9375f;
        else
            return 7.5625f * (x -= 2.625f / 2.75f) * x + 0.984375f;
    }
    public static float ioboun(float x) => x < .5f ? (1 - oboun(1 - 2 * x)) / 2 : (1 + oboun(2 * x - 1)) / 2;
}

public class u { 
    public static void fixtransparenttex(ITexture texture) {
        for (int i = 0; i < texture.Pixels.Length; i++) {
            var col = texture.Pixels[i].ToColorF();
            texture.Pixels[i] = (col with {
                R = col.R * col.A,
                G = col.G * col.A,
                B = col.B * col.A,
            }).ToColor();
        }
        texture.ApplyChanges();
    }
}