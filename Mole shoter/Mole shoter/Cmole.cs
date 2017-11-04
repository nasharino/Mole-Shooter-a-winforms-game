using Mole_shoter.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mole_shoter
{

    class Cmole : CImageBase //faz com que a classe Cmole vá buscar  métodos e propriedades à classe                           // BaseImagem poupando código em várias classes
    {

        private Rectangle _molekHotSpot = new Rectangle();

        public Cmole()        // 
            : base(Resources.mole)  // para aceder a pasta recursos
        {
            _molekHotSpot.X = left + 20;
            _molekHotSpot.Y = top - 1;
            _molekHotSpot.Width = 30;
            _molekHotSpot.Height = 40;

        }



        public void Update(int X, int Y)  // faz com que possamos acertar só no mole e nao no resto do fundo da imagem
        {                                   // cria um rectangulo a volta da imagem
            left = X;
            top = Y;
            _molekHotSpot.X = left + 20;
            _molekHotSpot.Y = top - 1;

        }
        public bool Hit(int X, int Y)      // se o rato estiver dentro do hotspot returna true, senao returna false
        {                                   // cria um outro rectangulo e verifica se um rectangulo esta dentro de outro.
            Rectangle c = new Rectangle(X, Y, 1, 1);


            if (_molekHotSpot.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
        
}
