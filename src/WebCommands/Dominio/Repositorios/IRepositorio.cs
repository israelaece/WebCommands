namespace WebCommands.Dominio.Repositorios
{
    public interface IRepositorio<T> where T : class
    {
        void Adicionar(T entidade);

        T BuscarPor(string codigo);
    }
}