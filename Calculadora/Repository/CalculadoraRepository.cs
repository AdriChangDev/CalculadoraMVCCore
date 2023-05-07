using Calculadora.Data;
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

        public IEnumerable<Operaciones> GetOperationById(int id)
        {
            return _context.Operador.Where(p => p.UsuarioId == id).ToList();
        }

        public bool LoginUserPasswd(string user, string psswd)
        {
            bool login = true;
            try
            {
                if (_context.Users.Any(l => l.Email == user && l.Password == psswd))
                {
                    login=true;
                }
                else
                {
                    login=false;
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
                if (_context.Users.Any(l => l.Email == name))
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
        public int GetUsuarioIDByNamePasswd(string name, string psswd)
        {
            return _context.Users.First(o => o.Email==name ).ID;
        }

        public void AddUserWithPasswd(string user, string psswd)
        {
            try { 
            _context.Users.Add(new Usuario
            {
                Email = user,
                Password = psswd
            });
            _context.SaveChanges();
            }
            catch
            {
                throw;
            }

		}

        public void AddUserName(string user)
        {
            _context.Users.Add(new Usuario
            {
                Email = user,
            });
            _context.SaveChanges();
        }



    }
}
