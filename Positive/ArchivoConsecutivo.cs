namespace Positive
{
    public class ArchivoConsecutivo
    {
        private int Consecutivo;
        public ArchivoConsecutivo(int consecutivo)
        {
            Consecutivo = consecutivo;
        }
        public int ObtenerActual()
        {
            return Consecutivo;
        }
        public int ObtenerSiguiente()
        {
            int siguiente = this.ObtenerActual() + 1;
            return siguiente;
        }
        public int Retroceder()
        {
            int anterior = this.ObtenerActual() - 1;
            return anterior;
        }
    }
}