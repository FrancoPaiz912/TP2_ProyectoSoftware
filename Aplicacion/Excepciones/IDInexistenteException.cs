namespace Aplicacion.Excepciones
{
    public class IDInexistenteException : Exception
    {
        public IDInexistenteException(string mensaje): base(mensaje) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }   
    }
}
