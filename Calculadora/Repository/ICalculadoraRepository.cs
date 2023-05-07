using Calculadora.Models;

namespace Calculadora.Repository
{
    public interface ICalculadoraRepository
    {
        public void DeleteOperation(int id);
        public void AddOperation(Operaciones ope);
        public IEnumerable<Operaciones> GetOperationById(int id);
        public bool LoginUserPasswd(string user, string psswd);
        public void AddUserWithPasswd(string user, string psswd);
        public void AddUserName(string user);
        public bool LoginName(string name);
        public int GetUsuarioIDByNamePasswd(string user, string psswd);
    }
}
