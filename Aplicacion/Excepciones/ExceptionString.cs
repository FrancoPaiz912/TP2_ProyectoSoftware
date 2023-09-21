namespace Aplicacion.Excepciones
{
    public class ExceptionString : Exception
    {
        public ExceptionString(string mensaje) : base(mensaje) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}
