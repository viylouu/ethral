partial class gateify {
    static void salImgui() {
        ImGui.Begin("save and load");

        if (ImGui.Button("update saves list")) {
            savefiles = new string[Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\", "*.json").Length];

            for (int i = 0; i < savefiles.Length; i++)
                savefiles[i] = Path.GetFileNameWithoutExtension(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\", "*.json")[i]);
        }

        if(savefiles.Length > 0)
            ImGui.ListBox("saves", ref imguisfsel, savefiles, savefiles.Length);
        else
            ImGui.Text("you have no saves");

        if (ImGui.Button("load")) {
            string filedata;

            using (StreamReader sr = new StreamReader(@"assets\savedata\gateify\"+savefiles[imguisfsel]+".json"))
                filedata = sr.ReadToEnd();

            gates = JsonConvert.DeserializeObject<List<node>>(filedata);
        }

        if (ImGui.Button("load as schematic")) {
            string filedata;

            using (StreamReader sr = new StreamReader(@"assets\savedata\gateify\" + savefiles[imguisfsel] + ".json"))
                filedata = sr.ReadToEnd();

            List<node> gatesadd = JsonConvert.DeserializeObject<List<node>>(filedata);

            int selstart = gates.Count;

            selects = new List<int>();

            for (int i = 0; i < gatesadd.Count; i++) {
                node gateadd = gatesadd[i];

                if(gateadd.out1 != -1)
                    gateadd.out1 += selstart;
                if (gateadd.out2 != -1)
                    gateadd.out2 += selstart;
                if (gateadd.in1 != -1)
                    gateadd.in1 += selstart;
                if (gateadd.in2 != -1)
                    gateadd.in2 += selstart;

                gates.Add(gateadd);

                selects.Add(i+selstart);
            }
        }

        ImGui.InputText("save name", ref savename, 100);

        if (ImGui.Button("save")) {
            string data = JsonConvert.SerializeObject(gates);

            using (StreamWriter sw = new StreamWriter(@"assets\savedata\gateify\"+savename+".json"))
                sw.Write(data);
        }

        if (ImGui.Button("clear gates"))
            gates = new List<node>();

        ImGui.End();
    }
}