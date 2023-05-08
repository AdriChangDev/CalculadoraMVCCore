using Calculadora.Models;

namespace Calculadora.Repository
{
    public interface ICalculadoraRepository
    {
        public void DeleteOperation(int id);
        public void DeleteUser();
        public void AddOperation(Operaciones ope);
        public IEnumerable<Operaciones> GetOperationById(int id);
        public bool LoginUserPasswd(Usuario user);
        public void AddUserWithPasswd(Usuario user);
        public bool LoginName(string name);
        public int GetIdByUser(Usuario user);
        public Usuario GetUserByID(int id);
        public void EditarUsuario(Usuario user);
    }
}
