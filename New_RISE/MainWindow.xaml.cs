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
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0, 500);
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

        private over newover = new over();

        private void chk_overlay_Unchecked(object sender, RoutedEventArgs e)
        {
            newover.Hide();
        }

        private void chk_overlay_Checked(object sender, RoutedEventArgs e)
        {
            bool x = (bool)chk_num.IsChecked;
            newover.lbl_hp.Visibility = x ? Visibility.Visible : Visibility.Hidden;

            x = (bool)chk_bar.IsChecked;
            newover.pb_hp.Visibility = x ? Visibility.Visible : Visibility.Hidden;

            x = (bool)chk_porcent.IsChecked;
            newover.lbl_porcent.Visibility = x ? Visibility.Visible : Visibility.Hidden;

            x = (bool)chk_mascote.IsChecked;

            newover.img.Visibility = x ? Visibility.Visible : Visibility.Collapsed;

            newover.Show();
        }

        public void restore()
        {
            logBlock.Text += ("Restauring....") + Environment.NewLine;
            if (address_reads > 0 || msc > 0)
            {
                pc.Suspend(pc.theProc.Id);
                byte[] restore = { 0x0, 0x0 };
                if (rise)
                {
                    msc -= 6;
                    restore = new byte[] { 0x0F, 0x8E, 0xF2, 0x7D, 0x00, 0x00, 0x48, 0xC1, 0xE9, 0x01, 0x48, 0x81, 0xE0, 0xFF, 0x0F, 0x00, 0x00, 0x8B, 0x04, 0x01, 0xC4, 0xE3, 0x39 };
                }
                else if (gu)
                {
                    //msc -= 6;
                    restore = new byte[] { 0x48, 0xC1, 0xE9, 0x01, 0x81, 0xE0, 0xFF, 0x0F, 0x00, 0x00, 0x89, 0xC0, 0x8B, 0x04, 0x01, 0xC4, 0xE3, 0x49, 0x22, 0xC0, 0x00, 0xC5, 0xFA, 0x7F, 0xC6, 0x8D, 0x85, 0x74 };
                    //memory.WriteBytes(msc.ToString("X"), restore);
                }

                pc.WriteBytes(msc.ToString("X"), restore);
                address_reads -= 0x100;
                VirtualFreeEx(pc.theProc.Handle, (IntPtr)address_reads, 0x200, AllocationType.Commit | AllocationType.Reserve);
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
            restore();
            newover.Close();
        }

        private void slider_opc_background_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color cl = Color.FromArgb((byte)slider_opc_background.Value, 0, 0, 0);
            newover.Background = new SolidColorBrush(cl);

        }

        private void cb_font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newover.lbl_porcent.FontFamily = new FontFamily(cb_font.Text);
            newover.lbl_hp.FontFamily = new FontFamily(cb_font.Text);
        }

        private void txtbox_number_TextChanged(object sender, TextChangedEventArgs e)
        {
            newover.lbl_porcent.FontSize = Convert.ToInt32(txtbox_number.Text);
            newover.lbl_hp.FontSize = Convert.ToInt32(txtbox_number.Text);
        }

        private void color_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            try
            {
                newover.lbl_porcent.Foreground = (Brush)bc.ConvertFrom(color.Text);
                newover.lbl_hp.Foreground = (Brush)bc.ConvertFrom(color.Text);
            }
            catch { }
        }

        private void bar_background_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            try
            {
                newover.pb_hp.Background = (Brush)bc.ConvertFrom(bar_background.Text);
            }
            catch { }
        }

        private void bar_foreground_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            try
            {
                newover.pb_hp.Foreground = (Brush)bc.ConvertFrom(bar_foreground.Text);
            }
            catch { }
        }

        private void monsterHP()
        {
            pc.Suspend(pc.theProc.Id);

            logBlock.Text += ("[ANALISE] Process suspend...") + Environment.NewLine;
            var address = VirtualAllocEx(pc.theProc.Handle, (IntPtr)0x0, (IntPtr)0x200, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
            byte[] wm = { 0x0, 0x0 };
            int nops = 0;
            if (gu)
            {
                wm = new byte[] {
                            0x48, 0xC1, 0xE9, 0x01,                     //- shr rcx,01
                            0x25, 0xFF, 0x0F, 0x00, 0x00,        	    //- and eax,00000FFF
                            0x52,                    			        //- push rdx
                            0x48, 0x8D, 0x14, 0x08,           		    //- lea rdx,[rax+rcx]
                            0x48, 0x89, 0x15, 0xEB, 0x00, 0x00, 0x00,   //- mov [mylocal+100],rdx
                            0x5A,                    			        //- pop rdx
                            0x89, 0xC0,                                 //- mov eax,eax
                            0x8B, 0x04, 0x01,                           //- mov eax,[rcx+rax]
                            0xFF, 0x25, 0x00,00,00,00,
                            0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,00,00         //- jmp 2305F289EEB
                            };
                nops = 1;
            }
            else if (rise)
            {
                wm = new byte[]  {
                            0x48, 0xC1, 0xE9, 0x01,                     //- shr rcx,01
                            0x48, 0x25, 0xFF, 0x0F, 0x00, 0x00,        	//- and rax,00000FFF
                            0x52,                    			        //- push rdx
                            0x48, 0x8D, 0x14, 0x08,           		    //- lea rdx,[rax+rcx]
                            0x48, 0x89, 0x15, 0xEA, 0x00, 0x00, 0x00,   //- mov [mylocal+100],rdx
                            0x5A,                    			        //- pop rdx
                            0x8B ,0x04 ,0x01,                           //- mov eax,[rcx+rax]
                            0xFF, 0x25, 0x00,00,00,00,
                            0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,00,00         //- jmp 2305F289EEB
                            };
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

            address_reads = (long)address + 0x100;
            pc.WriteBytes(address.ToString("X"), wm);
            logBlock.Text += ("[Write] writing the bytes of the hook ") + Environment.NewLine;

            pc.WriteBytes((msc).ToString("X"), jmp);
            logBlock.Text += ("[Write] writing the function bytes") + Environment.NewLine;

            for (int i = 0; i < nops; i++)
            {
                pc.WriteMemory((msc + jmp.Length + i).ToString("X"), "byte", "0x90");
            }

            pc.Resume(pc.theProc.Id);
            logBlock.Text += ("....DONE!....");
        }

        private void scannermemory()
        {
            logBlock.Text += ("SCANNNNNNNNN STARTING WAIT......") + Environment.NewLine;

            if (gu)
            {
                msc = scan("48 C1 E9 01 81 E0 FF 0F 00 00 89 C0 8B 04 01 C4 E3 49 22 C0 00 C5 FA 7F C6 8D 85 74", false, false, true);
                logBlock.Text += ("Found: " + msc.ToString("X")) + Environment.NewLine;
            }
            else if (rise)
            {
                msc = scan("0F 8E F2 7D 00 00 48 C1 E9 01 48 81 E0 FF 0F 00 00 8B 04 01 C4 E3 39", false, false, true);
                logBlock.Text += ("Found: " + msc.ToString("X")) + Environment.NewLine;
                msc += 6;
               
            }

            if (msc != 0 && msc > 0x1000)
                monsterHP();
            else
                pc.theProc = null;
            
                
        }

        private RECT window_rect;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
             x = window_rect.topo + newover.Height - newover.Top;
             y = window_rect.esquerda - newover.Width - newover.Left;

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

                            newover.img.Source = null;
                            newover.pb_hp.OpacityMask = null;

                            var img = new BitmapImage(new Uri("pack://application:,,,/recourses/progressbar.png"));
                            Brush br = new ImageBrush(img);
                            newover.pb_hp.OpacityMask = br;

                            newover.img.Source = new BitmapImage(new Uri("pack://application:,,,/recourses/kamura.png"));
                            this.Title = "Overlay RISE";
                        }
                        else if (pc.theProc.MainWindowTitle.Contains("GENERATIONS ULTIMATE"))
                        {
                            gu = true;
                            rise = false;
                            newover.img.Source = null;
                            newover.pb_hp.OpacityMask = null;

                            var img = new BitmapImage(new Uri("pack://application:,,,/recourses/progressbarGU.png"));
                            Brush br = new ImageBrush(img);

                            newover.pb_hp.OpacityMask = br;
                            newover.img.Source = new BitmapImage(new Uri("pack://application:,,,/recourses/GU.png"));

                            this.Title = "Overlay GU";
                        }

                        if (gu || rise)
                        {
                            scannermemory();
                        }
                    }
                }
            }
            else
            {
                GetWindowRect(pc.theProc.MainWindowHandle, out window_rect);

                newover.Top = window_rect.topo + newover.Height - x;
                newover.Left = window_rect.esquerda - newover.Width - y;
            }
            float current_hp = 1, max_hp = 1;
            if (address_reads > 0x100 && msc > 0x1000)
            {
                var max_life = pc.ReadLong(address_reads.ToString("X"));
                if (max_life > 0)
                {
                    if (gu)
                    {
                        //Resources.Values[0];
                        current_hp = pc.ReadInt(max_life.ToString("X"));
                        max_hp = pc.ReadInt((max_life + 4).ToString("X"));
                    }
                    else if (rise)
                    {
                        current_hp = pc.ReadFloat(max_life.ToString("X"));
                        max_hp = pc.ReadFloat((max_life + 4).ToString("X"));
                    }

                    float porcent = 0;
                    if (current_hp != 0.0f && max_hp != 0.0f)
                    {
                        porcent = (current_hp / max_hp) * 100;
                    }
                    newover.lbl_hp.Content = current_hp + "/" + max_hp;
                    newover.lbl_porcent.Content = porcent + "%";
                    newover.pb_hp.Maximum = max_hp;
                    newover.pb_hp.Value = current_hp;
                }
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
            newover.Height = 80 * (scale.SelectedIndex + 1);
            newover.Width = 250 * (scale.SelectedIndex + 1);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            save_config();
            logBlock.Clear();
            logBlock.AppendText("SAVED!");
        }

        private void chk_acceleration_Click(object sender, RoutedEventArgs e)
        {
            bool x = (bool)chk_acceleration.IsChecked;
            RenderOptions.ProcessRenderMode = x ? RenderMode.Default : RenderMode.SoftwareOnly;
            logBlock.Text += RenderOptions.ProcessRenderMode;
        }
    }
}