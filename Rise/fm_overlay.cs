using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace overlay_testing
{
    public partial class fm_overlay : Form
    {
        public fm_overlay()
        {
            InitializeComponent();
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
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
             IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
        int dwSize, AllocationType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static unsafe extern bool VirtualFreeEx(
           IntPtr hProcess, byte* pAddress,
           int size, AllocationType freeType);

        Process emu;
        RECT window_rect;

        public int winW { get; set; }
        public int winH { get; set; }

        public string allocd { get; set; }
        //public int winW = 1, winH= 1;

        public bool rise  { get; set; }
        public bool gu  { get; set; }

        Mem memory = new Mem();
        long scan(string aob, bool read, bool write, bool executavel)
        {
            long init = 0x0040000000;
            long endit = 0x7FFFFFFFFFF;
            foreach (ProcessModule mod in emu.Modules)
            {
                if (mod.ModuleName.CompareTo("Ryujinx.exe") == 0)
                {
                    endit = (long)mod.BaseAddress;
                }
                 
                if(mod.ModuleName.CompareTo("Ryujinx.dll") == 0)
                {
                    init = (long)mod.BaseAddress;
                }

            }


            Task<IEnumerable<long>> AoBScanResults = memory.AoBScan(init, endit, aob, read, write, executavel);
            IEnumerable<long> respost = AoBScanResults.Result;

            foreach (var rp in respost)
            {
                if(rp > 0)
                {
                    return rp;
                }
                    
            }
            return 0;
           
        }
        public Int64 msc { get; set; }
        Int64 address_reads;
        public Color bar { get; set; }

        float max_hp, current_hp;

        void monsterHP()
        {
            memory.Suspend(emu.Id);

            Console.WriteLine("[ANALISE] Process suspend...");
            var address = VirtualAllocEx(emu.Handle, (IntPtr)0x0, (IntPtr)0x200, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
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
                wm = new byte[]  {0x48, 0xC1, 0xE9, 0x01,                     //- shr rcx,01
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

           


            Console.WriteLine("space allocated to the hook {0} ", address.ToString("X"));

            for (int i = wm.Length - 8; i < wm.Length; i++)
            {
                wm[i] = BitConverter.GetBytes((msc + 14))[i - (wm.Length - 8)];
            }
            Console.WriteLine("[Shell] creating bytes for the hook...");
            byte[] jmp = {0xFF, 0x25, 0x00,00,00,00,
                          0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,00,00}; //BitConverter.GetBytes(address.ToInt64())

            for (int i = jmp.Length - 8; i < jmp.Length; i++)
            {
                jmp[i] = BitConverter.GetBytes(address.ToInt64())[i - (jmp.Length - 8)];
            }
            Console.WriteLine("[Shell] making the detour");

            address_reads = (long)address + 0x100;
            memory.WriteBytes(address.ToString("X"), wm);
            Console.WriteLine("[Write] writing the bytes of the hook ");

            memory.WriteBytes((msc).ToString("X"), jmp);
            Console.WriteLine("[Write] writing the function bytes");

            for (int i = 0; i < nops; i++)
            {
                memory.WriteMemory((msc+jmp.Length +  i).ToString("X"), "byte" , "0x90");
            }

            memory.Resume(emu.Id);
            Console.WriteLine("....DONE!....");
        }

        private void scannermemory()
        {
            Console.WriteLine("SCANNNNNNNNN STARTING WAIT......");

 
            if (gu)
            {

                msc = scan("48 C1 E9 01 81 E0 FF 0F 00 00 89 C0 8B 04 01 C4 E3 49 22 C0 00 C5 FA 7F C6 8D 85 74", false, false, true);
                Console.WriteLine("Found: " + msc.ToString("X"));
            }
            else if(rise)
            {
                msc = scan("0F 8E F2 7D 00 00 48 C1 E9 01 48 81 E0 FF 0F 00 00 8B 04 01 C4 E3 39", false, false, true);
                msc += 6;
                Console.WriteLine("Found: " + msc.ToString("X"));

            }


            if (msc != 0 && msc > 0x1000)
                monsterHP();
        }

        private void fm_overlay_Load(object sender, EventArgs e)
        {

            //bar = Color.DarkOrange;
            //lbl_hp.BackColor = Color.DarkOrange;
        }

        private void fm_overlay_Shown(object sender, EventArgs e)
        {
            this.Text = "Opeeneeddd";
            this.TopMost = true;
            this.TopLevel = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(255, 0, 0, 0);
            panel3.BackColor = this.BackColor;
            this.TransparencyKey = this.BackColor;
            //Console.WriteLine("Overlay opend!");
            timer1.Stop();
            tmr_process.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (emu.MainWindowHandle != null)
            {
                //panel2.BackColor = bar;
                //lbl_hp.BackColor = bar;

                GetWindowRect(emu.MainWindowHandle, out window_rect);           
                var w = Convert.ToInt32((window_rect.esquerda - window_rect.direita) * 0.10);
                if (w < 110)
                    w = 110;

                var h = Convert.ToInt32((window_rect.baixo - window_rect.topo) * 0.025);
                if (h < 60)
                    h = 60;

                this.Size = new Size(w, h);
                this.Top = window_rect.topo + winH;
                this.Left = window_rect.esquerda - w- winW;

                if(address_reads > 0x100 && msc > 0x1000)
                {
                    var max_life = memory.ReadLong(address_reads.ToString("X"));
                    if(max_life > 0)
                    {

                        if (gu)
                        {
                            current_hp  = memory.ReadInt(max_life.ToString("X"));
                            max_hp      = memory.ReadInt((max_life + 4).ToString("X"));
                        }
                        else if (rise)
                        {
                            current_hp  = memory.ReadFloat(max_life.ToString("X"));
                            max_hp      = memory.ReadFloat((max_life + 4).ToString("X"));
                        }

                        //current_hp = memory.ReadFloat(max_life.ToString("X"));
                        //max_hp = memory.ReadFloat((max_life+4).ToString("X"));
                        var poct =  current_hp/ max_hp;
                        panel2.Width = (int)(120 * poct);
                        lbl_hp.Text = current_hp + "/" + max_hp;

                    }
                }
                else if (this.Visible)
                {
                    scannermemory();
                }
            }

            //this.TopLevel = true;
            //this.TopMost = !this.TopMost;
            //this.BringToFront();

        }

        public void restore()
        {
            Console.WriteLine("Restauring....");
            if (address_reads > 0 || msc > 0)
            {
                memory.Suspend(emu.Id);
                byte[] restore = { 0x0,0x0};
                if (rise)
                {
                    msc -= 6;
                    restore =new byte[]{ 0x0F, 0x8E, 0xF2, 0x7D, 0x00, 0x00, 0x48, 0xC1, 0xE9, 0x01, 0x48, 0x81, 0xE0, 0xFF, 0x0F, 0x00, 0x00, 0x8B, 0x04, 0x01, 0xC4, 0xE3, 0x39 };
                    
                }else if (gu)
                {
                    //msc -= 6;
                    restore = new byte[] { 0x48, 0xC1, 0xE9, 0x01, 0x81, 0xE0, 0xFF, 0x0F, 0x00, 0x00, 0x89, 0xC0, 0x8B, 0x04, 0x01, 0xC4, 0xE3, 0x49, 0x22, 0xC0, 0x00, 0xC5, 0xFA, 0x7F, 0xC6, 0x8D, 0x85, 0x74 };
                    //memory.WriteBytes(msc.ToString("X"), restore);
                }

                memory.WriteBytes(msc.ToString("X"), restore);
                address_reads -= 0x100;
                VirtualFreeEx(emu.Handle, (IntPtr)address_reads, 0x200, AllocationType.Commit | AllocationType.Reserve);
                memory.Resume(emu.Id);
                address_reads = 0;
                msc = 0;
                Console.WriteLine("....DONE!....");
                this.Hide();
            }
            else
            {
                Console.WriteLine("eRrO bOi I nEeD dO aNyThInG tO ReStOrE");
            }
        }

        private void tmr_process_Tick(object sender, EventArgs e)
        {
            if(emu == null || emu.HasExited)
            {
                var x = Process.GetProcessesByName("Ryujinx");
                if (x.Length > 0)
                {
                    emu = x[0];
                }
                else
                {
                    Console.WriteLine("Try again...");
                    Hide();
                }
            }
            else if (emu != null && emu.MainWindowTitle.Contains("MONSTER HUNTER RISE") || emu.MainWindowTitle.Contains("MONSTER HUNTER GENERATIONS ULTIMATE"))
            {
                Console.WriteLine("Found! " + emu.MainWindowTitle);
                if (memory.OpenProcess(emu.Id))
                    timer1.Enabled = true;
                else
                    fm_overlay_Shown(sender, e);

                tmr_process.Stop();
            }
            else
            {
               
                timer1.Stop();
                tmr_process.Stop();
                if(emu != null)
                {
                    Console.WriteLine(emu.MainWindowTitle);
                    emu.Close();
                    emu = null;
                }
                Console.WriteLine("Maybe the emulator is open, but I didn't find the game, please open the game and load it and try to open the overlay again");
                this.Hide();
            }

            //this.TopMost = true;
            //this.BringToFront();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
          
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void fm_overlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)//if(GetAsyncKeyState(Keys.F5) > 0)
            {
                scannermemory();
            }
            if (e.KeyCode == Keys.F6) 
            {
                restore();
            }
        }
    }
}
