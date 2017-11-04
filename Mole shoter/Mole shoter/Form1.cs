#define debug

using Mole_shoter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mole_shoter
{
    public partial class Form1 : Form
    {
        Cmole _mole;                // para declarar as diferentes classes
        CSplat _splat;
        Csign _sign;
        CScore _Scoreframe;
        int _cursX = 0;                 // declara as variaveis para as coordenadas do rato
        int _cursY = 0;
        int gameframe = 0;              
        int splaTime = 0;               //declara a splat
        bool splat = false;


        const int Num = 8;              
        const int Splatnum = 3;

        int hits = 0;                   //variaveis do quadro
        int misses = 0;
        int totalshots = 0;
        double averagehits = 0;

        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            // para criar a mira
            Bitmap b = new Bitmap(Resources.site);
            this.Cursor = CustomCursor.CreateCursor(b, b.Height / 2, b.Width / 2);


            _mole = new Cmole() { left = 10, top = 200 };  // coloca os recuros nas cordenadas 
            _sign = new Csign() { left = 350, top = 100 };
            _Scoreframe = new CScore() { left = 10, top = 10 };
            _splat = new CSplat();                                
        }

        private void timergameloop_Tick(object sender, EventArgs e)             // componentes do timer
        {
            if(gameframe>= Num)
            {                           // para a mole dar update
                UpdateMole();
                gameframe = 0;
            }
            if (splat)
            {
                if (splaTime>= Splatnum)     // para a splat dar refresh
                {
                    splat = false;
                    splaTime = 0;
                    UpdateMole();
                }
                splaTime++;
            }
            if (averagehits >50 || averagehits<80)      //dificuldade do jogo
            {
                timergameloop.Interval = 750;
            }
            if (averagehits > 80)
            {
                timergameloop.Interval = 550;
            }
            if (averagehits <50)
                {
                timergameloop.Interval = 900;
                }
            if (averagehits >= 80)
            {
                if (hits >10)
                {
                    SoundPlayer simpleSound = new SoundPlayer(Resources.leroy);
                    simpleSound.Play();
                    timergameloop.Stop();
                    MessageBox.Show("Parabens matou todas as toupeiras");

                }
            }
           

            gameframe++;
            UpdateMole();
            this.Refresh();
        }
        private void UpdateMole()  // Random para fazer a mole aparecer em varios locais
        {
            _mole.Update(
                rnd.Next(Resources.mole.Width, this.Width - Resources.mole.Width),
                rnd.Next(this.Height / 2, this.Height - Resources.mole.Height * 2));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            if (splat==true)                //se acertar na mole, mostra a splat e da refresh da mole
            {
                _splat.DrawImage(dc);
            } 
            else
            {
                _mole.DrawImage(dc); 
            }
            

            
#if debug
            /*TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;    //formata o texto horizontalmente, no caso de um rectangulo vai para o centro do rectangulo
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);                     //declara a fonte de texto
            TextRenderer.DrawText(dc, "X=" + _cursX.ToString() + ":" + "Y=" + _cursY.ToString(), _font,  // para imprimir o texto
                new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flags);*/
            // para fazer o rectangulo
#endif 

            // Para escrever texto nas coordenadas
            _Scoreframe.DrawImage(dc);
            _sign.DrawImage(dc);
            TextFormatFlags flags = TextFormatFlags.Left;
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(e.Graphics,"Tiros: " + totalshots.ToString(),_font,new Rectangle (30,32,120,20),SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Acertou: " + hits.ToString(), _font, new Rectangle(30, 52, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Falhou: " + misses.ToString(), _font, new Rectangle(30,72, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Acertou: " + averagehits.ToString()+"%", _font, new Rectangle(30, 92, 120, 20), SystemColors.ControlText, flags);
            if ( averagehits<=50) TextRenderer.DrawText(e.Graphics, "Lvl: Noob ", _font, new Rectangle(30, 112, 120, 20), SystemColors.ControlText, flags); 
            if (averagehits >50&&averagehits<80) TextRenderer.DrawText(e.Graphics, "Lvl: Medium ", _font, new Rectangle(30, 112, 120, 20), SystemColors.ControlText, flags);
            if (averagehits >= 80) TextRenderer.DrawText(e.Graphics, "Lvl: Hell ", _font, new Rectangle(30, 112, 120, 20), SystemColors.ControlText, flags);
            base.OnPaint(e);
        }


        private void Form1_MouseMove_1(object sender, MouseEventArgs e)
        {
            _cursX = e.X;            // com a impressao das coordenadas no ecra
            _cursY = e.Y;


            this.Refresh();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)     //coordenadas do start, reset, stop etc e seus parametros
        {
            if (e.X > 434 && e.X<472 && e.Y >174 && e.Y < 184 )
            {
                
                timergameloop.Start();
                averagehits = 0;

            }
             else if (e.X > 434 && e.X < 472 && e.Y >189  && e.Y <200 )
            {
                timergameloop.Stop();
                SoundPlayer simpleSound = new SoundPlayer(Resources.RA);
                simpleSound.Play();
                MessageBox.Show("Jogo Pausado, aqui está um hit dos 80's para desanuviar");
               // if ()
                {
                    timergameloop.Start();
                }
               
            }
             else if (e.X > 434 && e.X < 472 && e.Y > 204 && e.Y < 213)
            {
                
                timergameloop.Stop();
                 hits = 0;
                 misses = 0;
                 totalshots = 0;
                 averagehits = 0;
                timergameloop.Start();
            }
           else  if (e.X > 434 && e.X < 472 && e.Y > 217 && e.Y < 228)
            {
                this.Close();
            }
            else
            {
                if (_mole.Hit(e.X,e.Y))
                {
                    splat = true;
                    _splat.left = _mole.left - Resources.blood.Width / 3;
                    _splat.top = _mole.top - Resources.blood.Width / 3;
                    hits=hits+1;
                }
                 else 
                {
                    misses=misses+1;
                }
             

                
                
            }

            totalshots = hits + misses;
            averagehits = (double)hits / (double)totalshots *100.0;
            gunshot();
        }
        private void gunshot()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.gunshot);
            simpleSound.Play();
        }
    }
}
