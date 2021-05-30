using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
namespace overlay_app
{

    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private Mem pc = new Mem();
        public bool rise { get; set; }
        public bool gu { get; set; }
        public Int64 msc { get; set; }
        public Int64 msc2 { get; set; } // if need another hook
        public bool another { get; set; } // if have another hook

        private Int64 address_reads;

        public struct configs
        {
            public bool overlay         { get; set; }
            public bool progress        { get; set; }
            public bool numeric         { get; set; }
            public bool procent         { get; set; }
            public bool mascot          { get; set; }
            public string font_f        { get; set; }
            public string hexcolor_f    { get; set; }
            public int size_f           { get; set; }
            public string hex_cbbar     { get; set; }
            public string hex_cfbar     { get; set; }
            public string scale_over    { get; set; }
            public int opacy            { get; set; }
            public bool ac_hw           { get; set; }
            public int x                { get; set; }
            public int y                { get; set; }
        };

        configs cfg = new configs();

        public MainWindow()
        {
            InitializeComponent();
            load_config();
            RenderOptions.ProcessRenderMode = (bool)chk_acceleration.IsChecked ? RenderMode.Default : RenderMode.SoftwareOnly;
            foreach (var x in Fonts.SystemFontFamilies)
            {
                cb_font.Items.Add(x.ToString());
            }

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0, 200);
            dispatcherTimer.Start();
        }
        public void save_config()
        {
 
            using (StreamWriter sw = new StreamWriter(@"config.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, cfg);
               
            }
        }

        double x= 0, y = 0;
        List<long> adr_hps = new List<long>();
        //over ad = new over();
        public void load_config()
        {
            if (File.Exists(@"config.json"))
            {
                string json;
                using (StreamReader file = File.OpenText(@"config.json"))
                {
                    json = file.ReadToEnd();
                }
                cfg = JsonConvert.DeserializeObject<configs>(json);

                chk_acceleration.IsChecked = cfg.ac_hw;
                cb_font.Text = cfg.font_f;
                color.Text = cfg.hexcolor_f;
                txtbox_number.Text = cfg.size_f.ToString();
                bar_background.Text = cfg.hex_cbbar;
                bar_foreground.Text = cfg.hex_cfbar;
                scale.Text = cfg.scale_over.ToString();
                slider_opc_background.Value = cfg.opacy;
                chk_overlay.IsChecked = cfg.overlay;
                chk_porcent.IsChecked = cfg.procent;
                chk_bar.IsChecked = cfg.progress;
                chk_num.IsChecked = cfg.numeric;
                chk_mascote.IsChecked = cfg.mascot;
                x = cfg.x;
                y = cfg.y;
            }
        }

        public struct RECT
        {
            public int direita, topo, esquerda, baixo;
        }

        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
             IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
        int dwSize, AllocationType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static unsafe extern bool VirtualFreeEx(
           IntPtr hProcess, byte* pAddress,
           int size, AllocationType freeType);

        private long scan(string aob, bool read, bool write, bool executavel)
        {
            long init = 0x01e74e2f0000;
            long endit = 0x7FFFFFFFFFF;
            foreach (ProcessModule mod in pc.theProc.Modules)
            {
                if (mod.ModuleName.CompareTo("Ryujinx.exe") == 0)
                    endit = (long)mod.BaseAddress;

                if (mod.ModuleName.CompareTo("Ryujinx.dll") == 0)
                    init = (long)mod.BaseAddress;
            }

            Task<IEnumerable<long>> AoBScanResults = pc.AoBScan(init, endit, aob, read, write, executavel);
            IEnumerable<long> respost = AoBScanResults.Result;

            foreach (var rp in respost)
            {
                if (rp > 0)
                {
                    return rp;
                }
            }
            return 0;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private List<over> newover = new List<over>();//
        
        private void chk_overlay_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach(var ov in newover)
                ov.Hide();
            

        }

        private void chk_overlay_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var ov in newover)
            {
                ov.lbl_hp.Visibility = chk_num.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
                ov.pb_hp.Visibility = chk_bar.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
                ov.lbl_porcent.Visibility = chk_porcent.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
                ov.img.Visibility = chk_mascote.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                ov.Show();
            }

        }

        public void restore()
        {
           
            logBlock.Text += ("Restauring....") + Environment.NewLine;
            if (address_reads > 0 || msc > 0)
            {
                newover.Clear();
                adr_hps.Clear();
                pc.Suspend(pc.theProc.Id);
                another = false;
                
                byte[] restore = { 0x0, 0x0 };
                if (rise)
                {
                    // OLD
                    //msc -= 6;
                    //restore = new byte[] { 0x0F, 0x8E, 0xF2, 0x7D, 0x00, 0x00, 0x48, 0xC1, 0xE9, 0x01, 0x48, 0x81, 0xE0, 0xFF, 0x0F, 0x00, 0x00, 0x8B, 0x04, 0x01, 0xC4, 0xE3, 0x39 };

                    // restore = new byte[] { 0x4D, 0x21, 0xD9, 0x42, 0x8B, 0x1C, 0x0B, 0xC4, 0xE3, 0x79, 0x22, 0xDB, 00, 0xC5, 0xEA, 0xC2, 0xC3, 0x07, 0xC5, 0xF9, 0x7E, 0xC3 };
                    another = true;
                    restore = new byte[] { 0x48, 0x21, 0xD0, 0x8B, 0x04, 0x01, 0xC4, 0xE3, 0x79, 0x22, 0xF8, 0x00, 0x48, 0x8B, 0x81, 0x50, 0x5D, 0x97, 0x15, 0xC5, 0xCA, 0xC2, 0xC7, 0x07, 0xC5, 0xF9, 0x7E, 0xC1, 0x85, 0xC9, 0x0F, 0x84, 0x2F, 0x00, 0x00, 0x00, 0x4C, 0x89, 0x8C, 0x24, 0x44, 0x13, 0x00, 0x00 };
                }
                else if (gu)
                {
                    //msc -= 6;
                
                    restore = new byte[] { 0x48, 0x21, 0xF5, 0x41, 0x8B, 0x2C, 0x28, 0xC4, 0xE3, 0x49, 0x22, 0xC5, 00, 0x8D, 0xA9, 0x74, 0x03, 00, 00 };
                    //memory.WriteBytes(msc.ToString("X"), restore);
                }

                pc.WriteBytes(msc.ToString("X"), restore);
                //address_reads -= 0x100;
                if(another)
                {
                    if(rise)
                    {
                        restore = new byte[] { 0x43, 0x89, 0x0C, 0x20, 0x48, 0x8D, 0x4D, 0x10, 0x49, 0xBC, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F, 00, 0x00, 00, 0x4C, 0x21, 0xE1 };
                        pc.WriteBytes(msc2.ToString("X"), restore);
                    }
                }

                VirtualFreeEx(pc.theProc.Handle, (IntPtr)address_reads, 0x500, AllocationType.Commit | AllocationType.Reserve);
                pc.Resume(pc.theProc.Id);
                address_reads = 0;
                msc = 0;
                logBlock.Text += ("....DONE!....") + Environment.NewLine;
            }
            else
            {
                logBlock.Text += ("eRrO bOi I nEeD dO aNyThInG tO ReStOrE") + Environment.NewLine;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var ov in newover)
                ov.Close();

            restore();
            newover.Clear();
            //Close();
        }

        private void slider_opc_background_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color cl = Color.FromArgb((byte)slider_opc_background.Value, 0, 0, 0);
            foreach (var ov in newover)
                ov.Background = new SolidColorBrush(cl);
        }

        private void cb_font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var ov in newover)
            {
                ov.lbl_porcent.FontFamily = new FontFamily(cb_font.Text);
                ov.lbl_hp.FontFamily = new FontFamily(cb_font.Text);
            }

        }

        private void txtbox_number_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var ov in newover)
            {
                ov.lbl_porcent.FontSize = Convert.ToInt32(txtbox_number.Text);
                ov.lbl_hp.FontSize = Convert.ToInt32(txtbox_number.Text);
            }
                
        }

        private void color_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            try
            {
                foreach (var ov in newover)
                {
                    ov.lbl_porcent.Foreground = (Brush)bc.ConvertFrom(color.Text);
                    ov.lbl_hp.Foreground = (Brush)bc.ConvertFrom(color.Text);
                }

            }
            catch { }
        }

        private void bar_background_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            try
            {

                foreach (var ov in newover)
                    ov.pb_hp.Background = (Brush)bc.ConvertFrom(bar_background.Text);


            }
            catch { }
        }

        private void bar_foreground_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            try
            {
                foreach (var ov in newover)
                    ov.pb_hp.Foreground = (Brush)bc.ConvertFrom(bar_foreground.Text);
            }
            catch { }
        }

        private void monsterHP()
        {
            pc.Suspend(pc.theProc.Id);

            logBlock.Text += ("[ANALISE] Process suspend...") + Environment.NewLine;
            var address = VirtualAllocEx(pc.theProc.Handle, (IntPtr)0x0, (IntPtr)0x500, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
            byte[] wm = { 0x0, 0x0 };
            byte[] wm2 = { 0x0, 0x0 };
            int nops = 0, nops2 = 0;

            if (gu)
            {
                // 0x48, 0x21, 0xF5,   0x42, 0x8B, 0x6C, 0x05, 00,
                wm = new byte[] {
                            0x48, 0x21, 0xF5,              	        //- and rbp,rsi
                            0x50,                                   //- push rax
                            0x4A, 0x8D, 0x44, 0x05, 00,             //- lea rax,[rbp + r8 + 00]
                            0x48, 0xA3, 0x00, 0x01, 0x58, 0x02, 0x55, 0x01, 0x00,00, // - mov [15502580100],rax
                            0x58,                                   // push rax
                            0x42, 0x8B, 0x6C, 0x05, 00,             //- mov ebp,[rbp+r8+00]
                            0xC4,0xE3,0x49,0x22,0xC5,00,            //- vpinsrd xmm0,esi,ebp,00
                            0x8D, 0xA9, 0x74,0x03,00,00,            //- lea ebp,[rcx + 00000374]

                            0xFF, 0x25, 0x00,00,00,00,
                            0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,00,00         //- jmp 2305F289EEB
                            };

                long adr = (long)address;
                for (int i =0; i < 8; i++)
                    wm[11+i] = BitConverter.GetBytes((adr + 0x100))[i];


                nops = 5;
            }
            else if (rise)
            {
                // OLD
                //wm = new byte[]  {
                //            0x48, 0xC1, 0xE9, 0x01,                     //- shr rcx,01
                //            0x48, 0x25, 0xFF, 0x0F, 0x00, 0x00,        	//- and rax,00000FFF
                //            0x52,                    			        //- push rdx
                //            0x48, 0x8D, 0x14, 0x08,           		    //- lea rdx,[rax+rcx]
                //            0x48, 0x89, 0x15, 0xEA, 0x00, 0x00, 0x00,   //- mov [mylocal+100],rdx
                //            0x5A,                    			        //- pop rdx
                //            0x8B ,0x04 ,0x01,                           //- mov eax,[rcx+rax]
                //            0xFF, 0x25, 0x00,00,00,00,
                //            0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,00,00         //- jmp 2305F289EEB
                //            };


                wm = new byte[]
                {
                    0x48, 0x21, 0xD0,              			    //- and rax,rdx
                    0x53,                    			        //- push rbx
                    0x41, 0x50,                 			    //- push r8
                    0x41, 0x54,                 			    //- push r12
                    0x4C, 0x8D, 0x25, 0xF1,00,00,00,     	                //- lea r12,[2D10C7A0100] { (0) }                           -> MEMORY ARRAY +100+(8*index)
                    0x48, 0x8D, 0x1C, 0x08,           		                //- lea rbx,[rax+rcx]                                       -> COPY
                                                                                                                                        // INIT
                    0x4C, 0x8B, 0x05, 0x7B,00,00,00,     	                //- mov r8,[2D10C7A0095] { (0) }                            -> SIZE
                    0x43, 0x83, 0x3C, 0x20,  00,       		                //- cmp dword ptr [r8+r12],00 { 0 }                         -> IF ARRAY[INDEX] == 0
                    0x0F ,0x84 ,0x19,0x00,0x00,0x00,         	            //- je 2D10C7A0038                                          -> GOTO WRITE_THEN
                    0x4B, 0x39, 0x1C, 0x20,           		                //- cmp [r8+r12],rbx                                        -> IF ARRAY[INDEX] == COPY
                    0x0F,0x84 ,0x0F,0x00,0x00,0x00,         	            //- je 2D10C7A0038                                          -> GOTO WRITE_THEN
                    0x83, 0x05, 0x5F, 00,00,00, 0x08,     	                //- add dword ptr [2D10C7A0095],08 { (0),8 }                -> SIZE += 8
                    0xFF, 0x05, 0x54,00,00,00,                              //- inc [2D10C7A0090]                                       -> INDEX
                    0xEB ,0xD5,                 			                //- jmp 2D10C7A0013                                         -> INIT
                    0x4B ,0x89 ,0x1C ,0x20,           		                //- mov [r8+r12],rbx                                        -> WRITE IN ARRAY ADDRESS
                    0xC7 ,0x05 ,0x44,0x00,0x00,0x00 ,0x00,0x00,0x00,0x00,   //- mov [2D10C7A0090],00000000 { (0),0 }                    -> SIZE ZERO
                    0xC7 ,0x05 ,0x3F,0x00,0x00,0x00 ,0x00,0x00,0x00,0x00, 	//- mov [2D10C7A0095],00000000 { (0),0 }                    -> ZERO INDEX
                    

                    0x41, 0x5C,                 			                //- pop r12                                                 
                    0x41, 0x58,                 			                //- pop r8
                    0x5B,                    			                    //- pop rbx
                    0x8B, 0x04, 0x08,              			                //- mov eax,[rax+rcx]                                       -> RETURN GAME NORMOL
                    0xC4, 0xE3, 0x79, 0x22, 0xF8,00,          	            //- vpinsrd xmm7,eax,eax,00                                 
                    0x48, 0x8B, 0x81, 0x50, 0x5D,0x97,0x15,                 //- mov rax,[rcx+15975D50]

                    0xFF, 0x25, 0x00,00,00,00,
                    0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,00,00 	    //- jmp to_game
                     /*
                        addr + 100 = ARRAY
                        addr + 90  = INDEX <- HERE NOT NECESSARY BUT IF NOT WANT MAKE HERE IN C# ONLY BECAUSE THAT I DO THAT. SO IF DONT WANT IS LIKE (SIZE / 8) EQ INDEX
                        addr + 95  = SIZE (8 * index)
                 
                     */
                };

                wm2 = new byte[]
                {
                    0x43, 0x89, 0x0C, 0x20,                         //- mov [r8+r12],ecx
                    0x89, 0x0D, 0xD6,0xFF,0xFF,0xFF,        	    //- mov [29DD93401E0],ecx                                           -> MEMORY + 1E0 IF [MEMORY + 1E0] == 1 {CLEAR_ARRAY}
                    0x48, 0x8D, 0x4D, 0x10,           	            //- lea rcx,[rbp+10]
                    0x49, 0xBC, 0xFF,0xFF,0xFF,0xFF,0x7F,00,00,00, 	//- mov r12,0000007FFFFFFFFF
                    0xFF,0x25, 00,00,00,00,
                    01,01,01,01,01,01,00,00 	                    //- jmp 29D2204EAF9
                };
                nops = 5;

                if(another)
                    nops2 = 4;


            }

            logBlock.Text += ("space allocated to the hook " + address.ToString("X")) + Environment.NewLine;

            for (int i = wm.Length - 8; i < wm.Length; i++)
            {
                wm[i] = BitConverter.GetBytes((msc + 14))[i - (wm.Length - 8)];
            }
            logBlock.Text += ("[Shell] creating bytes for the hook...") + Environment.NewLine;
            byte[] jmp = {0xFF, 0x25, 0x00,00,00,00,
                          0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,00,00}; //BitConverter.GetBytes(address.ToInt64())

            for (int i = jmp.Length - 8; i < jmp.Length; i++)
            {
                jmp[i] = BitConverter.GetBytes(address.ToInt64())[i - (jmp.Length - 8)];
            }
            logBlock.Text += ("[Shell] making the detour") + Environment.NewLine;

            address_reads = (long)address;
            pc.WriteBytes(address.ToString("X"), wm);
            logBlock.Text += ("[Write] writing the bytes of the hook ") + Environment.NewLine;

            pc.WriteBytes((msc).ToString("X"), jmp);
            logBlock.Text += ("[Write] writing the function bytes") + Environment.NewLine;

            for (int i = 0; i < nops; i++)
            {
                pc.WriteMemory((msc + jmp.Length + i).ToString("X"), "byte", "0x90");
            }

            if (another)
            {
                for (int i = wm2.Length - 8; i < wm2.Length; i++)
                    wm2[i] = BitConverter.GetBytes((msc2 + 14))[i - (wm2.Length - 8)];
                
                for (int i = jmp.Length - 8; i < jmp.Length; i++)
                    jmp[i] = BitConverter.GetBytes((address+0x200).ToInt64())[i - (jmp.Length - 8)];
                
                pc.WriteBytes((address+0x200).ToString("X"), wm2);
                pc.WriteBytes((msc2).ToString("X"), jmp);

                for (int i = 0; i < nops2; i++)
                 pc.WriteMemory((msc2 + jmp.Length + i).ToString("X"), "byte", "0x90");
              
            }



            pc.Resume(pc.theProc.Id);
            logBlock.Text += ("....DONE!....");
        }

        private void scannermemory()
        {
            logBlock.Text += ("SCANNNNNNNNN STARTING WAIT......") + Environment.NewLine;

            if (gu)
            {
                msc = scan("48 21 F5 41 8B 2C 28 C4 E3 49 22 C5 00 8D A9 74 03 00 00", false, false, true);
                logBlock.Text += ("Found: " + msc.ToString("X")) + Environment.NewLine;
            }
            else if (rise)
            {
                another = true;
                //msc = scan("4D 21 D9 42 8B 1C 0B C4 E3 79 22 DB", false, false, true);
                //if (msc == 0)
                msc = scan("48 21 D0 8B 04 01 C4 E3 79 22 F8 00 48 8B 81 50 5D 97 15 C5 CA C2 C7 07 C5 F9 7E C1 85 C9 0F 84 2F 00 00 00 4C 89 8C 24 44 13 00 00", false, false, true);
                logBlock.Text += ("Found: " + msc.ToString("X")) + Environment.NewLine;
                if (another)
                {
                    msc2 = scan("43 89 0C 20 48 8D 4D 10 49 BC FF FF FF FF 7F 00 00 00 4C 21 E1", false, false, true);
                    logBlock.Text += ("Found: " + msc2.ToString("X")) + Environment.NewLine;
                }
                    
                //msc += 6;

            }

            if (msc != 0 && msc > 0x1000)
                monsterHP();
            else
                pc.theProc = null;
           
        }

        private RECT window_rect;
        int index_array = 0;
        int clear = 0;
        BitmapImage img,game;
        Brush br;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(newover.Count > 0)
            {
                x = window_rect.topo + newover[0].Height - newover[0].Top;
                y = window_rect.esquerda - newover[0].Width - newover[0].Left;
            }
             

            if (pc.pHandle == null || pc.theProc == null)
            {
                var pcs = Process.GetProcessesByName("Ryujinx");
                if (pcs.Length > 0 && pcs[0].MainWindowTitle.Contains("MONSTER HUNTER"))
                {
                    pc.OpenProcess(pcs[0].Id);
                    if (!pc.theProc.HasExited)
                    {
                        logBlock.Text += pcs[0].MainWindowTitle + Environment.NewLine;
                        if (pc.theProc.MainWindowTitle.Contains("RISE"))
                        {
                            rise = true;
                            gu = false;

                            foreach (var ov in newover)
                            {
                                ov.img.Source = null;
                                ov.pb_hp.OpacityMask = null;
                            }

                             img = new BitmapImage(new Uri("pack://application:,,,/recourses/progressbar.png"));
                             br = new ImageBrush(img);
                             game = new BitmapImage(new Uri("pack://application:,,,/recourses/kamura.png"));
                            foreach (var ov in newover)
                            {
                                ov.pb_hp.OpacityMask = br;
                                ov.img.Source = new BitmapImage(new Uri("pack://application:,,,/recourses/kamura.png"));
                            }
        
                            this.Title = "Overlay RISE";
                        }
                        else if (pc.theProc.MainWindowTitle.Contains("GENERATIONS ULTIMATE"))
                        {
                            gu = true;
                            rise = false;
                            foreach (var ov in newover)
                            {
                                ov.img.Source = null;
                                ov.pb_hp.OpacityMask = null;
                            }

                             img = new BitmapImage(new Uri("pack://application:,,,/recourses/progressbarGU.png"));
                             br = new ImageBrush(img);
                             game = new BitmapImage(new Uri("pack://application:,,,/recourses/GU.png")); 
                            foreach (var ov in newover)
                            {
                                ov.pb_hp.OpacityMask = br;
                                ov.img.Source = new BitmapImage(new Uri("pack://application:,,,/recourses/GU.png"));
                            }



                            this.Title = "Overlay GU";
                        }

                        if (gu || rise)
                        {
                            scannermemory();
                        }
                    }
                }
            }

            float current_hp = 1, max_hp = 1;

            if (address_reads > 0x100 && msc > 0x1000)
            {
                ++index_array;
                if (gu)
                {
                    index_array = 0;
                    clear = 0;
                }
                else
                    clear = pc.ReadInt((address_reads + 0x1E0).ToString("X"));
                

                long max_life = pc.ReadLong((address_reads + 0x100 + (0x8 * index_array)).ToString("X"));
                
                
                if (max_life > 0)
                {
                    bool l_hp_fund = false;
                    foreach(var x in adr_hps)
                       if (x == max_life) l_hp_fund = true;
                    if (!l_hp_fund)
                    {
                        if(gu)
                        {
                            adr_hps.Clear();
                            foreach(var ov in newover)
                            {
                                ov.Close();
                            }
                            newover.Clear();
                        }

                        adr_hps.Add(max_life);
                        newover.Add(new over());
                        newover[newover.Count - 1].pb_hp.OpacityMask = br;
                        newover[newover.Count - 1].img.Source = game;
                        if (chk_overlay.IsChecked.Value)
                        {
                            newover[newover.Count - 1].Show();
                        }
                    }
                    
                    for(int i = 0; i < newover.Count; i++)
                    {
                        max_life = adr_hps[i];

                        if (gu)
                        {
                            current_hp = pc.ReadInt(max_life.ToString("X"));
                            max_hp = pc.ReadInt((max_life + 4).ToString("X"));
                        }
                        else if (rise)
                        {

                            current_hp = pc.ReadFloat(max_life.ToString("X"));
                            max_hp = pc.ReadFloat((max_life + 4).ToString("X"));
                        }


                        float porcent = 0;
                        if (current_hp != 0.0f && max_hp > 0.0f)
                        {
                            porcent = (current_hp / max_hp) * 100;
                        }else if (rise)
                        {
                            adr_hps.RemoveAt(i);
                            //newover[i].Hide();
                            newover[i].Close();
                            newover.RemoveAt(i);
                            break;
                        }

                        newover[i].lbl_hp.Content = current_hp + "/" + max_hp;
                        newover[i].lbl_porcent.Content = porcent + "%";
                        newover[i].pb_hp.Maximum = max_hp;
                        newover[i].pb_hp.Value = current_hp;

                        GetWindowRect(pc.theProc.MainWindowHandle, out window_rect);
                        if (newover.Count > 0)
                        {
                            if(i > 0)
                            {
                                newover[i].Top = window_rect.topo + newover[i].Height + (newover[0].Height * i )- x;
                                newover[i].Left = window_rect.esquerda - newover[i].Width - y;
                            }
                            else
                            {
                                newover[i].Top = window_rect.topo + newover[i].Height - x;
                                newover[i].Left = window_rect.esquerda - newover[i].Width - y;
                            }

                        }


                    }
                    if (clear == 1)
                    {
                        for (var x = 0; x < adr_hps.Count; ++x)
                        {
                            //newover[x].Hide();
                            newover[x].Close();
                            byte[] bclear = new byte[] {0,0,0,0,0,0,0,0};
                            pc.WriteBytes((address_reads + 0x100 + (0x8 * x)).ToString("X"), bclear);
                        }
                        adr_hps.Clear();
                        newover.Clear();
                    }
                }
                if (index_array > 5) index_array = 0;
            }
            posX.Text = x.ToString();
            posY.Text = y.ToString();


            cfg.ac_hw = (bool)chk_acceleration.IsChecked;
            cfg.font_f = cb_font.Text;
            cfg.hexcolor_f = color.Text;
            cfg.size_f = Convert.ToInt32(txtbox_number.Text);
            cfg.hex_cbbar = bar_background.Text;
            cfg.hex_cfbar = bar_foreground.Text;
            cfg.scale_over = scale.Text;
            cfg.opacy = Convert.ToInt32(slider_opc_background.Value);
            cfg.overlay = (bool)chk_overlay.IsChecked;
            cfg.procent = (bool)chk_porcent.IsChecked;
            cfg.progress = (bool)chk_bar.IsChecked;
            cfg.numeric = (bool)chk_num.IsChecked;
            cfg.mascot = (bool)chk_mascote.IsChecked;
            cfg.x = (int)x;
            cfg.y = (int)y;

        }

        private void scale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var ov in newover)
            {
                ov.Height = 80 * (scale.SelectedIndex + 1);
                ov.Width = 250 * (scale.SelectedIndex + 1);
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            save_config();
            logBlock.Clear();
            logBlock.AppendText("SAVED!");
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {

            for(int i = 0; i < newover.Count; ++i)
            {
                newover[i].Hide();
                newover[i].Close();
            }
          
            newover.Clear();
        }

        private void chk_acceleration_Click(object sender, RoutedEventArgs e)
        {
            bool x = (bool)chk_acceleration.IsChecked;
            RenderOptions.ProcessRenderMode = x ? RenderMode.Default : RenderMode.SoftwareOnly;
            logBlock.Text += RenderOptions.ProcessRenderMode;
        }
    }
}