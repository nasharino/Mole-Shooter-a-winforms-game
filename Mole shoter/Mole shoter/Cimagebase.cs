using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mole_shoter
{
    class CImageBase : IDisposable          // Dispose server para outras classes virem aqui buscar recursos de modo a poupar codigo alocado em memoria
                                            // para cada classe
    {
            bool disposed = false;

            Bitmap _bitmap;
            private int X;
            private int Y;

            public int left { get { return X; } set { X = value; } }
            public int top { get { return Y; } set { Y = value; } }

        public CImageBase (Bitmap _Resources)      
        {
            _bitmap = new Bitmap(_Resources);
        }
        public void DrawImage (Graphics gfx)     // para fazer draw image nas cordenadas
        {
            gfx.DrawImage(_bitmap, X, Y);
        }
            public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose (bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            { _bitmap.Dispose(); }
        }


    }
}
