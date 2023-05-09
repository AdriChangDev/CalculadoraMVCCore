using Calculadora.Data;
using Calculadora.Intermediario;
using Calculadora.Models;
using System.Xml.Linq;

namespace Calculadora.Repository
{
    public class CalculadoraRepository : ICalculadoraRepository
    {
        private CalculadoraDbContext _context;
        public CalculadoraRepository(CalculadoraDbContext context)
        {
            _context = context;
        }

        public void AddOperation(Operaciones ope)
        {
            try
            {
                _context.Operador.Add(ope);
                _context.SaveChanges();
            }
            catch
            {
                Console.Write("");
            }
        }

        public void DeleteOperation(int id)
        {
            _context.Operador.Remove(_context.Operador.First(x => x.Id == id));
            _context.SaveChanges();
        }

        public IEnumerable<Operaciones> GetOperationById()
        {
            return _context.Operador.Where(p => p.UsuarioId == Estatico.IdConectado).ToList();
        }

        public bool LoginUserPasswd(Usuario user)
        {
            bool login = false;
            try
            {
                if (_context.Users.Any(l => (l.Email == user.Email || l.NombreUsuario == user.NombreUsuario) && l.Password == user.Password))
                {
                    login = true;
                }
                else
                {
                    login = false;
                }

            }
            catch
            {
                login = false;
                throw;

            }
            return login;
        }
        public bool LoginName(string name)
        {
            bool login = true;
            try
            {
                if (_context.Users.Any(l => l.Email == name || l.NombreUsuario == name))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                login = false;
                throw;

            }
            return login;
        }


        public void AddUserWithPasswd(Usuario user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }

        }
        public int GetIdByUser(Usuario user)
        {
            try
            {
                return _context.Users.FirstOrDefault(l => l.Email == user.Email || l.NombreUsuario == user.NombreUsuario).ID;
            }
            catch
            {
                throw;
            }
        }
        public Usuario GetUserByID()
        {
            try
            {
                return _context.Users.FirstOrDefault(l => l.ID == Estatico.IdConectado);
            }
            catch
            {
                throw;
            }
        }
        public void DeleteUser()
        {
            Usuario id = _context.Users.First(l => l.ID == Estatico.IdConectado);
            _context.Users.Remove(id);
            _context.SaveChanges();
        }
        public void EditarUsuario(Usuario user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }
        public Operaciones GetOperacionesById()
        {
            return _context.Operador.FirstOrDefault(l => l.Id == Estatico.idOperacion);
        }
        public Operaciones GetOperacionesById(int id)
        {
            return _context.Operador.FirstOrDefault(l => l.Id == id);

        }

    }
}
