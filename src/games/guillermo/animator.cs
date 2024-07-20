partial class guillermo {
    class animent {
        public anim[] anims;

        public int ID;

        public int frame;

        public ITexture tex;

        public float halfframe = 0;

        public void loaddata(string name) {
            anims = parseanimfile(name + ".json");
            tex = Graphics.LoadTexture(name + ".png");
        }

        public void setanim(string anim) {
            for (int i = 0; i < anims.Length; i++)
                if (anims[i].name == anim)
                { ID = i; frame = 0; break; }
        }

        public Rectangle getcurframesrc() {
            halfframe+=Time.DeltaTime;

            if (halfframe >= 1f/anims[ID].param.fps) {
                halfframe = 0;
                frame++;
            }

            if (frame >= anims[ID].tokens.Length)
                frame = 0;

            while (anims[ID].tokens[frame].type) {
                //TODO: play sound here

                frame++;

                if (frame >= anims[ID].tokens.Length)
                    frame = 0;

                halfframe = 0;
            }

            return new Rectangle(anims[ID].tokens[frame].x*anims[ID].param.size, anims[ID].tokens[frame].y*anims[ID].param.size, anims[ID].param.size, anims[ID].param.size);
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
        public int x,y;
        public string sound;
        public bool type;
    }

    static anim[] parseanimfile(string file) {
        string filedata;

        using (StreamReader sr = new StreamReader(file))
            filedata = sr.ReadToEnd();

        return JsonConvert.DeserializeObject<anim[]>(filedata);
    }
}