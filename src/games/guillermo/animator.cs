partial class guillermo {
    class animent {
        public anim[] anims;

        public int ID;

        public int frame;

        public ITexture tex;

        public float halfframe = 0;

        public void loaddata(string name) {
            anims = parseanimfile(name + ".anim");
            tex = Graphics.LoadTexture(name + ".png");
        }

        public void setanim(string anim) {
            for (int i = 0; i < anims.Length; i++)
                if (anims[i].name == anim)
                { ID = i; frame = 0; break; }
        }

        public Rectangle getcurframesrc() {
            halfframe+=Time.DeltaTime/anims[ID].param.fps;

            if (halfframe >= 1) {
                halfframe = 0;
                frame++;
            }

            if (frame >= anims[ID].tokens.Length)
                frame = 0;

            while (anims[ID].tokens[frame].type == animtype.sound) {
                //TODO: play sound here

                frame++;

                if (frame >= anims[ID].tokens.Length)
                    frame = 0;

                halfframe = 0;
            }

            return new Rectangle(anims[ID].tokens[frame].x, anims[ID].tokens[frame].y, anims[ID].param.size, anims[ID].param.size);
        }
    }

    class anim {
        public string name;
        public animtoken[] tokens;
        public animparam param;
    }

    class animparam {
        public int fps;
        public int size;
    }

    class animtoken {
        public animtype type;
        public int repeat = 1;
        public int x,y;
    }

    enum animtype {
        def,
        sound,
        unset
    }

    static anim[] parseanimfile(string file) {
        string filedata;

        using (StreamReader sr = new StreamReader(file))
            filedata = sr.ReadToEnd();

        List<anim> anims = new List<anim>();

        int i = -1;
        string curtext = "";
        bool inanim = false;
        int x=0,y=0;
        animtype curtype = animtype.unset;
        List<animtoken> tokens = new List<animtoken>();
        animparam param = new animparam();
        int curanim = -1;
        int num = 0;
        bool posses = false;
        string pos = "";
        bool getfps = false;
        string fps = "";
        bool getsize = false;
        string size = "";

        while (i < filedata.Length) {
            i++;

            if (curtext == "{" && !inanim)
            { inanim = true; anims.Add(new anim { name = curtext.TrimEnd(), param = param, tokens = tokens.ToArray() }); curtext = ""; curanim++; }

            if (curtext == "{" && inanim)
            { Console.WriteLine("err while parsing animfile: \"{\" character while in an anim"); return null; }

            if (curtext == "}" && inanim) { 
                anims[curanim].tokens = tokens.ToArray(); 
                anims[curanim].param = param;
                inanim = false; 
                curtext = ""; 
                tokens.Clear(); 
                param = new animparam();
            }

            if (curtext == "}" && !inanim)
            { Console.WriteLine("err while parsing animfile: tried to exit nonexistant anim"); return null; }

            if (inanim && curtext == "fps:")
            { curtext = ""; getfps = true; continue; }

            if (inanim && curtext == "size:")
            { curtext = ""; getsize = true; continue; }

            if (curtype == animtype.def && (curtext == ")\n" || curtext == ") " || curtext == ")   ") && curtext != ") x" && inanim)
            { tokens.Add(new animtoken { type = animtype.def, x = x, y = y }); curtype = animtype.unset; posses = false; }

            if (posses) {
                if (curtext == ",") {
                    switch (num) {
                        case 0:
                            x = Convert.ToInt32(pos);
                            break;
                        case 1:
                            y = Convert.ToInt32(pos);
                            break;
                    }

                    num++; 
                }
                else if (char.IsDigit(curtext[i])) {
                    pos += curtext;
                    curtext = "";
                }
            }

            if (getfps) {
                if (!char.IsDigit(curtext[i])) {
                    param.fps = Convert.ToInt32(fps);
                    getfps = false;
                }
                else {
                    fps += curtext;
                    curtext = "";
                }
            }

            if (getsize) {
                if (!char.IsDigit(curtext[i])) {
                    param.size = Convert.ToInt32(size);
                    getfps = false;
                }
                else {
                    size += curtext;
                    curtext = "";
                }
            }

            curtext += filedata[i];
        }

        return anims.ToArray();
    }
}