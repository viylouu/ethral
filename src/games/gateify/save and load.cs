partial class gateify {
    static void salImgui() {
        ImGui.Begin("save and load");

        if (ImGui.Button("update saves list")) {
            savefiles = new string[Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\", "*.json").Length];

            for (int i = 0; i < savefiles.Length; i++)
                savefiles[i] = Path.GetFileNameWithoutExtension(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\assets\savedata\gateify\", "*.json")[i]);
        }

        ImGui.ListBox("saves", ref imguisfsel, savefiles, savefiles.Length);

        if (ImGui.Button("load")) {
            string filedata;

            using (StreamReader sr = new StreamReader(@$"assets\savedata\gateify\{savefiles[imguisfsel]}.json"))
                filedata = sr.ReadToEnd();

            gates = JsonConvert.DeserializeObject<List<node>>(filedata);
        }

        ImGui.NewLine();

        ImGui.InputText("save name", ref savename, 100);

        if (ImGui.Button("save")) {
            string data = JsonConvert.SerializeObject(gates);

            using (StreamWriter sw = new StreamWriter(@$"assets\savedata\gateify\{savename}.json"))
                sw.Write(data);
        }

        ImGui.End();
    }
}